using System;
using System.Web.Security;
using System.Web.UI;

namespace CernunnosWeb
{

    public partial class Site : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Esconde Controles da Master.
        /// </summary>
        public void EscondeControlesMaster()
        {
            this.header.Visible = false;
            this.SiteMap1.Visible = false;
            this.footer.Visible = false;
        }

        /// <summary>
        /// Responsável pelo carregamento dos controles da página.
        /// </summary>
        /// <param name="sender">Objeto que está realizando o evento.</param>
        /// <param name="e">Contém os dados do evento.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }

        /// <summary>
        /// Finaliza a sessão do usuário e o redireciona para a tela de login.
        /// </summary>
        /// <param name="sender">Objeto que está realizando o evento.</param>
        /// <param name="e">Contém os dados do evento.</param>
        protected void SingOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
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