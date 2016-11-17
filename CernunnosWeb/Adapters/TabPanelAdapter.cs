using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace CernunnosWeb.Adapters
{
    /// <summary>
    /// This CSS Adapter is used to control the rendering of the ASP.NET TextBox this.Control.
    /// By default the control renders as a series of embedded tables and is difficult to apply
    /// CSS styles to and not compliant with NZ Web Guidelines.
    /// This adapter renders the control using unordered lists, ul.
    /// </summary>
    public class TabPanelAdapter : System.Web.UI.WebControls.Adapters.WebControlAdapter
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TabPanelAdapter()
        {
        }

        /// <summary>
        /// Initialise control.
        /// </summary>
        /// <param name="e">Contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// Render the HTML for the begin of the Control.
        /// </summary>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        protected override void RenderBeginTag(HtmlTextWriter writer)
        {
            base.RenderBeginTag(writer);
        }

        /// <summary>
        /// Generates the target-specific markup for the control to which the control adapter is attached.
        /// </summary>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            this.RenderBeginTag(writer);

            this.RenderContents(writer);

            this.RenderEndTag(writer);
        }
    }
}