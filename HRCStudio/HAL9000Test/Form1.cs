using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HRC.Library;
using HRC.Foundation;
using HRC.Library.ContextFoundation;

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
            Entities.Il il = new Entities.Il();
            il.Ad = "proxy tester";
            il.Aktif = true;

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
    }
}