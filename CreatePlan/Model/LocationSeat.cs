using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePlan.Model
{
    public partial class LocationSeat
    {
        public LocationSeat()
        {
        }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public string SeatIdentification { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool VisibleInSeatplan { get; set; }

    }
}
