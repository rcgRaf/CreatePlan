using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Collections.Generic;
using CreatePlan.Service;

namespace CreatePlan
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void CreatePlan()
        {
            //BackgroundImage.jpg is in Root Folder of the Project. Replace it with the Picture of the Plan
            var location = Helper.CreateLocationSeatplan("Testplan", "BackgroundImage.jpg", 2792, 2792);


            #region generate seatplan
            //Code to generate the plan here


            #endregion

            #region Export PDF
            //No changes needed here. Helps to see the results of the generation
            Helper.SavePdfToFileSystem(location);
            #endregion

        }

    }
}
