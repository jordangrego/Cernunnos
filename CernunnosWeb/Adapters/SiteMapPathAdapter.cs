using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CernunnosWeb.Adapters
{
    /// <summary>
    /// This CSS Adapter is used to control the rendering of the ASP.NET SiteMapPath this.Control.
    /// By default the control renders as a series of embedded tables and is difficult to apply
    /// CSS styles to and not compliant with NZ Web Guidelines.
    /// This adapter renders the control using unordered lists, ul.
    /// </summary>
    public class SiteMapPathAdapter : System.Web.UI.WebControls.Adapters.WebControlAdapter
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SiteMapPathAdapter()
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
            writer.WriteLine();
            writer.WriteBeginTag("ol");

            if (!this.Control.CssClass.Equals(string.Empty))
            {
                writer.WriteAttribute("class", this.Control.CssClass);
            }

            writer.Write(HtmlTextWriter.TagRightChar);
        }

        /// <summary>
        /// Render the HTML for the end of the Control.
        /// </summary>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        protected override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.WriteEndTag("ol");

            if ((this.Control != null) && (this.Control.Attributes["CssSelectorClass"] != null) && (this.Control.Attributes["CssSelectorClass"].Length > 0))
            {
                writer.Indent--;
                writer.WriteLine();
                writer.WriteEndTag("ol");
            }

            writer.WriteLine();
        }

        /// <summary>
        /// Render the HTML for the contents of the this.Control.
        /// </summary>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Indent++;
            SiteMapPath item = (SiteMapPath)Control;

            SiteMapProvider provider = ((System.Web.UI.WebControls.SiteMapPath)Control).Provider;
            SiteMapNodeCollection collection = new SiteMapNodeCollection();
            SiteMapNode node = provider.CurrentNode;

            if (node != null)
            {
                collection.Add(node);
                while (node != provider.CurrentNode.RootNode)
                {
                    node = node.ParentNode;
                    collection.Add(node);
                }
            }

            this.BuildItems(collection, true, writer);

            writer.Indent--;
            writer.WriteLine();
        }

        /// <summary>
        /// Render the HTML for each set of breadcrumb items. 
        /// </summary>
        /// <param name="items">Menu items.</param>
        /// <param name="isRoot">Is root menu.</param>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        private void BuildItems(SiteMapNodeCollection items, bool isRoot, HtmlTextWriter writer)
        {
            if (items.Count > 0)
            {
                for (int i = items.Count - 1; i > -1; i--)
                {
                    // Write href links for item, if item is not currentNode
                    if (i != 0)
                    {
                        this.BuildItem(items[i], writer);
                    }
                    else
                    {
                        // if node is currentNode just print the title
                        writer.WriteLine();
                        writer.WriteBeginTag("li");
                        writer.WriteAttribute("class", "active");
                        writer.Write(HtmlTextWriter.TagRightChar);
                        writer.Write(items[i].Title);
                        writer.WriteEndTag("li");
                    }
                }
            }
        }

        /// <summary>
        /// Render each breadcrumb path.
        /// </summary>
        /// <param name="item">Menu item.</param>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        private void BuildItem(SiteMapNode item, HtmlTextWriter writer)
        {
            if ((item != null) && (writer != null))
            {
                if (item.Url.Length > 0)
                {
                    writer.WriteBeginTag("li");
                    writer.Write(HtmlTextWriter.TagRightChar);
                    writer.WriteBeginTag("a");
                    writer.WriteAttribute("href", Page.ResolveUrl(item.Url));
                    writer.Write(HtmlTextWriter.TagRightChar);
                    writer.Write(item.Title);
                    writer.WriteEndTag("a");
                    writer.WriteEndTag("li");
                }
                else
                {
                    writer.WriteLine();
                    writer.WriteBeginTag("li");
                    writer.WriteAttribute("class", "active");
                    writer.Write(HtmlTextWriter.TagRightChar);
                    writer.Write(item.Title);
                    writer.WriteEndTag("li");
                }
            }
        }
    }
}