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
            if (firstColor != null || secondColor != null)
            {
                /* COnvert the strings into Color objects so we can add them together */
                Color rgbColorOne = ColorTranslator.FromHtml(firstColor);
                Color rgbColorTwo = ColorTranslator.FromHtml(secondColor);

                /* Add each channel from the two colors, clamping the ranges from 0 to 255 */
                int finalColorAlpha = Math.Min(Math.Max(rgbColorOne.A + rgbColorTwo.A, 0), 255);
                int finalColorRed = Math.Min(Math.Max(rgbColorOne.R + rgbColorTwo.R, 0), 255);
                int finalColorGreen = Math.Min(Math.Max(rgbColorOne.G + rgbColorTwo.G, 0), 255);
                int finalColorBlue = Math.Min(Math.Max(rgbColorOne.B + rgbColorTwo.B, 0), 255);

                /* Construct the mixture using the channels we just calculated */
                string finalColor = ColorTranslator.ToHtml(Color.FromArgb(finalColorAlpha, finalColorRed, finalColorGreen, finalColorBlue));

                /* Set the background information for each of the boxes */
                ViewBag.FirstColorBox = "background: " + firstColor + ";";
                ViewBag.SecondColorBox = "background: " + secondColor + ";";
                ViewBag.FinalColorBox = "background: " + finalColor + ";";

                /* Change the flag to show the boxes */
                ViewBag.ShowBoxes = true;
            }

            /* Return the view, with or without the boxes visible */
            return View();
        }
    }
}