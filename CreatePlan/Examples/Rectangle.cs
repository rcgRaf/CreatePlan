using CreatePlan.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePlan.Examples
{
    [TestClass]
    public class Rectangle
    {
        [TestMethod]
        public void DrawPlan()
        {




            #region Draw
            var location = Helper.CreateLocationSeatplan("Testplan", "Background.jpg", 2792, 2792);

            var r = 1;

            var x = 477;

            var y = 972;

            var t = 252;

            var n = 4;

            for (int j = 0; j <4; j++)
            {
                n = 4;
                t = 0;

                if (j % 2 == 1)
                {
                    n = 5;
                    t = 252;
                }
                    for (int i = 0; i < n; i++)
                    {
                        DrawTable(x - t + i * 540, y + j * 432,  r, location);
                        r++;
                    }
               
            }





            #endregion

            #region Export PDF
            Helper.SavePdfToFileSystem(location);
            #endregion


        }


        public void DrawTable(int x, int y, int num, Model.LocationSeatplan location)
        {

            var xDist = 270;
            var yDist = 65;

            y = y - yDist;

            //459       459
            //972      1233     261         65.25

            for (var key = 1; key <= 8; key++)
            {
                switch (key)
                {
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        DrawSeat(x+xDist, y+(key+1%5)* yDist, key, num, location);
                        break;


                    default:
                        DrawSeat(x, y+key*yDist, key, num, location);
                        break;
                }


            }
        }

        public void DrawSeat(int x, int y, int key, int table, Model.LocationSeatplan location)
        {
            var chairKeys = new List<ChairKey>();
            chairKeys.Add(new ChairKey { Key = key.ToString(CultureInfo.InvariantCulture), Invisible = false });
            var chairRow = new ChairRow
            {
                ChairSize = 41,
                ChairKeys = chairKeys,
                StartPositionX = x,
                StartPositionY = y,
                EndPositionX = x,
                EndPositionY = y,
                NumberOfChairs = chairKeys.Count
            };

            Helper.Draw(chairRow, location, "Tisch " + table + ", Platz ");

        }



    }
}
