﻿using System;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.Reporting.WebForms;

namespace MvcReportViewer
{
    internal class ReportViewerParametersParser
    {
        public ReportViewerParameters Parse(NameValueCollection queryString)
        {
            if (queryString == null)
            {
                throw new ArgumentNullException("queryString");
            }

            var settinsManager = new ControlSettingsManager();

            var parameters = InitializeDefaults();
            ResetDefaultCredentials(queryString, parameters);
            parameters.ControlSettings = settinsManager.Deserialize(queryString);

            foreach (var key in queryString.AllKeys)
            {
                if (key.EqualsIgnoreCase(UriParameters.ReportPath))
                {
                    parameters.ReportPath = queryString[key];
                }
                else if (key.EqualsIgnoreCase(UriParameters.ReportServerUrl))
                {
                    parameters.ReportServerUrl = queryString[key];
                }
                else if (key.EqualsIgnoreCase(UriParameters.Username))
                {
                    parameters.Username = queryString[key];
                }
                else if (key.EqualsIgnoreCase(UriParameters.Password))
                {
                    parameters.Password = queryString[key];
                }
                else if (!settinsManager.IsControlSetting(key))
                {
                    var values = queryString.GetValues(key);
                    if (values != null)
                    {
                        foreach (var value in values)
                        {
                            if (parameters.ReportParameters.ContainsKey(key))
                            {
                                parameters.ReportParameters[key].Values.Add(value);
                            }
                            else
                            {
                                var reportParameter = new ReportParameter(key);
                                reportParameter.Values.Add(value);
                                parameters.ReportParameters.Add(key, reportParameter);
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(parameters.ReportServerUrl))
            {
                throw new MvcReportViewerException("Report Server is not specified.");
            }

            if (string.IsNullOrEmpty(parameters.ReportPath))
            {
                throw new MvcReportViewerException("Report is not specified.");
            }

            return parameters;
        }

        private static void ResetDefaultCredentials(NameValueCollection queryString, ReportViewerParameters parameters)
        {
            if (queryString.ContainsKeyIgnoreCase(UriParameters.Username) ||
                queryString.ContainsKeyIgnoreCase(UriParameters.Password))
            {
                parameters.Username = string.Empty;
                parameters.Password = string.Empty;
            }
        }

        private ReportViewerParameters InitializeDefaults()
        {
            var isAzureSSRS = ConfigurationManager.AppSettings[WebConfigSettings.IsAzureSSRS];
            bool isAzureSSRSValue;
            if (string.IsNullOrEmpty(isAzureSSRS) || !bool.TryParse(isAzureSSRS, out isAzureSSRSValue))
            {
                isAzureSSRSValue = false;
            }
            
            var parameters = new ReportViewerParameters
                {
                    ReportServerUrl = ConfigurationManager.AppSettings[WebConfigSettings.Server],
                    Username = ConfigurationManager.AppSettings[WebConfigSettings.Username],
                    Password = ConfigurationManager.AppSettings[WebConfigSettings.Password],
                    IsAzureSSRS = isAzureSSRSValue
                };

            return parameters;
        }
    }
}
