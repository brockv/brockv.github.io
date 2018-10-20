using System.Web.Mvc;
using System.Drawing;
using System;

namespace Homework4.Controllers
{
    public class ColorController : Controller
    {
        /// <summary>
        /// GET for ColorMixer
        /// </summary>
        /// <returns>The view - ColorMixer.cshtml</returns>
        public ActionResult ColorMixer()
        {
            /* Set the flag to show the boxes to false initially */
            ViewBag.ShowBoxes = false;

            /* Return the view */
            return View();
        }

        /// <summary>
        /// POST for ColorMixer. Gets the values from the textboxes and generates a new
        /// color by adding the ARGB color channels together.
        /// </summary>
        /// <param name="firstColor">The color code from the first textbox.</param>
        /// <param name="secondColor">The color code from the second textbox.</param>
        /// <returns>The view - ColorMixer.cshtml - with the bool flag in the appropriate state.</returns>
        [HttpPost]
        public ActionResult ColorMixer(string firstColor, string secondColor)
        {
            /* Set the flag to show the boxes to false initially */
            ViewBag.ShowBoxes = false;

            /* Make sure the fields aren't null before proceeding */
            if (firstColor != null && secondColor != null)
            {
                /* Convert the strings into Color objects so we can add them together */
                Color rgbColorOne = ColorTranslator.FromHtml(firstColor);
                Color rgbColorTwo = ColorTranslator.FromHtml(secondColor);

                /* Add each channel together from the two colors */
                int finalColorRed = rgbColorOne.R + rgbColorTwo.R;
                int finalColorGreen = rgbColorOne.G + rgbColorTwo.G;
                int finalColorBlue = rgbColorOne.B + rgbColorTwo.B;

                /* 
                 * Subtract 255 from any values greater than 255, effectively 
                 * wrapping around from 0. This allows for more color combinations
                 * that don't result in plain white.
                 */
                if (finalColorRed > 255) finalColorRed -= 255;
                if (finalColorGreen > 255) finalColorGreen -= 255;
                if (finalColorBlue > 255) finalColorBlue -= 255;

                /* Construct the mixture using the channels we just calculated */
                string finalColor = ColorTranslator.ToHtml(Color.FromArgb(finalColorRed, finalColorGreen, finalColorBlue));

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