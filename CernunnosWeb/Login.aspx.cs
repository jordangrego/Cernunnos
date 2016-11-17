using CernunnosLib.Negocio;
using System;
using System.Web.UI.WebControls;

namespace CernunnosWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AutenticaUsuario_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string login = this.lgnAutenticaUsuario.UserName;
            string dominio = string.Empty;
            string senha = this.lgnAutenticaUsuario.Password;

            if (login.IndexOf("\\") >= 0)
            {
                dominio = login.Substring(0, login.IndexOf("\\"));
                login = login.Substring(login.IndexOf("\\") + 1);
            }

            e.Authenticated = new SegurancaBcl().AutenticarUsuario(login, ref senha, dominio);
        }
    }
}