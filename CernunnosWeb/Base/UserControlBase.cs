using CernunnosWeb.Security.Principal;
using System;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CernunnosWeb.Base
{
    /// <summary>
    /// Contem os métodos padrões nos controles do sistema.
    /// </summary>
    public class UserControlBase : System.Web.UI.UserControl
    {
        /// <summary>
        /// Evento para configurar o formulário de acordo com as situações.
        /// </summary>
        public event EventHandler ConfigurarAcesso;

        /// <summary>
        /// Retorna os dados do usuário logado.
        /// </summary>
        public CustomIdentity UsuarioLogado
        {
            get
            {
                return this.Page.User.Identity as CustomIdentity;
            }
        }

        /// <summary>
        /// Retorna os dados do usuário.
        /// </summary>
        public IPrincipal User
        {
            get
            {
                return this.Page.User;
            }
        }

        /// <summary>
        /// Configura os acessos da página conforme a ação (Cadastrar, Atualizar e Visuzalizar).
        /// </summary>
        /// <param name="e">Contém os dados do evento.</param>
        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.OnConfigurarAcesso(EventArgs.Empty);
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Configura o nivel de acesso a página.
        /// </summary>
        /// <param name="e">Contém os dados do evento.</param>
        protected virtual void OnConfigurarAcesso(EventArgs e)
        {
            EventHandler handler = this.ConfigurarAcesso;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Registra o script no controle informado.
        /// </summary>
        /// <param name="control">Controle em que o script será registrado.</param>
        /// <param name="script">Script a ser registrado.</param>
        protected void RegistrarScript(Control control, string script)
        {
            ScriptManager.RegisterStartupScript(control, control.GetType(), "SCRIPT_" + DateTime.Now.ToString("ddMMyyymmssfffffff") + "_" + new Random(int.Parse(DateTime.Now.ToString("fffffff"))).Next(9999999), script, true);
            if (control is UpdatePanel && ((UpdatePanel)control).UpdateMode == UpdatePanelUpdateMode.Conditional)
            {
                ((UpdatePanel)control).Update();
            }
        }

        /// <summary>
        /// Retira ponto e converte virgula em ponto do campos de valores.
        /// </summary>
        /// <param name="valor">Valor a ser convertido.</param>
        /// <returns>Valor formatado.</returns>
        protected decimal StringToDecimal(string valor)
        {
            if (!valor.Equals(string.Empty))
            {
                ////valor = valor.Replace(".", string.Empty);
                ////valor = valor.Replace(",", ".");
                return decimal.Parse(valor);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Formata variavel decimal em string com pontuação.
        /// </summary>
        /// <param name="valor">Valor a ser convertido.</param>
        /// <returns>Valor formatado.</returns>
        protected string DecimalToString(decimal valor)
        {
            if (!valor.Equals(0))
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR");
                return valor.ToString("N", culture);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Desabilita todos os controles contidos no controle informado.
        /// </summary>
        /// <param name="parent">Controle pai a ser desabilitado.</param>
        protected void DesabilitarControles(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).ReadOnly = true;
                }
                else if (ctrl is Button)
                {
                    if (!((Button)ctrl).Text.Contains("Voltar") &&
                        !((Button)ctrl).Text.Contains("Download") &&
                        !((Button)ctrl).Text.Contains("Visualizar") &&
                        !((Button)ctrl).Text.Contains("Fechar"))
                    {
                        ((Button)ctrl).Visible = false;
                    }
                }
                else if (ctrl is LinkButton)
                {
                    if (!((LinkButton)ctrl).Text.Contains("Download") &&
                        !((LinkButton)ctrl).Text.Contains("Visualizar") &&
                        !((LinkButton)ctrl).CssClass.Contains("cancel") &&
                        !((LinkButton)ctrl).Text.Contains("glyphicon-log-out"))
                    {
                        ((LinkButton)ctrl).Visible = false;
                    }
                }
                else if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).Enabled = false;
                }
                else if (ctrl is RadioButtonList)
                {
                    ((RadioButtonList)ctrl).Enabled = false;
                }
                else if (ctrl is ImageButton)
                {
                    ((ImageButton)ctrl).Enabled = false;
                }
                else if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Enabled = false;
                }
                else if (ctrl is DropDownList)
                {
                    ((DropDownList)ctrl).Enabled = false;
                }
                else if (ctrl is ListBox)
                {
                    ((ListBox)ctrl).Enabled = false;
                }
                else if (ctrl is AjaxControlToolkit.AsyncFileUpload)
                {
                    ((AjaxControlToolkit.AsyncFileUpload)ctrl).Enabled = false;
                }
                else if (ctrl is AjaxControlToolkit.AjaxFileUpload)
                {
                    ((AjaxControlToolkit.AjaxFileUpload)ctrl).Enabled = false;
                }
                else if (ctrl is RequiredFieldValidator)
                {
                    ((RequiredFieldValidator)ctrl).Enabled = false;
                }

                this.DesabilitarControles(ctrl);
            }
        }

        /// <summary>
        /// Habilita todos os controles contidos no controle informado.
        /// </summary>
        /// <param name="parent">Controle pai a ser desabilitado.</param>
        protected void HabilitarControles(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).ReadOnly = false;
                }
                else if (ctrl is Button)
                {
                    ((Button)ctrl).Visible = true;
                }
                else if (ctrl is LinkButton)
                {
                    ((LinkButton)ctrl).Visible = true;
                }
                else if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).Enabled = true;
                }
                else if (ctrl is RadioButtonList)
                {
                    ((RadioButtonList)ctrl).Enabled = true;
                }
                else if (ctrl is ImageButton)
                {
                    ((ImageButton)ctrl).Enabled = true;
                }
                else if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Enabled = true;
                }
                else if (ctrl is DropDownList)
                {
                    ((DropDownList)ctrl).Enabled = true;
                }
                else if (ctrl is ListBox)
                {
                    ((ListBox)ctrl).Enabled = true;
                }
                else if (ctrl is AjaxControlToolkit.AsyncFileUpload)
                {
                    ((AjaxControlToolkit.AsyncFileUpload)ctrl).Enabled = true;
                }
                else if (ctrl is AjaxControlToolkit.AjaxFileUpload)
                {
                    ((AjaxControlToolkit.AjaxFileUpload)ctrl).Enabled = true;
                }
                else if (ctrl is RequiredFieldValidator)
                {
                    ((RequiredFieldValidator)ctrl).Enabled = true;
                }

                this.HabilitarControles(ctrl);
            }
        }

        /// <summary>
        /// Limpa todos os controles da pagina contidos no controle informado 
        /// e executa o método que configura os controles da página.
        /// </summary>
        /// <param name="parent">Controle pai a ser limpado.</param>
        protected void LimparControles(Control parent)
        {
            this.LimparControlesFilhos(parent);
            if (parent is UpdatePanel && ((UpdatePanel)parent).UpdateMode == UpdatePanelUpdateMode.Conditional)
            {
                ((UpdatePanel)parent).Update();
            }
        }

        /// <summary>
        /// Limpa todos os controles da contidos no controle informado.
        /// </summary>
        /// <param name="parent">Controle pai a ser limpado.</param>
        private void LimparControlesFilhos(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Text = string.Empty;
                }
                else if (ctrl is HiddenField)
                {
                    ((HiddenField)ctrl).Value = string.Empty;
                }
                else if (ctrl is DropDownList)
                {
                    ((DropDownList)ctrl).SelectedIndex = -1;
                }
                else if (ctrl is RadioButtonList)
                {
                    ((RadioButtonList)ctrl).SelectedIndex = -1;
                }
                else if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Checked = false;
                }
                else if (ctrl is GridView)
                {
                    ((GridView)ctrl).DataSource = null;
                    ((GridView)ctrl).DataBind();
                    ((GridView)ctrl).Visible = false;
                }
                else if (ctrl is DataList)
                {
                    ((DataList)ctrl).DataSource = null;
                    ((DataList)ctrl).DataBind();
                    ((DataList)ctrl).Visible = false;
                }
                else if (ctrl is Repeater)
                {
                    ((Repeater)ctrl).DataSource = null;
                    ((Repeater)ctrl).DataBind();
                    ((Repeater)ctrl).Visible = false;
                }

                this.LimparControles(ctrl);
            }
        }
    }
}