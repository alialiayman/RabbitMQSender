using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitMQSender
{
    public partial class Form1 : Form
    {
        int counter;
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Top = 0;
            Left = 0;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "rabbitmq.cloudvirga.local",
                Port = Protocols.DefaultProtocol.DefaultPort,
                UserName = "guest",
                Password = "guest"
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();
            model.QueueDeclare("Ayman.Queue", true, false, false, null);
            for (int i = 0; i < int.Parse(dfnNumberOfMessages.Text); i++)
            {
                model.BasicPublish("","Ayman.Queue",null,Encoding.Default.GetBytes((counter++) + "-" + DateTime.Now.ToString("HH:mm:ss.fff")));
            }


        }
    }
}
