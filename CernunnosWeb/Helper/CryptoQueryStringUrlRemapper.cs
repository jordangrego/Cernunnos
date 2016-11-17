using System;
using System.Web;
using System.Web.Configuration;
using CernunnosWeb.Security.Cryptography;

namespace Montreal.Protocolo.Web.Helper
{
    /// <summary>
    /// Http module that handles encrypted query strings.
    /// </summary>
    public class CryptoQueryStringUrlRemapper : IHttpModule
    {
        #region IHttpModule Members

        /// <summary>
        /// Initialize the http module.
        /// </summary>
        /// <param name="application">Application, that called this module.</param>
        public void Init(HttpApplication application)
        {
            // Attach the acquire request state event to catch the encrypted query string
            application.BeginRequest += this.Application_BeginRequest;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion

        /// <summary>
        /// Event that notify a module that new request is beginning.
        /// </summary>
        /// <param name="sender">Objeto que está realizando o evento.</param>
        /// <param name="e">Contém os dados do evento.</param>
        public void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context.Request.Url.OriginalString.Contains("aspx") && context.Request.RawUrl.Contains("?"))
            {
                string queryStrings = context.Request.ServerVariables["QUERY_STRING"];

                if (!queryStrings.Equals(string.Empty))
                {
                    ////Checks whether the query string is encrypted
                    if (queryStrings.StartsWith(CryptoQueryStringHandler.ParameterName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (context.Request.QueryString.AllKeys.Length == 1)
                        {
                            queryStrings = queryStrings.Substring(CryptoQueryStringHandler.ParameterName.Length);

                            string cryptoKey = WebConfigurationManager.AppSettings["CryptoKey"];
                            string decryptedQuery = CryptoQueryStringHandler.DecryptQueryStrings(queryStrings, cryptoKey);

                            context.RewritePath(context.Request.AppRelativeCurrentExecutionFilePath, string.Empty, decryptedQuery);
                        }
                        else if (!queryStrings.Contains("AsyncFileUploadID"))
                        {
                            throw new Exception("Request inválido");
                        }
                    }
                    else if (context.Request.HttpMethod == "GET")
                    {
                        ////Encrypt the query string and redirects to the encrypted URL.
                        ////Remove if you don't want all query strings to be encrypted automatically.
                        string encryptedQuery = CryptoQueryStringHandler.EncryptQueryStrings(queryStrings, WebConfigurationManager.AppSettings["CryptoKey"]);
                        ////string encryptedQuery = Encrypt(query);
                        context.Response.Redirect(context.Request.AppRelativeCurrentExecutionFilePath + "?" + encryptedQuery);
                    }
                }
            }
        }
    }
}