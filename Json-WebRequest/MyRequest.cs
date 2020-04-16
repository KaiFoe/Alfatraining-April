using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Foundation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UIKit;

namespace Json_WebRequest
{
    public class MyRequest
    {

        public MyRequest() { }

        public string getRequest(string count, string mode)
        {
            var request = WebRequest.Create(string.Format(@"https://www.codeyourapp.de/tools/query.php?count={0}&mode={1}", count, mode));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    return "Error fetching data. Server returns status code: " + response.StatusCode;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                        return "Response contained empty body";
                    else
                    {
                        return content;
                    }
                }
            }
        }

        public List<Quote> jsonGetQuotes(string count, string mode)
        {
            List<Quote> quotesList = new List<Quote>();
            try
            {
                var content = getRequest(count, mode);
                JObject quoteObject = (JObject)JsonConvert.DeserializeObject(content);

                var resultObjects = AllChildren(JObject.Parse(content))
                    .First(c => c.Type == JTokenType.Array && c.Path.Contains("quotes"))
                    .Children<JObject>();

                foreach (JObject result in resultObjects)
                {
                    Quote newQuote = result.ToObject<Quote>();
                    quotesList.Add(newQuote);
                }
                return quotesList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                    yield return cc;
            }
        }
    }
}