using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Json_WebRequest
{
    public class Quote
    {
        public string id { get; set; }
        public string author { get; set; }
        public string text { get; set; }

        public Quote() { }

        public Quote(string id, string author, string text)
        {
            this.id = id;
            this.author = author;
            this.text = text;
        }
    }
}