using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Model
{
    internal class Shop
    {
        public int  SID { get; set; }
        public string Sname { get; set; }
        public string Vat { get; set; }
        public string Oname { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Odate { get; set; }
        public decimal Rate { get; set; }
        public decimal Advance { get; set; }
        public decimal Dueamount { get; set;}
    }
}
