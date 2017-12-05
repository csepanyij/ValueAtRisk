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
        BindingList<PortfolioItem> Portfolio = new BindingList<PortfolioItem>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ticks = db.Ticks.ToList();
            dataGridView1.DataSource = Ticks;
            

            dataGridView2.DataSource = Portfolio;
        }

        decimal PortfolioErtek(DateTime datum)
        {
            decimal ertek = 0;
            foreach (var elem in Portfolio)
            {
                var last = (from x in Ticks where elem.Index == x.Index.Trim() && datum <= x.TradingDay select x).First();
                ertek += (decimal)last.Price * elem.Darab;
            }
            return ertek;
        }
    }
}
