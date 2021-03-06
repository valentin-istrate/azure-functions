﻿namespace Demo.GreetingDurableFunction.Extensions
{
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class UriExtensions
    {
        public static string TryGetQueryValue(this Uri uri, string name, string defaultValue = "")
        {
            Dictionary<string, StringValues> queryDictionary = QueryHelpers.ParseQuery(uri.Query);
            string resultValue;

            if (queryDictionary.TryGetValue(name, out StringValues values) && values.Any())
            {
                resultValue = values.First();
            }
            else
            {
                resultValue = defaultValue;
            }

            return resultValue;
        }
    }
}