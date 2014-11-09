using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteList.Assets
{
    public class Rno
    {
        public Rno() { }

        public Rno(string routenumber)
        {
            Routenumber = routenumber;
        }
        public string Routenumber { get; set; }


        public override string ToString()
        {
            return Routenumber.ToString();
        }
    }

}
