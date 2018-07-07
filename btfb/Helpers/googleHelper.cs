using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace btfb.Helpers
{
    public class googleHelper
    {
        public string origin { get; set; }
        public string destination { get; set; }
        public googleHelper(string from, string to)
        {
            origin = from;
            destination = to;
        }
        public string CalculateDistance()
        {
            // Build your request to the API
            var request = WebRequest.Create("https://maps.googleapis.com/maps/api/distancematrix/json?origins="+origin+"&destinations="+destination);
            // Indicate you are looking for a JSON response
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse)request.GetResponse();

            // Read through the response
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                // Define a serializer to read your response
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                // Get your results
                dynamic result = serializer.DeserializeObject(sr.ReadToEnd());

                // Read the distance property from the JSON request
                var distance = result["rows"][0]["elements"][0]["distance"]["text"]; // yields "1,300 KM"
                return distance.ToString();
            }
        }
    }
}