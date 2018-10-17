using System.Web.Mvc;
using System.Drawing;
using System;

namespace Homework4.Controllers
{
    public class ColorController : Controller
    {
        // GET: Color Mixer
        public ActionResult ColorMixer()
        {

            string firstColor = Request.Form["firstColor"];
            string secondColor = Request.Form["secondColor"];

            Color rgbColorOne = ColorTranslator.FromHtml(firstColor);
            Color rgbColorTwo = ColorTranslator.FromHtml(secondColor);

            int finalColorAlpha = Math.Max(Math.Min(rgbColorOne.A + rgbColorTwo.A, 255), 0);
            int finalColorRed = Math.Max(Math.Min(rgbColorOne.R + rgbColorTwo.R, 255), 0);
            int finalColorGreen = Math.Max(Math.Min(rgbColorOne.G + rgbColorTwo.G, 255), 0);
            int finalColorBlue = Math.Max(Math.Min(rgbColorOne.B + rgbColorTwo.B, 255), 0);

            Console.WriteLine(finalColorAlpha);
            Console.WriteLine(finalColorRed);
            Console.WriteLine(finalColorGreen);
            Console.WriteLine(finalColorBlue);

            string finalColor = ColorTranslator.ToHtml(Color.FromArgb(finalColorAlpha, finalColorRed, finalColorGreen, finalColorBlue));

            ViewBag.FirstColor = "width:160px; height: 160px; border: 2px solid #000000; display: inline-block; background: " + firstColor + "; ";
            ViewBag.SecondColor = "width:160px; height: 160px; border: 2px solid #000000; display: inline-block; background: " + secondColor + "; ";
            ViewBag.FinalColor = "width:160px; height: 160px; border: 2px solid #000000;  display: inline-block;background: " + finalColor + "; ";

            return View();
        }
    }
}