using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Homework7.Controllers
{
    public class MainController : Controller
    {
        /// <summary>
        /// Default method. Only called when the application first starts, or
        /// if the page is refreshed.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Assembles a GET request using the client's search term, an API key, and GIHPY's Sticker 
        /// API Translate endpoint, and sends the request to GIPHY.
        /// </summary>
        /// <param name="lastWord">The client's search term.</param>
        /// <returns>A JSON object containing the data about the request.</returns>
        [HttpGet]
        public JsonResult TranslateGIF(string lastWord)
        {
            /* TODO -- HIDE THIS SOMEWHERE */
            string superSecretKey = "";

            /* Build the URL with GIPHY's Translate Endpoint and the client's last word typed */
            string giphyURL = "http://api.giphy.com/v1/stickers/translate?api_key=" + superSecretKey + "&s=" + lastWord;

            /* Create a request using the constructed URL */
            WebRequest sendRequest = WebRequest.Create(giphyURL);

            /* Grab the response from the above request */
            WebResponse getReponse = sendRequest.GetResponse();

            /* Convert the response so we can read it */
            Stream data = sendRequest.GetResponse().GetResponseStream();

            /* Convert the response to a string so we can do stuff with it */
            string convertResponse = new StreamReader(data).ReadToEnd();

            /* Parse the JSON returned from GIPHY's Translate endpoint */
            var serialize = new System.Web.Script.Serialization.JavaScriptSerializer();
            var jsonResponse = serialize.DeserializeObject(convertResponse);

            /* Close the streams */
            data.Close();
            getReponse.Close();

            /* Returned the parsed object back to the client */
            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }

    }
}