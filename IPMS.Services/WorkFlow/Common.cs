using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPMS.Repository;
using System.Web.Script.Serialization;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
using IPMS.Domain;

namespace IPMS.Services.WorkFlow
{
    public class Common
    {
        public static string GetTokensDictionaryForReferenceData(Entity entity, object T)
        {
            Dictionary<string, string> tokensDict = new Dictionary<string, string>();
            var js = new JavaScriptSerializer();
            var colname = string.Empty;
            string[] arrayOfTokensForEntity = entity.Tokens.Split(',');
            foreach (string _attribute in arrayOfTokensForEntity)
            {
                object value;
                bool x = GetValue(T, _attribute, out value, new HashSet<object>());
             // For Pilot Exemption Request pending tasks ID NO is adding '.' to showing as ID NO. 
                if (_attribute == "IDNo")
                {
                    colname = "IDNo.";
                }
                else
                {
                    colname = _attribute;
                }
                tokensDict.Add(colname, Convert.ToString(value, CultureInfo.InvariantCulture));
            }
            return js.Serialize(tokensDict);
        }
        public static Dictionary<string, string> GetTokensDictionary(Entity entity, object T)
        {
            Dictionary<string, string> tokensDict = new Dictionary<string, string>();
            string[] arrayOfTokensForEntity = entity.Tokens.Split(',');
            foreach (string _attribute in arrayOfTokensForEntity)
            {
                object value;
                bool x = GetValue(T, _attribute, out value, new HashSet<object>());
                if (value == null)
                {
                    x = GetValue(((object)entity), _attribute, out value, new HashSet<object>());
                }
                tokensDict.Add("%" + _attribute + "%", Convert.ToString(value, CultureInfo.InvariantCulture));
            }
            return tokensDict;
        }



        public static bool GetValue(object currentObject, string propName, out object value, HashSet<object> searchedObjects)
        {
            try
            {
                if (currentObject == null)
                {
                    value = null;
                    return false;
                }
                else
                {
                    PropertyInfo propInfo = currentObject.GetType().GetProperty(propName);
                    if (propInfo != null)
                    {
                        value = propInfo.GetValue(currentObject, null);

                        if (value != null) //If value not found
                        {
                            string[] format = new string[] { GlobalConstants.DateTimeFormatforToken };
                            DateTime datetime;
                            if (DateTime.TryParseExact(value.ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out datetime))
                            {
                                DateTime datevalue = Convert.ToDateTime(value.ToString(),CultureInfo.InvariantCulture);
                                value = datevalue.ToString(GlobalConstants.IPMSDateTimeFormat, CultureInfo.InvariantCulture);
                            }
                        }
                        return true;
                    }
                    // search child properties
                    foreach (PropertyInfo propInfo2 in currentObject.GetType().GetProperties())
                    {   // ignore indexed properties
                        if (propInfo2.GetIndexParameters().Length == 0)
                        {
                            object newObject = propInfo2.GetValue(currentObject, null);
                            if (newObject != null && searchedObjects.Add(newObject) &&
                                GetValue(newObject, propName, out value, searchedObjects))
                            {
                                if (value != null)
                                {
                                    string[] format = new string[] { GlobalConstants.DateTimeFormatforToken };
                                    DateTime datetime;
                                    if (DateTime.TryParseExact(value.ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out datetime))
                                    {
                                        DateTime datevalue = Convert.ToDateTime(value.ToString(), CultureInfo.InvariantCulture);
                                        value = datevalue.ToString(GlobalConstants.IPMSDateTimeFormat, CultureInfo.InvariantCulture);
                                    }
                                }
                                return true;
                            }
                        }
                    }
                    // property not found here
                    value = null;
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

}
