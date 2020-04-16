using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Json_WebRequest
{
    public class MyRequest
    {

        public MyRequest() { }

        public void getRequest(string rxcui)
        {
            
            var request = WebRequest.Create(string.Format(@"https://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/{0}/allinfo", rxcui));

        }
    }
}