using System.Web.Mvc;
using System.Drawing;
using System;

namespace Homework4.Controllers
{
    public class ColorController : Controller
    {
        /// <summary>
        /// GET for Converter
        /// </summary>
        /// <returns>The view - ColorMixer.cshtml</returns>
        public ActionResult ColorMixer()
        {
            /* Set the flag to show the boxes to false initially */
            ViewBag.ShowBoxes = false;

            /* Grab the color codes from the text boxes */
            string firstColor = Request.Form["firstColor"];
            string secondColor = Request.Form["secondColor"];

            /* Make sure the fields aren't null before proceeding */
            if (firstColor != null && secondColor != null)
            {
                /* COnvert the strings into Color objects so we can add them together */
                Color rgbColorOne = ColorTranslator.FromHtml(firstColor);
                Color rgbColorTwo = ColorTranslator.FromHtml(secondColor);

                /* Add each channel from the two colors, clamping the range from 0 to 255 */
                int finalColorAlpha = Math.Max(Math.Min(rgbColorOne.A + rgbColorTwo.A, 255), 0);
                int finalColorRed = Math.Max(Math.Min(rgbColorOne.R + rgbColorTwo.R, 255), 0);
                int finalColorGreen = Math.Max(Math.Min(rgbColorOne.G + rgbColorTwo.G, 255), 0);
                int finalColorBlue = Math.Max(Math.Min(rgbColorOne.B + rgbColorTwo.B, 255), 0);

                /* Construct the mixture using the channels we just calculated */
                string finalColor = ColorTranslator.ToHtml(Color.FromArgb(finalColorAlpha, finalColorRed, finalColorGreen, finalColorBlue));

                /* Set the style information for each of the boxes */
                ViewBag.FirstColor = "width:160px; height: 160px; border: 2px solid #000000; display: inline-block; background: " + firstColor + "; ";
                ViewBag.SecondColor = "width:160px; height: 160px; border: 2px solid #000000; display: inline-block; background: " + secondColor + "; ";
                ViewBag.FinalColor = "width:160px; height: 160px; border: 2px solid #000000;  display: inline-block;background: " + finalColor + "; ";

                /* Change the flag to show the boxes */
                ViewBag.ShowBoxes = true;
            }


            return View();
        }
    }
}