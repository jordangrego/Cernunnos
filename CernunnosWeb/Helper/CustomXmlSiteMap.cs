using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CernunnosWeb.Security.Principal;

namespace CernunnosWeb.Helper
{
    /// <summary>
    /// Class generates site map trees from XML files with the file name extension .sitemap.
    /// </summary>
    public class CustomXmlSiteMap : System.Web.XmlSiteMapProvider
    {
        /// <summary>
        /// Retrieves a Boolean value indicating whether the specified System.Web.SiteMapNode 
        /// object can be viewed by the user in the specified context.
        /// </summary>
        /// <param name="context">The System.Web.HttpContext that contains user information.</param>
        /// <param name="node">The System.Web.SiteMapNode that is requested by the user.</param>
        /// <returns>True if security trimming is enabled and node can be viewed by the user or security trimming is not enabled; otherwise, false.</returns>
        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            CustomPrincipal user = HttpContext.Current.User as CustomPrincipal;

            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (!this.SecurityTrimmingEnabled)
            {
                return true;
            }

            if ((node.Roles != null) && (node.Roles.Count > 0))
            {
                foreach (string role in node.Roles)
                {
                    if (!string.Equals(role, "*", StringComparison.InvariantCultureIgnoreCase) && ((user == null) || !user.IsInRole(role)))
                    {
                        continue;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}