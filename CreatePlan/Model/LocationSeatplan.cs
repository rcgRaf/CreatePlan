using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePlan.Model
{
    public partial class LocationSeatplan
    {
        public LocationSeatplan()
        {
            LocationSeats = new List<LocationSeat>();
        }

        public string Name { get; set; }
        public string PlanImagePath { get; set; }
        public int PlanWidth { get; set; }
        public int PlanHeight { get; set; }

        public ICollection<LocationSeat> LocationSeats { get; set; }
    }
}
