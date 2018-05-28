using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePlan.Service
{
    public class ChairRow
    {
        public new List<ChairKey> ChairKeys { get; set; }

        public Int32 StartPositionX { get; set; }
        public Int32 StartPositionY { get; set; }

        public Int32 EndPositionX { get; set; }
        public Int32 EndPositionY { get; set; }

        public Int32 NumberOfChairs { get; set; }

        public Int32 ChairSize { get; set; }

        public double SpaceBetweenChairsAxisX
        {
            get
            {
                return ((EndPositionX - StartPositionX) * 0.2 / (NumberOfChairs + 1));
            }
        }
        public double ChairSizeAxisX
        {
            get
            {
                return (((EndPositionX - StartPositionX) * 0.8) / NumberOfChairs);
            }
        }

        public double SpaceBetweenChairsAxisY
        {
            get
            {
                return ((EndPositionY - StartPositionY) * 0.2 / (NumberOfChairs + 1));
            }
        }
        public double ChairSizeAxisY
        {
            get
            {
                return (((EndPositionY - StartPositionY) * 0.8) / NumberOfChairs);
            }
        }

        public List<Chair> SeatCoordinates
        {
            get
            {
                var entries = new List<Chair>();

                // check if row has no angle/rotation
                if (StartPositionX == EndPositionX)
                {
                    double x = StartPositionX;
                    double y = StartPositionY + ChairSizeAxisY / 2;

                    for (int i = 0; i < NumberOfChairs; i++)
                    {
                        y = y + SpaceBetweenChairsAxisY;

                        entries.Add(new Chair { Key = ChairKeys[i].Key, PosX = (Int32)x, PosY = (Int32)y, Invisible = ChairKeys[i].Invisible });

                        y = y + ChairSizeAxisY;
                    }
                    return entries;
                }
                else
                {

                    double m = ((double)EndPositionY - (double)StartPositionY) / ((double)EndPositionX - (double)StartPositionX); // Steigung
                    double b = StartPositionY - m * StartPositionX; // Achsenhöhe

                    double x = StartPositionX + ChairSizeAxisX / 2; ;


                    for (int i = 0; i < NumberOfChairs; i++)
                    {
                        x = x + SpaceBetweenChairsAxisX;

                        var y = m * x + b;

                        entries.Add(new Chair { Key = ChairKeys[i].Key, PosX = (Int32)x, PosY = (Int32)y, Invisible = ChairKeys[i].Invisible });

                        x = x + ChairSizeAxisX;
                    }
                    return entries;
                }
            }
        }
    }
}
