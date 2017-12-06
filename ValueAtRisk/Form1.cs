using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValueAtRisk
{
    public partial class Form1 : Form
    {
        List<Tick> Ticks;
        List<decimal> Nyeresegek = new List<decimal>();
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

            Portfolio.Add(new PortfolioItem("OTP", 10));
            Portfolio.Add(new PortfolioItem("ZWACK", 10));
            Portfolio.Add(new PortfolioItem("ELMU", 5));

            int intervallum = 30;
            DateTime kezdo = (from x in Ticks select x.TradingDay).Min();
            DateTime zaro = (from x in Ticks select x.TradingDay).Max();
            TimeSpan z = zaro - kezdo;
            for (int i = 0; i < z.Days - intervallum; i++)
            {
                decimal ny = PortfolioErtek(kezdo.AddDays(i + intervallum)) - PortfolioErtek(kezdo.AddDays(i));
                Nyeresegek.Add(ny);
                Console.WriteLine(i + ". " + ny);
            }

            List<decimal> nyeresegekRendezve = (from x in Nyeresegek orderby x select x).ToList();

            dataGridView2.DataSource = nyeresegekRendezve;
            MessageBox.Show(nyeresegekRendezve[nyeresegekRendezve.Count() / 5].ToString());

            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName);
                for (int i = 0; i < nyeresegekRendezve.Count; i++)
                {
                    sw.WriteLine(nyeresegekRendezve[i]);
                }
                sw.Close();
            }
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
