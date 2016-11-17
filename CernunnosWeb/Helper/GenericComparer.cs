using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;

namespace Montreal.Protocolo.Web.Helper
{
    /// <summary>
    /// Define um método que implementa um tipo para comparar dois objetos.
    /// </summary>
    /// <typeparam name="T">O tipo de objetos para comparar.</typeparam>
    public class GenericComparer<T> : IComparer<T>
    {
        #region Properties

        /// <summary>
        /// Ordenação ascendente ou decrescente.
        /// </summary>
        private SortDirection sortDirection;

        /// <summary>
        /// Campo a ser ordenado.
        /// </summary>
        private string sortExpression;

        #endregion

        #region Constructors

        /// <summary>
        /// Inicia a classe com os parametros de ordenação.
        /// </summary>
        /// <param name="sortExpression">Campo a ser ordenado.</param>
        /// <param name="sortDirection">Ordenação ascendente ou decrescente.</param>
        public GenericComparer(string sortExpression, SortDirection sortDirection)
        {
            this.sortExpression = sortExpression;
            this.sortDirection = sortDirection;
        }

        #endregion

        #region IComparer<T> Members

        public int Compare(T x, T y)
        {
            string[] fieldParts = this.sortExpression.Split('.');

            object compareValX = x;
            object compareValY = y;

            foreach (string field in fieldParts)
            {
                compareValX = this.GetPropertyValue(compareValX, field);
                compareValY = this.GetPropertyValue(compareValY, field);
            }

            int retorno;

            if (compareValX == null && compareValY == null)
            {
                retorno = 0;
            }
            else if (compareValX == null)
            {
                retorno = -1;
            }
            else if (compareValY == null)
            {
                retorno = 1;
            }
            else
            {
                retorno = ((IComparable)compareValX).CompareTo(compareValY);
            }

            if (this.sortDirection == SortDirection.Descending)
            {
                retorno = retorno * -1;
            }

            return retorno;
        }

        private object GetPropertyValue(object o, string property)
        {
            if (o != null)
            {
                PropertyInfo pi = o.GetType().GetProperty(property);
                object val = pi.GetValue(o, null);
                return val;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}