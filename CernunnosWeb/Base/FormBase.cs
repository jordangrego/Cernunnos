using Montreal.Protocolo.Web.Helper;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CernunnosWeb.Base
{
    /// <summary>
    /// Contem os métodos padrões de dos formularios do sistema.
    /// </summary>
    public class FormBase : PageBase
    {
        #region "Campos privados e Propriedades"

        /// <summary>
        /// Armazena o IdFornecedor em caso de alteração do registro.
        /// </summary>
        protected int IdRegistro
        {
            get
            {
                if (Extension.FindControl<HiddenField>(this.Page, "hdnIdRegistro") != null && !(Extension.FindControl<HiddenField>(this.Page, "hdnIdRegistro")).Value.Equals(string.Empty))
                {
                    return int.Parse(Extension.FindControl<HiddenField>(this.Page, "hdnIdRegistro").Value);
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                if (Extension.FindControl<HiddenField>(this.Page, "hdnIdRegistro") != null)
                {
                    Extension.FindControl<HiddenField>(this.Page, "hdnIdRegistro").Value = value.ToString();
                }
            }
        }

        /// <summary>
        /// Informa se a página é somente leitura.
        /// </summary>
        protected bool IsSomenteLeitura
        {
            get
            {
                if (Request.QueryString["Visualizar"] != null && !Request.QueryString["Visualizar"].Equals(string.Empty))
                {
                    return Request.QueryString["Visualizar"].ToString().Equals("1");
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Informa se esta inserindo um novo registro.
        /// </summary>
        protected bool IsNovoRegistro
        {
            get
            {
                return (this.IdRegistro == 0);
            }
        }

        #endregion

        /// <summary>
        /// Guarda o id do registro para alteração.
        /// </summary>
        /// <param name="e">Contém os dados do evento.</param>
        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["IdRegistro"] != null && !Request.QueryString["IdRegistro"].Equals(string.Empty))
                {
                    (Extension.FindControl<HiddenField>(this.Page, "hdnIdRegistro")).Value = Request.QueryString["IdRegistro"].ToString();
                }
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// Configura o nivel de acesso a página.
        /// </summary>
        /// <param name="e">Contém os dados do evento.</param>
        protected override void OnConfigurarAcesso(EventArgs e)
        {
            base.OnConfigurarAcesso(e);

            if (this.IsSomenteLeitura)
            {
                this.DesabilitarControles(this.Page);
                if (Extension.FindControl<Label>(this.Page, "lblAcao") != null)
                {
                    (Extension.FindControl<Label>(this.Page, "lblAcao")).Text = "Visualizar";
                }

                if (Extension.FindControl<LinkButton>(this.Page, "btnExcluir") != null)
                {
                    (Extension.FindControl<LinkButton>(this.Page, "btnExcluir")).Visible = false;
                }

                if (Extension.FindControl<Button>(this.Page, "btnSalvarNovo") != null)
                {
                    (Extension.FindControl<Button>(this.Page, "btnSalvarNovo")).Visible = false;
                }

                if (Extension.FindControl<LinkButton>(this.Page, "btnVoltar") != null)
                {
                    var btnVoltar = (Extension.FindControl<LinkButton>(this.Page, "btnVoltar"));
                    btnVoltar.CssClass = btnVoltar.CssClass.Replace("btn-default", "btn-primary");
                }
            }
            else if (!this.IdRegistro.Equals(0))
            {
                if (Extension.FindControl<Label>(this.Page, "lblAcao") != null)
                {
                    (Extension.FindControl<Label>(this.Page, "lblAcao")).Text = "Alterar";
                }

                if (Extension.FindControl<LinkButton>(this.Page, "btnExcluir") != null)
                {
                    (Extension.FindControl<LinkButton>(this.Page, "btnExcluir")).Visible = true;
                }

                if (Extension.FindControl<Button>(this.Page, "btnSalvarNovo") != null)
                {
                    (Extension.FindControl<Button>(this.Page, "btnSalvarNovo")).Visible = false;
                }
            }
            else
            {
                if (Extension.FindControl<Label>(this.Page, "lblAcao") != null)
                {
                    (Extension.FindControl<Label>(this.Page, "lblAcao")).Text = "Cadastrar";
                }

                if (Extension.FindControl<LinkButton>(this.Page, "btnExcluir") != null)
                {
                    (Extension.FindControl<LinkButton>(this.Page, "btnExcluir")).Visible = false;
                }

                if (Extension.FindControl<Button>(this.Page, "btnSalvarNovo") != null)
                {
                    (Extension.FindControl<Button>(this.Page, "btnSalvarNovo")).Visible = true;
                }
            }

            ////Configura o botão padão do formulário
            if (Extension.FindControl<Button>(this.Page, "btnSalvar") != null)
            {
                this.Form.DefaultButton = (Extension.FindControl<Button>(this.Page, "btnSalvar")).UniqueID;
            }
        }

        /// <summary>
        /// Faz o download do documento do projeto básico.
        /// </summary>
        /// <param name="id">Id do documento.</param>
        protected void DownloadFile(int id)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "DOWNLOAD" + DateTime.Now.ToString("HHmmss"), "DownloadFile('" + id.ToString() + "');", true);
        }
    }
}