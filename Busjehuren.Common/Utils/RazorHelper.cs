using RazorEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Common.Utils
{
    public static class RazorHelper
    {
        public static string Parse<T>(string path, T model)
        {
            string fullPath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, path);
            string template = File.ReadAllText(fullPath);

            return ParseTemplate(template, model);
        }

        public static string ParseTemplate<T>(string template, T model)
        {
            try
            {
                return Razor.Parse(template, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
