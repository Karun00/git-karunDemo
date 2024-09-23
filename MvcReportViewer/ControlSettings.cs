﻿using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Web.UI.WebControls;

namespace MvcReportViewer
{
    /// <summary>
    /// Class represents ReportViewer Control settings.
    /// </summary>
    public class ControlSettings
    {
        /// <summary>
        /// Gets or sets the background color of the control's report area.
        /// </summary>
        [UriParameter("_1")]
        public Color? BackColor { get; set; }

        /// <summary>
        /// Gets or sets the collapsed state of the document map.
        /// </summary>
        [UriParameter("_2")]
        public bool? DocumentMapCollapsed { get; set; }

        /// <summary>
        /// Gets or sets the width of the document map.
        /// </summary>
        [UriParameter("_3")]
        public Unit? DocumentMapWidth { get; set; }

        /// <summary>
        /// Gets or sets the target window or frame for the Web page content that is returned when a hyperlink in the report is clicked.
        /// </summary>
        [UriParameter("_4")]
        public string HyperlinkTarget { get; set; }

        /// <summary>
        /// Gets or sets the internal border color of the control.
        /// </summary>
        [UriParameter("_5")]
        public Color? InternalBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the internal border style of the control.
        /// </summary>
        [UriParameter("_6")]
        public BorderStyle? InternalBorderStyle { get; set; }

        /// <summary>
        /// Gets or sets the width of the internal border of the control.
        /// </summary>
        [UriParameter("_7")]
        public Unit? InternalBorderWidth { get; set; }

        /// <summary>
        /// Gets or sets the color of an active link in the control.
        /// </summary>
        [UriParameter("_8")]
        public Color? LinkActiveColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the active link in the control while the mouse pointer is over the link.
        /// </summary>
        [UriParameter("_9")]
        public Color? LinkActiveHoverColor { get; set; }

        /// <summary>
        /// Gets or sets the color of a disabled link in the control.
        /// </summary>
        [UriParameter("_10")]
        public Color? LinkDisabledColor { get; set; }

        /// <summary>
        /// Gets or sets the collapsed state of the parameter prompt area or the credentials prompt area.
        /// </summary>
        [UriParameter("_11")]
        public bool? PromptAreaCollapsed { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Back button is visible on the toolbar.
        /// </summary>
        [UriParameter("_12")]
        public bool? ShowBackButton { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to display a prompt for user credentials.
        /// </summary>
        [UriParameter("_13")]
        public bool? ShowCredentialPrompts { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the button that shows and collapses the document map is visible on the split bar.
        /// </summary>
        [UriParameter("_14")]
        public bool? ShowDocumentMapButton { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Export control is visible on the toolbar.
        /// </summary>
        [UriParameter("_15")]
        public bool? ShowExportControls { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Find text box is visible on the toolbar.
        /// </summary>
        [UriParameter("_16")]
        public bool? ShowFindControls { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the page navigation controls are visible on the toolbar.
        /// </summary>
        [UriParameter("_17")]
        public bool? ShowPageNavigationControls { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether parameter prompts are displayed.
        /// </summary>
        [UriParameter("_18")]
        public bool? ShowParameterPrompts { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether Print button is visible on the toolbar.
        /// </summary>
        [UriParameter("_19")]
        public bool? ShowPrintButton { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the button that shows and collapses the prompt area is visible on the split bar.
        /// </summary>
        [UriParameter("_20")]
        public bool? ShowPromptAreaButton { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Refresh button is visible.
        /// </summary>
        [UriParameter("_21")]
        public bool? ShowRefreshButton { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the report body is visible on the control.
        /// </summary>
        [UriParameter("_22")]
        public bool? ShowReportBody { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the toolbar is visible on the control.
        /// </summary>
        [UriParameter("_23")]
        public bool? ShowToolBar { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that controls whether to display the Cancel link in the wait control.
        /// </summary>
        [UriParameter("_24")]
        public bool? ShowWaitControlCancelLink { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Zoom list box is visible.
        /// </summary>
        [UriParameter("_25")]
        public bool? ShowZoomControl { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the ReportViewer control should automatically resize to accommodate report content.
        /// </summary>
        [UriParameter("_26")]
        public bool? SizeToReportContent { get; set; }

        /// <summary>
        /// Gets or sets the background color of the document map split bar and the prompt area split bar.
        /// </summary>
        [UriParameter("_27")]
        public Color? SplitterBackColor { get; set; }

        /// <summary>
        /// Gets or sets the border color of an item on the toolbar.
        /// </summary>
        [UriParameter("_28")]
        public Color? ToolBarItemBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the border style of an item on the toolbar.
        /// </summary>
        [UriParameter("_29")]
        public BorderStyle? ToolBarItemBorderStyle { get; set; }

        /// <summary>
        /// Gets or sets the width of the toolbar item border.
        /// </summary>
        [UriParameter("_30")]
        public Unit? ToolBarItemBorderWidth { get; set; }

        /// <summary>
        /// Gets or sets the background color of toolbar item while the mouse pointer is over the item.
        /// </summary>
        [UriParameter("_31")]
        public Color? ToolBarItemHoverBackColor { get; set; }

        /// <summary>
        /// Gets or sets the zoom mode of the control.
        /// </summary>
        [UriParameter("_32")]
        public ZoomMode? ZoomMode { get; set; }

        /// <summary>
        /// Gets or sets the zoom percentage to use when displaying the report.
        /// </summary>
        [UriParameter("_33")]
        public int? ZoomPercent { get; set; }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether to keep the user session from expiring as long as the Web page is displayed in the browser.
        /// By default this property is set to false because of http://stackoverflow.com/questions/18491223/ssrs-why-do-ska-cookies-build-up-until-http-400-bad-request-request-too-long
        /// issue. The issue has been fixed in SQL Server 2012 SP1 CU7.
        /// </summary>
        [UriParameter("_34")]
        public bool? KeepSessionAlive { get; set; }
    }
}
