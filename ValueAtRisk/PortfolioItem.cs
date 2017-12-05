using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueAtRisk
{
    class PortfolioItem
    {
        public PortfolioItem(string index, decimal darab)
        {
            this.Darab = darab;
            this.Index = index;
        }

        public string Index { get; set; }
        public decimal Darab { get; set; }
    }
}
