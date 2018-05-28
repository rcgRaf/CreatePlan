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
        public void ExampleRectangle()
        {




            #region Draw
            var location = Helper.CreateLocationSeatplan("Testplan", "Background.jpg", 2792, 2792);

            for (int j = 0; j < 7; j++)
            {
                if (j == 0 || j == 1)
                    for (int i = 0; i < 4; i++)
                    {
                        var r = j * 4;

                        DrawTable(2037 - i * 384, 652 + j * 307, (i + 1) + r, location);
                    }
                else
                {
                    var r = (j+1)*3;
                    for (int i = 0; i < 3; i++)
                        DrawTable(2037 - (i + 1) * 384, 652 + j * 307,i+r , location);
                }
            }





            #endregion

            #region Export PDF
            Helper.SavePdfToFileSystem(location);
            #endregion


        }


        public void DrawTable(int x, int y, int num, Model.LocationSeatplan location)
        {

            var xDist = 94;
            var xDist2 = 42;
            var yDist = 68;



            for (var key = 1; key <= 6; key++)
            {
                switch (key)
                {
                    case 1:
                        DrawSeat(x, y, key, num, location);
                        break;

                    case 2:
                        DrawSeat(x - xDist, y, key, num, location);
                        break;

                    case 3:
                        DrawSeat(x - xDist - xDist2, y + yDist, key, num, location);
                        break;

                    case 4:
                        DrawSeat(x - xDist, y + yDist * 2, key, num, location);
                        break;
                    case 5:
                        DrawSeat(x, y + yDist * 2, key, num, location);
                        break;
                    case 6:
                        DrawSeat(x + xDist2, y + yDist, key, num, location);
                        break;

                    default:
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
                ChairSize = 25,
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
