using Busjehuren.Common.Utils;
using Busjehuren.Core.Enums;
using Busjehuren.Core.Services.Contract;
using Busjehuren.FE.App_Start;
using Busjehuren.FE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Busjehuren.FE.DropDownLists
{
    public static class DropDownList
    {
        public static List<SelectListItem> LandBestemmings()
        {
            IContentService _contentService = NinjectExt.Default.Get<IContentService>();

            return _contentService.LandBestemmings(GlobalStatic.DropdownsLandBestemming, GlobalStatic.NetherLandPositionText);
        }

        public static IEnumerable<SelectListItem> GetGenders()
        {
            var result = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(v => new SelectListItem
            {
                Text = EnumUtils<Gender>.GetDescription(v),
                Value = v.ToString()
            }).ToList();

            return result;
        }
    }
}