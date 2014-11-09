using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuttleService.Assets
{
   [Table("ShuttleSearch")]
    public class ShuttleSearch
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }
        public string Service { get; set; }
        public string FromLocation { get; set; }
        public string PickUpTime { get; set; }      
        public string EndLocation { get; set; }
        public string DropTime { get; set; }
    }
}
