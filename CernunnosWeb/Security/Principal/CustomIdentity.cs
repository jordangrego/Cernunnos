using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace CernunnosWeb.Security.Principal
{
    /// <summary>
    /// Implementa uma classe de Identidade personalizado para uso com o SISRH.
    /// </summary>
    [Serializable]
    public class CustomIdentity : MarshalByRefObject, IIdentity
    {
        /// <summary>
        /// Id do Usuário.
        /// </summary>
        private long idUsuario;

        /// <summary>
        /// Nome do Usuário.
        /// </summary>
        private string name;

        /// <summary>
        /// Login do Usuario.
        /// </summary>
        private string login;

        /// <summary>
        /// Email do Usuario.
        /// </summary>
        private string email;

        /// <summary>
        /// Perfis do Usuario.
        /// </summary>
        private string[] roles;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public CustomIdentity()
        {
        }

        /// <summary>
        /// Inicializa a classe com os dados informados.
        /// </summary>
        /// <param name="data">Dados do usuário.</param>
        public CustomIdentity(string data)
        {
            if (data.Contains(";"))
            {
                var stringParts = data.Split(';');

                if (stringParts.Length > 0)
                {
                    long.TryParse(stringParts[0], out this.idUsuario);
                }

                if (stringParts.Length > 1)
                {
                    this.name = stringParts[1];
                }

                if (stringParts.Length > 2)
                {
                    this.login = stringParts[2];
                }

                if (stringParts.Length > 3)
                {
                    this.email = stringParts[3];
                }

                if (stringParts.Length > 4)
                {
                    this.roles = stringParts[4].Split('|');
                }
            }
        }

        /// <summary>
        /// Inicializa a classe passando cada parametro.
        /// </summary>
        /// <param name="idUsuario">Id do Usuario.</param>
        /// <param name="name">Nome do Usuario.</param>
        /// <param name="login">Login do Usuario.</param>
        /// <param name="email">Email do Usuario.</param>
        /// <param name="idCliente">Id do Cliente.</param>
        /// <param name="roles">Perfis do Usuario.</param>
        public CustomIdentity(long idUsuario, string name, string login, string email, string[] roles)
        {
            this.idUsuario = idUsuario;
            this.name = name.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            this.login = login;
            this.email = email;
            this.roles = roles;
        }

        /// <summary>
        /// Tipo da Autenticação.
        /// </summary>
        public string AuthenticationType
        {
            get { return "CustomIdentity"; }
        }

        /// <summary>
        /// Verifica se está autenticado.
        /// </summary>
        public bool IsAuthenticated
        {
            get { return !string.IsNullOrWhiteSpace(this.Name); }
        }

        /// <summary>
        /// Id do Usuario.
        /// </summary>
        public long IdUsuario
        {
            get { return this.idUsuario; }
        }

        /// <summary>
        /// Nome do Usuario.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Login do Usuario.
        /// </summary>
        public string Login
        {
            get { return this.login; }
        }

        /// <summary>
        /// Email do Usuario.
        /// </summary>
        public string Email
        {
            get { return this.email; }
        }

        /// <summary>
        /// Perfis do Usuario.
        /// </summary>
        public string[] Roles
        {
            get { return this.roles; }
        }

        /// <summary>
        /// Retorna os dados do usuário logado em string separados por (;).
        /// </summary>
        /// <returns>String dos dados do usuário.</returns>
        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};", this.idUsuario.ToString(), this.name, this.login, this.email, string.Join("|", this.roles));
        }
    }
}
