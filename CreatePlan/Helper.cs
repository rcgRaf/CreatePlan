using CreatePlan.Model;
using CreatePlan.Service;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePlan
{
    class Helper
    {
        private static readonly BaseFont _fontBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        private const float SpaceForHeader = 120f;
        public static void Draw(ChairRow chairRow, LocationSeatplan locationSeatplan, string description = "")
        {
            //   var rnd = new Random();

            foreach (var chair in chairRow.SeatCoordinates)
            {
                if (chair.Invisible) continue;

                // location seat
                var seat = Helper.CreateLocationSeat(locationSeatplan, chair.PosX, chair.PosY, chairRow.ChairSize,
                    chairRow.ChairSize, description + chair.Key);
            }

        }
        public static void Draw(ChairCircle chairRow, LocationSeatplan locationSeatplan, string description = "")
        {
            //   var rnd = new Random();

            foreach (var chair in chairRow.SeatCoordinates)
            {
                if (chair.Invisible) continue;

                // location seat
                var seat = Helper.CreateLocationSeat(locationSeatplan, chair.PosX, chair.PosY, chairRow.ChairSize,
                    chairRow.ChairSize, description + chair.Key);
            }
        }
        public static void Draw(RectangularTable rectangularTable, LocationSeatplan locationSeatplan)
        {
            var rnd = new Random();

            foreach (var chair in rectangularTable.SeatCoordinates)
            {

                if (chair.Invisible) continue;
                // location seat
                if (chair.Key != null)
                {
                    var seat = Helper.CreateLocationSeat(locationSeatplan, chair.PosX, chair.PosY, rectangularTable.ChairSize, rectangularTable.ChairSize, chair.Key);
                }
            }

        }
        public static LocationSeatplan CreateLocationSeatplan(string name, string planImagePath, int planWidth, int planHeight, bool locked = false, DateTime? creationDate = null, DateTime? modDate = null)
        {
            var location = new LocationSeatplan
            {               
                Name = name,
                PlanImagePath = planImagePath,
                PlanWidth = planWidth,
                PlanHeight = planHeight,
            };

            return location;
        }

        public static LocationSeat CreateLocationSeat(LocationSeatplan seatplan, int posX, int posY, int width, int height, string identification)
        {
            var seat = seatplan.LocationSeats.SingleOrDefault(ls => ls.SeatIdentification == identification);

            if (seat != null && (seat.PositionX != posX || seat.PositionY != posY))
                throw new Exception("Seat already exists with different coordiantes -> SeadIdentification:" + seat.SeatIdentification + "!");

            if (seat == null)
            {
                seat = new LocationSeat
                {
                    PositionX = posX,
                    PositionY = posY,
                    SeatIdentification = identification,
                    Width = width,
                    Height = height,
                };


                seatplan.LocationSeats.Add(seat);
            }
            return seat;
        }
        public static float GetScaleFactor(float imageWidth, float imageHeight, float docHeight, float docWidth)
        {
            double widthScale = 0, heightScale = 0;
            if (imageWidth != 0)
                widthScale = docWidth / imageWidth;
            if (imageHeight != 0)
                heightScale = docHeight / imageHeight;

            return (float)Math.Min(widthScale, heightScale);
        }
        public static void CreateSeatplanPdf(Document doc, string imageFilePath, LocationSeatplan plan, PdfContentByte pdfContentByte, float scaleFactor, float pageBorderLeft = 0)
        {
            var jpg = Image.GetInstance(imageFilePath);

            //Resize image depend upon your need for give the size to image
            jpg.ScaleToFit(jpg.Width * scaleFactor, jpg.Height * scaleFactor);

            //image as background
            jpg.Alignment = Image.UNDERLYING;


            jpg.SetAbsolutePosition(pageBorderLeft, doc.Top - (jpg.GetTop(0) * scaleFactor) - SpaceForHeader);

            doc.Add(jpg);
            var seatCount = 0;
            foreach (var seat in plan.LocationSeats)
            {
                seatCount++;
                var seatRadius = (seat.Width / 2f) * scaleFactor;
                pdfContentByte.Circle(pageBorderLeft + (seat.PositionX * scaleFactor), doc.Top - (seat.PositionY * scaleFactor) - SpaceForHeader, seatRadius);

                pdfContentByte.SetRGBColorFill(51, 204, 0);
                pdfContentByte.SetRGBColorStroke(51, 204, 0);
                        

                pdfContentByte.FillStroke();

                pdfContentByte.SetRGBColorFill(0, 0, 0);

                // add text to seat & set the right font size
                var fontSizeSeat = seatRadius / 1.8;
                pdfContentByte.BeginText();
                pdfContentByte.SetFontAndSize(_fontBase, (float)fontSizeSeat);

                var seatText = string.Empty;
                            
                foreach (var s in seat.SeatIdentification.Split(' '))
                {
                    if ((s.Contains(',') || seat.SeatIdentification.Split(' ').Last() == s) && s.Length < 5)
                        seatText += s;
                }

                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, seatText, pageBorderLeft + (seat.PositionX * scaleFactor), doc.Top - (seat.PositionY * scaleFactor) - SpaceForHeader - 1, 0);
                pdfContentByte.EndText();
            }


            pdfContentByte.ClosePathFillStroke();

            doc.NewPage();
        }

        
        public static void SavePdfToFileSystem(LocationSeatplan location)
        {
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document(PageSize.A4, 0, 0, 0, 0))
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();

                        var pdfContentByte = writer.DirectContent;
                        var imageFilePath = Directory.GetCurrentDirectory() + "\\..\\..\\" + location.PlanImagePath;
                        
                        var maxHeightForSeatplan = doc.Top - 0 - 0 - 0;
                        var maxWidthForSeatplan = doc.Right - 0 - 0;

                        // get scale factor -> fit to pdf
                        var scale = GetScaleFactor(location.PlanWidth, location.PlanHeight, maxHeightForSeatplan, maxWidthForSeatplan);
                        if (scale > 1)
                            scale = 1;
                        doc.Add(new Paragraph(" "));
                        CreateSeatplanPdf(doc, imageFilePath, location, pdfContentByte, scale);

                        doc.Close();
                    }
                }
                var filename = DateTime.Now.ToString("yyyyMMddhhmmss") + location.Name + "_sheet.pdf";
                File.WriteAllBytes(filename, ms.ToArray());
                System.Diagnostics.Process.Start(filename);
            }
        }
    }
}
