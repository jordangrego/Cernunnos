using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace CernunnosWeb.Security.Principal
{
    /// <summary>
    /// Implementa uma classe principal personalizada que utiliza o banco de dados SISRH 
    /// para verificar se há associação de função.
    /// </summary>
    [Serializable]
    public class CustomPrincipal : MarshalByRefObject, IPrincipal
    {
        /// <summary>
        /// Implementa a propriedade Identidade definida por IPrincipal.
        /// </summary>
        private IIdentity identity;

        /// <summary>
        /// Pesfis do usuário.
        /// </summary>
        private string[] roles;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="identity">O Identity atribuído ao usuário.</param>
        public CustomPrincipal(CustomIdentity identity)
        {
            this.identity = identity;
            this.roles = new string[identity.Roles.Length];
            identity.Roles.CopyTo(this.roles, 0);
            Array.Sort(this.roles);
        }

        /// <summary>
        /// Implementa a propriedade Identidade definida por IPrincipal.
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }
        }

        /// <summary>
        /// Implementa a propriedade IsInRole definido por IPrincipal.
        /// </summary>
        /// <param name="role">Perfil a ser verificado.</param>
        /// <returns>Boleano indicando se o usuário está no perfil.</returns>
        public bool IsInRole(string role)
        {
            return Array.BinarySearch(this.roles, role) >= 0 ? true : false;
        }

        /// <summary>
        /// Verifica se usuário está em todos os prefism especificados.
        /// </summary>
        /// <param name="roles">Perfis a serem verificados.</param>
        /// <returns>Boleano indicando se o usuário está nos perfis.</returns>
        public bool IsInAllRoles(params string[] roles)
        {
            foreach (string searchrole in roles)
            {
                if (Array.BinarySearch(this.roles, searchrole) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Verifica se usuário está em qualquer um dos prefis especidicados.
        /// </summary>
        /// <param name="roles">Perfis a serem verificados.</param>
        /// <returns>Boleano indicando se o usuário está no perfil.</returns>
        public bool IsInAnyRoles(params string[] roles)
        {
            foreach (string searchrole in roles)
            {
                if (Array.BinarySearch(this.roles, searchrole) > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
