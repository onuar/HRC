using System;
using System.Windows.Forms;
using Entities;
using HRC.Library.ContextFoundation;
using HRC.Library.DatabaseObject.DatabaseSchema;

namespace HAL9000Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = "processing";
            var il = new Il { Ad = "proxy tester", Aktif = true };

            //classic
            //IIlBusiness ilb = HRC.Library.ContextFoundation.ProxyGenerator<IlBusiness, IIlBusiness>.GetProxy();
            //cache
            IIlBusiness ilb = ProxyHelper<IlBusiness, IIlBusiness>.Instance.AddOrGet();
            ilb.DoSomething(il);

            this.Text = "process completed";

            updateCounter++;
            label1.Text = updateCounter.ToString();
        }

        int updateCounter = 1;
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = updateCounter.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var il = new Il { Ad = "hede" };
            var schema = SchemaCollection.Instance.GetSchema(il);
        }
    }
}