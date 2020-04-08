﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PhoneTranslator
{
    public static class PhoneTranslator
    {
        public static string ToNumber(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
            {
                return "";
            } else
            {
                raw = raw.ToUpperInvariant();
            }

            var newNumber = new StringBuilder();
            foreach (var c in raw)
            {
                //Wenn - oder Zahlen vorkommen, direkt zur neuen Nummer hinzufügen
                if ("-0123456789".Contains(c))
                {
                    newNumber.Append(c);
                } else
                {
                    //Ansonsten Zeichen in Zahl umwandeln und zur Nummer hinzufügen
                    var result = TranslateToNumber(c);
                    if (result != null) {
                        newNumber.Append(result);
                    }
                }
            }
            return newNumber.ToString();
        }
        static bool Contains (this string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }

        static int? TranslateToNumber(char c)
        {
            if ("ABC".Contains(c))
            {
                return 2;
            } else if ("DEF".Contains(c))
            {
                return 3;
            } else if ("GHI".Contains(c))
            {
                return 4;
            } else if ("JKL".Contains(c))
            {
                return 5;
            } else if ("MNO".Contains(c))
            {
                return 6;
            } else if ("PQRS".Contains(c))
            {
                return 7;
            } else if ("TUV".Contains(c))
            {
                return 8;
            } else if ("WXYZ".Contains(c))
            {
                return 9;
            }
            return null;
        }
    }
}