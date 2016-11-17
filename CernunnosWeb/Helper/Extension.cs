using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Montreal.Protocolo.Web.Helper
{
    /// <summary>
    /// Classe de extensão.
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Recursivamente encontra um controle filho do pai especificado.
        /// </summary>
        /// <param name="startingControl">Controle pai.</param>
        /// <param name="id">Controle a ser procurado.</param>
        /// <returns>O controle se ele for encontrado ou nulo se não for.</returns>
        public static T FindControl<T>(this Control startingControl, string id) where T : Control
        {
            T found = startingControl.FindControl(id) as T;

            if (found == null)
            {
                foreach (Control activeControl in startingControl.Controls)
                {
                    found = activeControl as T;

                    if (found == null || (string.Compare(id, found.ID, true) != 0))
                    {
                        found = FindControl<T>(activeControl, id);
                    }

                    if (found != null)
                    {
                        break;
                    }
                }
            }

            return found;
        }

        /*
        /// <summary>     
        /// Similar to Control.FindControl, but recurses through child controls.
        /// Assumes that startingControl is NOT the control you are searching for.
        /// </summary>
        /// <param name="startingControl">Controle pai.</param>
        /// <param name="id">Controle a ser procurado.</param>
        /// <returns>O controle se ele for encontrado ou nulo se não for.</returns>
        public static T FindChildControl<T>(this Control startingControl, string id) where T : Control
        {
            T found = null;

            foreach (Control activeControl in startingControl.Controls)
            {
                found = activeControl as T;

                if (found == null || (string.Compare(id, found.ID, true) != 0))
                {
                    found = FindChildControl<T>(activeControl, id);
                }

                if (found != null)
                {
                    break;
                }
            }

            return found;
        }
        */
    }
}