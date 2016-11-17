using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CernunnosWeb
{
    public partial class Unauthorized : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Responsável pelo carregamento dos controles da página.
        /// </summary>
        /// <param name="sender">Objeto que está realizando o evento.</param>
        /// <param name="e">Contém os dados do evento.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Trata as exceções nas chamadas Ajax.
        /// </summary>
        /// <param name="sender">Objeto que está realizando o evento.</param>
        /// <param name="e">Contém os dados do evento.</param>
        protected void ScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            if (!e.Exception.Message.StartsWith("VALIDACAO:"))
            {
                // ExceptionHelper.LogException(e.Exception, this.Request.Path);
            }

            this.ScriptManager.AsyncPostBackErrorMessage = e.Exception.Message;
        }
    }
}