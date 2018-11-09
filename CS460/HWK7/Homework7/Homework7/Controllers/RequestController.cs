using Homework7.DAL;
using Homework7.Models;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Homework7.Controllers
{
    public class RequestController : Controller
    {
        /* Initialize the list we'll store user's forms in */
        private SearchRequestLogContext requestLogDatabase = new SearchRequestLogContext();

        /// <summary>
        /// Assembles a GET request using the client's search term, an API key, and GIHPY's Sticker 
        /// API Translate endpoint, and sends the request to GIPHY.
        /// </summary>
        /// <param name="lastWord">The client's search term.</param>
        /// <returns>A JSON object containing the data about the request.</returns>
        [HttpGet]
        public JsonResult TranslateGIF(string lastWord)
        {
            /* Get the API Key for making the request */
            string superSecretKey = System.Web.Configuration.WebConfigurationManager.AppSettings["APIKEY"];

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

            /* Create a new entry in the database */
            SearchRequestLog requestLogDB = requestLogDatabase.RequestLogs.Create();

            /* Get the time of the request */
            requestLogDB.RequestTimestamp = DateTime.Now;

            /* Get what the client searched for */
            requestLogDB.RequestType = Request.Url.OriginalString;

            /* Get the IP address of the client */
            requestLogDB.ClientIP = Request.UserHostAddress;

            /* Get the client's browser */
            requestLogDB.BrowserAgent = Request.Browser.Type;

            /* Add the search request to the database and save the changes */
            requestLogDatabase.RequestLogs.Add(requestLogDB);
            requestLogDatabase.SaveChanges();

            /* Close the streams */
            data.Close();
            getReponse.Close();

            /* Returned the parsed object back to the client */
            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }
    }
}