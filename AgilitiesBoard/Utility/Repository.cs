using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgilitiesBoard.Utility
{
    public class Repository : IRepository
    {

        public List<T> GetList<T> (string key)
        {
            HttpContext currentContext = GetSessionContext();
            return (List<T>)currentContext.Session[key];
        }

        public void SetList<T>(string key, List<T> entry)
        {
            HttpContext currentContext = GetSessionContext();
            currentContext.Session[key] = entry;
        }

        private static HttpContext GetSessionContext()
        {
            HttpContext currentContext = HttpContext.Current;

            if (currentContext == null)
            {
                throw new InvalidOperationException();
            }
            return currentContext;
        }
    }
}