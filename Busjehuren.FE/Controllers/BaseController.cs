using Busjehuren.Core.Enums;
using Busjehuren.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Busjehuren.FE.Models;

namespace Busjehuren.FE.Controllers
{
    public class BaseController : Controller
    {
        public BaseController():base()
        { 
        }

        protected BookingData BookingData
        {
            get
            {
                if (Session[GlobalStatic.BookingData] == null)
                {
                    Session[GlobalStatic.BookingData] = new BookingData();
                }
                return (BookingData)Session[GlobalStatic.BookingData];
            }
            set
            {
                Session[GlobalStatic.BookingData] = value;
            }
        }

        public bool IsPost
        {
            get { return Request.HttpMethod == "POST"; }
        }

        public bool IsGet
        {
            get { return Request.HttpMethod == "GET"; }
        }

        public bool IsNotSearch
        {
            get { return Request.UrlReferrer == null ? true : Request.UrlReferrer.AbsolutePath.Equals("/") || Request.UrlReferrer.AbsolutePath.Equals("/vestigingen"); }
        }
    }
}