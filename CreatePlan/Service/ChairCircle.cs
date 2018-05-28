using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePlan.Service
{
    public class ChairCircle
    {
        public new List<ChairKey> ChairKeys { get; set; }

        public Int32 CircleCenterX { get; set; }
        public Int32 CircleCenterY { get; set; }

        public Int32 Radius { get; set; }

        public Int32 NumberOfChairs { get; set; }

        public Int32 ChairSize { get; set; }

        /// <summary>
        /// Der Winkel, unter welchem der erste Platz gezeichnet wird.
        /// 0 = der äusserste rechte Punkt (mitte rechts)
        /// 90 = der tiefste Punkt (mitte, unten)
        /// 180 = der äusserste linke Punkt (mitte links)
        /// 270 = der höchste Punkt
        /// </summary>
        public int? StartPositionDegree { get; set; }
        public int? EndPositionDegree { get; set; }

        public double Angle
        {
            get
            {
                if (!StartPositionDegree.HasValue)
                    StartPositionDegree = 0;
                if (!EndPositionDegree.HasValue)
                    EndPositionDegree = 360;

                return (EndPositionDegree.Value - StartPositionDegree.Value) * Math.PI / 180 / NumberOfChairs;
            }
        }

        public List<Chair> SeatCoordinates
        {
            get
            {
                var entries = new List<Chair>();

                if (!StartPositionDegree.HasValue)
                    StartPositionDegree = 0;
                if (!EndPositionDegree.HasValue)
                    EndPositionDegree = 360;

                for (int i = 0; i < NumberOfChairs; i++)
                {
                    var additionalAngle = (Math.PI) * StartPositionDegree.Value / 180;// NumberOfChairsStartPositionDegree.Value* Angle;
                    var x = Math.Cos(i * Angle + additionalAngle) * Radius + CircleCenterX;
                    var y = Math.Sin(i * Angle + additionalAngle) * Radius + CircleCenterY;
                    entries.Add(new Chair { Key = ChairKeys[i].Key, PosX = (Int32)x, PosY = (Int32)y, Invisible = ChairKeys[i].Invisible });
                }
                return entries;
            }
        }
    }
}
