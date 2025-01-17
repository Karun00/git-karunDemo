﻿using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace MvcReportViewer
{
    public interface IMvcReportViewerOptions : IHtmlString
    {
        /// <summary>
        /// Sets the path to the report on the server.
        /// </summary>
        /// <param name="reportPath">The path to the report on the server.</param>
        /// <returns>An instance of MvcViewerOptions class.</returns>
        IMvcReportViewerOptions ReportPath(string reportPath);

        /// <summary>
        /// Sets the URL for the report server.
        /// </summary>
        /// <param name="reportServerUrl">The URL for the report server.</param>
        /// <returns>An instance of MvcViewerOptions class.</returns>
        IMvcReportViewerOptions ReportServerUrl(string reportServerUrl);

        /// <summary>
        /// Sets the report server username.
        /// </summary>
        /// <param name="username">The report server username.</param>
        /// <returns>An instance of MvcViewerOptions class.</returns>
        IMvcReportViewerOptions Username(string username);

        /// <summary>
        /// Sets the report server password.
        /// </summary>
        /// <param name="password">The report server password.</param>
        /// <returns>An instance of MvcViewerOptions class.</returns>
        IMvcReportViewerOptions Password(string password);

        /// <summary>
        /// Sets the report parameter properties for the report.
        /// </summary>
        /// <param name="reportParameters">The report parameter properties for the report.</param>
        /// <returns>An instance of MvcViewerOptions class.</returns>
        IMvcReportViewerOptions ReportParameters(object reportParameters);

        /// <summary>
        /// Sets the report parameter properties for the report.
        /// </summary>
        /// <param name="reportParameters">The report parameter properties for the report.</param>
        /// <returns>An instance of MvcViewerOptions class.</returns>
        IMvcReportViewerOptions ReportParameters(IEnumerable<KeyValuePair<string, object>> reportParameters);

        /// <summary>
        /// Sets an object that contains the HTML attributes to set for the element.
        /// </summary>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An instance of MvcViewerOptions class.</returns>
        IMvcReportViewerOptions Attributes(object htmlAttributes);

        /// <summary>
        /// Sets the method for sending parameters to the iframe, either GET or POST.
        /// POST should be used to send long arguments, etc. Use GET otherwise.
        /// </summary>
        /// <param name="method">The HTTP method for sending parametes to the iframe, either GET or POST.</param>
        /// <returns>An instance of MvcViewerOptions class.</returns>
        IMvcReportViewerOptions Method(FormMethod method);

        /// <summary>
        /// Sets ReportViewer control UI parameters
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>An instance of MvcViewerOptions class.</returns>
        IMvcReportViewerOptions ControlSettings(ControlSettings settings);
    }
}
