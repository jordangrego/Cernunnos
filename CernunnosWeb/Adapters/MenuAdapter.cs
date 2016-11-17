using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CernunnosWeb.Adapters
{
    /// <summary>
    /// This CSS Adapter is used to control the rendering of the ASP.NET Menu control.
    /// By default the control renders as a series of embedded tables and is difficult to apply
    /// CSS styles to and not compliant with NZ Web Guidelines.
    /// This adapter renders the control using unordered lists, ul.
    /// </summary>
    public class MenuAdapter : System.Web.UI.WebControls.Adapters.MenuAdapter
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MenuAdapter()
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
        /// Wrap the control in an outer div. This is useful in the event there are no
        /// menu items to render.
        /// </summary>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        protected override void RenderBeginTag(HtmlTextWriter writer)
        {
            // The current implementation doesn't wrap the control in a div. Not 
            // sure it's necessary.
        }

        /// <summary>
        /// Render the HTML for the end of the control.
        /// </summary>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        protected override void RenderEndTag(HtmlTextWriter writer)
        {
            // The current implementation doesn't wrap the control in a div. Not 
            // sure it's necessary.
        }

        /// <summary>
        /// Render the HTML for the contents of the control.
        /// </summary>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Indent++;
            this.BuildItems(Control.Items, true, writer);
            writer.Indent--;
            writer.WriteLine();
        }

        /// <summary>
        /// Render the HTML for each set of menu items. 
        /// </summary>
        /// <param name="items">Menu items.</param>
        /// <param name="isRoot">Is root menu.</param>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        private void BuildItems(MenuItemCollection items, bool isRoot, HtmlTextWriter writer)
        {
            if (items.Count > 0)
            {
                writer.WriteLine();

                writer.WriteBeginTag("ul");

                // If the user has specified a class name on the control then use this for the outer ul
                if (isRoot)
                {
                    writer.WriteAttribute("class", "nav navbar-nav");
                }
                else
                {
                    writer.WriteAttribute("class", "dropdown-menu");
                }

                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Indent++;

                foreach (MenuItem item in items)
                {
                    this.BuildItem(item, writer);
                }

                // If this is the root then we've already rendered the outer ul
                writer.Indent--;
                writer.WriteLine();
                writer.WriteEndTag("ul");
            }
        }

        /// <summary>
        /// Render each MenuItem.
        /// </summary>
        /// <param name="item">Menu item.</param>
        /// <param name="writer">Containing methods to build and render the device-specific output.</param>
        private void BuildItem(MenuItem item, HtmlTextWriter writer)
        {
            Menu menu = Control as Menu;
            if ((menu != null) && (item != null) && (writer != null))
            {
                writer.WriteLine();
                writer.WriteBeginTag("li");

                string className = string.Empty;
                if (item.ChildItems.Count > 0)
                {
                    className += "dropdown";
                }

                if (this.IsActive(item))
                {
                    className += (className.Length > 0 ? " " : string.Empty) + "active";
                }

                if (className != string.Empty)
                {
                    writer.WriteAttribute("class", className);
                }

                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Indent++;
                writer.WriteLine();

                if (item.NavigateUrl.Length > 0)
                {
                    writer.WriteBeginTag("a");
                    writer.WriteAttribute("href", Page.ResolveUrl(item.NavigateUrl));
                    if (item.Target.Length > 0)
                    {
                        writer.WriteAttribute("target", item.Target);
                    }

                    if (item.ToolTip.Length > 0)
                    {
                        writer.WriteAttribute("title", item.ToolTip);
                    }
                    else if (menu.ToolTip.Length > 0)
                    {
                        writer.WriteAttribute("title", menu.ToolTip);
                    }

                    writer.Write(HtmlTextWriter.TagRightChar);
                }
                else if (item.ChildItems.Count > 0)
                {
                    writer.WriteBeginTag("a");
                    writer.WriteAttribute("href", "#");
                    writer.WriteAttribute("class", "dropdown-toggle");
                    writer.WriteAttribute("data-toggle", "dropdown");

                    if (item.ToolTip.Length > 0)
                    {
                        writer.WriteAttribute("title", item.ToolTip);
                    }
                    else if (menu.ToolTip.Length > 0)
                    {
                        writer.WriteAttribute("title", menu.ToolTip);
                    }

                    writer.Write(HtmlTextWriter.TagRightChar);
                }
                else
                {
                    writer.WriteBeginTag("a");
                    writer.WriteAttribute("href", "#");
                    writer.Write(HtmlTextWriter.TagRightChar);
                }

                if (item.ImageUrl.Length > 0)
                {
                    writer.WriteBeginTag("img");
                    writer.WriteAttribute("src", Page.ResolveUrl(item.ImageUrl));
                    writer.WriteAttribute("alt", item.ToolTip.Length > 0 ? item.ToolTip : (menu.ToolTip.Length > 0 ? menu.ToolTip : item.Text));
                    writer.Write(HtmlTextWriter.SelfClosingTagEnd);
                }

                writer.Write(item.Text);

                if (item.NavigateUrl.Length > 0)
                {
                    writer.WriteEndTag("a");
                }
                else if (item.ChildItems.Count > 0)
                {
                    writer.WriteBeginTag("b");
                    writer.WriteAttribute("class", "caret");
                    writer.Write(HtmlTextWriter.TagRightChar);
                    writer.WriteEndTag("b");
                    writer.WriteEndTag("a");
                }
                else
                {
                    writer.WriteEndTag("a");
                }

                if ((item.ChildItems != null) && (item.ChildItems.Count > 0))
                {
                    this.BuildItems(item.ChildItems, false, writer);
                }

                writer.Indent--;
                writer.WriteLine();
                writer.WriteEndTag("li");
            }
        }

        /// <summary>
        /// Determines whether a menu item should be considered active. This will include
        /// the actual selected item and it's ancestors.
        /// </summary>
        /// <param name="item">Menu item.</param>
        /// <returns>Returns a boolean indicating whether variable is active.</returns>
        private bool IsActive(MenuItem item)
        {
            if (item.Selected)
            {
                return true;
            }

            foreach (MenuItem child in item.ChildItems)
            {
                if (this.IsActive(child))
                {
                    return true;
                }
            }

            return false;
        }
    }
}