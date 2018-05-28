using CreatePlan.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePlan.Examples
{
    [TestClass]
    public class Table
    {
        [TestMethod]
        public void ExampleTable()
        {
            var location = Helper.CreateLocationSeatplan("Testplan", "Background.jpg", 2792, 2792);

            #region Draw

            var rectTableDeg = new RectangularTable();
            rectTableDeg.TableKey = "58";
            rectTableDeg.ChairKeys = new List<string>() { "Tisch 1, Platz 3", "Tisch 1, Platz 2", "Tisch 1, Platz 1", "Tisch 1, Platz 6", "Tisch 1, Platz 5", "Tisch 1, Platz 4" };
            rectTableDeg.StartPositionX = 1892;
            rectTableDeg.StartPositionY = 1558;
            rectTableDeg.EndPositionX = 2030;
            rectTableDeg.EndPositionY = 1631;
            rectTableDeg.Width = 107;
            rectTableDeg.Length = 157;
            rectTableDeg.NumberOfChairs = 6;
            Helper.Draw(rectTableDeg, location);

            #endregion

            #region Export PDF
            Helper.SavePdfToFileSystem(location);
            #endregion
        }
    }
}
