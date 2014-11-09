using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CTransport.Assets
{
    [Table("route")]
    public class route
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Location { get; set; }
        public string RouteNo { get; set; }
       // public string Location1 { get; set; }
       /* public string Location2 { get; set; }
        public string Location3 { get; set; }
        public string Location4 { get; set; }
        public string Location5 { get; set; }*/
    }
}

