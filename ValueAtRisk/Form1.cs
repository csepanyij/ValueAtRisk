using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValueAtRisk
{
    public partial class Form1 : Form
    {
        List<Tick> Ticks;
        AdatbazisDataContext db = new AdatbazisDataContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ticks = db.Ticks.ToList();
            dataGridView1.DataSource = Ticks;
        }
    }
}
