using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePlan.Service
{
    public class RectangularTable
    {
        public Int32 StartPositionX { get; set; }
        public Int32 StartPositionY { get; set; }

        public Int32 EndPositionX { get; set; }
        public Int32 EndPositionY { get; set; }

        public Int32 Length { get; set; }
        public Int32 Width { get; set; }
        public Int32 NumberOfChairs { get; set; }

        public string TableKey { get; set; }
        public new List<string> ChairKeys { get; set; }


        public Int32 SpaceBetweenChairs
        {
            get
            {
                return (Int32)(Length * 0.2 / ((NumberOfChairs / 2) + 1));
            }
        }
        public Int32 ChairSize
        {
            get
            {
                return (Int32)((Length * 0.8) / (NumberOfChairs / 2));
            }
        }

        public double SpaceBetweenChairsAxisX
        {
            get
            {
                return ((EndPositionX - StartPositionX) * 0.2 / ((NumberOfChairs / 2) + 1));
            }
        }
        public double ChairSizeAxisX
        {
            get
            {
                return (((EndPositionX - StartPositionX) * 0.8) / (NumberOfChairs / 2));
            }
        }

        public double SpaceBetweenChairsAxisY
        {
            get
            {
                return ((EndPositionY - StartPositionY) * 0.2 / ((NumberOfChairs / 2) + 1));
            }
        }
        public double ChairSizeAxisY
        {
            get
            {
                return (((EndPositionY - StartPositionY) * 0.8) / (NumberOfChairs / 2));
            }
        }

        public Int32 StartPositionSecondRowX
        {
            get
            {
                double b1 = EndPositionX - StartPositionX;
                double a1 = EndPositionY - StartPositionY;

                int c1 = Length;
                int c2 = Width;

                var beta1 = Math.Atan(b1 / a1);

                var a2 = c2 * Math.Cos(beta1);

                if (StartPositionY < EndPositionY)
                    return StartPositionX + (int)a2;
                else
                    return StartPositionX - (int)a2;
            }
        }

        public Int32 StartPositionSecondRowY
        {
            get
            {
                double c = Width;
                double a = EndPositionX - StartPositionX;
                double b = EndPositionY - StartPositionY;

                var beta = Math.Atan(b / a);
                var a2 = Width * Math.Cos(beta);

                return StartPositionY - (int)a2;
            }
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public List<Chair> SeatCoordinates
        {
            get
            {
                var entries = new List<Chair>();

                // check if table has no angle/rotation
                if (StartPositionX == EndPositionX)
                {
                    double x = StartPositionX;
                    double y = StartPositionY + ChairSizeAxisY / 2;

                    for (int i = 0; i < NumberOfChairs; i++)
                    {
                        if (i == NumberOfChairs / 2)
                        {
                            x = x + Width;
                            y = StartPositionSecondRowY + ChairSizeAxisY / 2;
                        }
                        y = y + SpaceBetweenChairsAxisY;

                        entries.Add(new Chair { Key = ChairKeys[i], PosX = (Int32)x, PosY = (Int32)y });

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
                        if (i == NumberOfChairs / 2)
                        {
                            x = StartPositionSecondRowX + ChairSizeAxisX / 2;
                            b = StartPositionSecondRowY - m * StartPositionSecondRowX;
                        }
                        x = x + SpaceBetweenChairsAxisX;

                        var y = m * x + b;

                        entries.Add(new Chair { Key = ChairKeys[i], PosX = (Int32)x, PosY = (Int32)y });

                        x = x + ChairSizeAxisX;
                    }
                    return entries;
                }
            }
        }

    }
}
