using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPMSFeedService.Models
{
    public static class GlobalConstants
    {
        public const string SupCat_BerthType = "BETY";
        public const string AGENT = "AGNT";
        public const string DateFormat = "dd-MM-yyyy";
        public const string IPMSDateFormat = "yyyy-MM-dd";
        public const string IPMSTimeFormat = "HH:mm";

        public const string DateTimeFormatforToken = "M/d/yyyy h:m:s tt";
        public const string DateTimeFormatWith24Hour = "yyyy-MM-dd HH:mm";
        public const string IPMSDateTimeFormat = "yyyy/MM/dd HH:mm";

        public const string Validateparameters = "[\"<>#]";
        public const string ValidateparametersForPost = "[<>]";
        public const string InvalidInput = "Invalid Input";
        public const string InternalServerErrorMessage = "Internal Server error occured. Please contact to administrator.";

    }
}