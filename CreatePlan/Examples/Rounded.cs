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
    public class Rounded
    {
        [TestMethod]
        public void ExampleRounded()
        {
            var location = Helper.CreateLocationSeatplan("Testplan", "BackgroundImage.jpg", 2792, 2792);

            #region Draw
            List<ChairKey> chairKeys;
            const int chairSize = 15;

            chairKeys = new List<ChairKey>();
            for (var seat = 1; seat <= 50; seat++)
            {
                chairKeys.Add(new ChairKey { Key = seat.ToString(CultureInfo.InvariantCulture), Invisible = false });
            }
            var chairCircle = new ChairCircle
            {
                ChairSize = chairSize,
                ChairKeys = chairKeys,
                NumberOfChairs = chairKeys.Count,
                CircleCenterX = 1038,
                CircleCenterY = 1310,
                Radius = 240
            };
            Helper.Draw(chairCircle, location, "Circle 1, Platz:");
            #endregion

            #region Export PDF
            Helper.SavePdfToFileSystem(location);
            #endregion
        }
    }
}
