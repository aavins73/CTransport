using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationSearchRoute.Assets
{
    [Table("LocationSearchRte")]
    public class LocationSearchRte
    {
        [PrimaryKey, AutoIncrement,NotNull]
        public int Id { get; set; }
        public string BoardLocation { get; set; }
        public string BoardTime { get; set; }
        public string RouteNo { get; set; }
        public string EndLocation { get; set; }
        public string DropTime { get; set; }
    }
}
