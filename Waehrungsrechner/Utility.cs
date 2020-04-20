using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Foundation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UIKit;

namespace Waehrungsrechner
{
    class Utility
    {
        static string key = "4177|FQXa^sUYUow_6yWNBDWFyckfh1sxfCY3";

        public Utility() { }

        public string getRequest(string waehrung)
        {
            var request = WebRequest.Create(string.Format(@"http://api.wahrungsrechner.org/v1/full/{0}/json?key={1}", waehrung, key));
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
        
        public List<Conversion> jsonGetQuotes(string waehrung)
        {
            List<Conversion> conversionList = new List<Conversion>();
            try
            {
                var content = getRequest(waehrung);
                JObject quoteObject = (JObject)JsonConvert.DeserializeObject(content);

                var resultObjects = AllChildren(JObject.Parse(content))
                    .First(c => c.Type == JTokenType.Array && c.Path.Contains("conversion"))
                    .Children<JObject>();

                foreach (JObject result in resultObjects)
                {
                    Conversion newConversion = result.ToObject<Conversion>();
                    conversionList.Add(newConversion);
                }
                return conversionList;
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

        /*
        public void writeQuotesToJson(List<Quote> quotesList)
        {
            //TaskListe in Json-String formatieren
            string json = JsonConvert.SerializeObject(quotesList, Formatting.Indented);
            //Dateipfad und -name festlegen
            var saveFile = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Quotes.json");

            //DEBUG-Ausgabe
            Console.WriteLine(saveFile);

            //Streamwriter um json-String in Datei zu schreiben
            using (var writer = File.CreateText(saveFile))
            {
                writer.Write(json);
            }

            File.WriteAllText(saveFile, json);
        }*/
    }
}