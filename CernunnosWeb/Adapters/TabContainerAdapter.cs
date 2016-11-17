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
    public class TabContainerAdapter : System.Web.UI.WebControls.Adapters.WebControlAdapter
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TabContainerAdapter()
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
        /// Render the HTML for the contents of the this.Control.
        /// </summary>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            TabContainer container = (TabContainer)Control;

            this.RenderHeader(writer);

            if (!container.Height.IsEmpty)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, container.Height.ToString());
            }
            else
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Id, container.ClientID + "_body");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ajax__tab_body");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            this.RenderChildren(writer);
            writer.RenderEndTag();
        }

        /// <summary>
        /// Render header part of the TabContainer.
        /// </summary>
        /// <param name="writer">HtmlTextWriter object.</param>
        protected virtual void RenderHeader(HtmlTextWriter writer)
        {
            TabContainer container = (TabContainer)Control;

            writer.WriteBeginTag("ul");
            writer.WriteAttribute("ID", container.ClientID + "_header");
            writer.WriteAttribute("class", "nav nav-tabs");
            writer.Write(HtmlTextWriter.TagRightChar);

            var tabs = container.Tabs.Count;
            for (int i = 0; i < container.Tabs.Count; i++)
            {
                var panel = container.Tabs[i];
                if (panel.Visible)
                {
                    this.RenderTabHeader(writer, container.Tabs[i], i);
                }
            }

            writer.WriteEndTag("ul");
        }

        /// <summary>
        /// Render header part of the TabContainer.
        /// </summary>
        /// <param name="writer">HtmlTextWriter object.</param>
        /// <param name="tab">TabPanel object.</param>
        /// <param name="index">TabPanel index.</param>
        private void RenderTabHeader(HtmlTextWriter writer, TabPanel tab, int index)
        {
            writer.Indent--;
            writer.WriteLine();
            writer.WriteBeginTag("li");

            if (((TabContainer)Control).ActiveTabIndex == index)
            {
                writer.WriteAttribute("class", "active");
            }

            writer.Write(HtmlTextWriter.TagRightChar);

            if (tab.Enabled)
            {
                writer.WriteBeginTag("a");
                writer.WriteAttribute("ID", "__tab_" + tab.ClientID);
                writer.WriteAttribute("href", "#");
                writer.WriteAttribute("data-toggle", "tab");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write(tab.HeaderText);
                writer.WriteEndTag("a");
            }
            else
            {
                writer.WriteBeginTag("a");
                writer.WriteAttribute("ID", "__tab_" + tab.ClientID);
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Write(tab.HeaderText);
                writer.WriteEndTag("a");
            }

            writer.WriteEndTag("li");
        }
    }
}