using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Busjehuren.Core.Models;

namespace Busjehuren.FE.Models
{
    public class TimeVM
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}