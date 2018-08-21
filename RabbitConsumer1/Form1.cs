using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitConsumer1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Top = 0;
            Left = Screen.PrimaryScreen.WorkingArea.Width - Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ConsumeInTheBackGround();

        }

        private void ConsumeInTheBackGround()
        {
            Task.Factory.StartNew(()=> {


                var connectionFactory = new ConnectionFactory
                {
                    HostName = "rabbitmq.cloudvirga.local",
                    Port = Protocols.DefaultProtocol.DefaultPort,
                    UserName = "guest",
                    Password = "guest"
                };

                var connection = connectionFactory.CreateConnection();
                var model = connection.CreateModel();
                var consumer = new QueueingBasicConsumer(model);
                model.BasicConsume("Ayman.Queue", false, consumer);
                while (true)
                {
                    var deliveryArgs = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                    var message = Encoding.Default.GetString(deliveryArgs.Body);
                    Invoke(new MethodInvoker(() =>
                    {
                        listBox1.Items.Insert(0,message);
                    }
                        ));

                    model.BasicAck(deliveryArgs.DeliveryTag, false);
                }
            });

        }
    }
}
