﻿using System;

namespace Busjehuren.Core.Models
{
    public class EmailModel
    {
        public string From { get; set; }
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        
    }
}
