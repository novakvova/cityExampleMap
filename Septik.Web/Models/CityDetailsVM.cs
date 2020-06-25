using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Septik.Web.Models
{
    public class CityDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Images { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
    }
}
