using System.Drawing;
using System.Web.Mvc;
using GdPicture14;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Diagnostics;

namespace aspnet_mvc_razor_app.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => RedirectToAction(nameof(Reproduce));

        public ActionResult Reproduce()
        {
            var timeProcessOfSelectPageMethodsInMilliseconds = new List<long>();
            var timeProcessOfAddEmbeddedImageAnnotMethodsInMilliseconds = new List<long>();

            var filePath = Request.MapPath("~/TestFiles/30pages.tif");
            using (var annotationManager = new AnnotationManager())
            {
                GdPictureStatus status = annotationManager.InitFromFile(filePath);

                var imaging = new GdPictureImaging();

                int imageId = imaging.CreateGdPictureImageFromFile(filePath);
                var bitmaps = new List<Bitmap>();
                for (int pgIdx = 1; pgIdx <= imaging.GetPageCount(imageId); pgIdx++)
                {
                    imaging.SelectPage(imageId, pgIdx);

                    var w = imaging.GetWidth(imageId);
                    var h = imaging.GetHeight(imageId);

                    var bitmap = new Bitmap(w, h, PixelFormat.Format32bppArgb);

                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        for (int y = 0; y < bitmap.Height; y++)
                        {
                            var color = x % 2 == 0 ? Color.Red : Color.Green;
                            bitmap.SetPixel(x, y, color);
                        }
                    }

                    bitmaps.Add(bitmap);
                }

                imaging.ReleaseGdPictureImage(imageId);

                int pageCount = annotationManager.PageCount;

                for (int page = 1; page <= pageCount; page++)
                {
                    // Stop watch to show time was used by SelectedPage method
                    var sw = new Stopwatch();
                    sw.Start();
                    status = annotationManager.SelectPage(page);
                    sw.Stop();

                    timeProcessOfSelectPageMethodsInMilliseconds.Add(sw.ElapsedMilliseconds);
                    // End of stop watch of current page

                    var crrAnn = bitmaps[page - 1];

                    sw.Restart();
                    annotationManager.AddEmbeddedImageAnnot(crrAnn, 0f, 0f, crrAnn.Width * 1f, crrAnn.Height * 1f);
                    sw.Stop();

                    timeProcessOfAddEmbeddedImageAnnotMethodsInMilliseconds.Add(sw.ElapsedMilliseconds);
                }

                return View(timeProcessOfSelectPageMethodsInMilliseconds);
            }

        }
    }
}