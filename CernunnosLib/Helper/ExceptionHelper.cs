using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CernunnosLib.Helper
{
    public sealed class ExceptionHelper
    {
        /// <summary>
        /// Retorna o StackTrace.
        /// </summary>
        /// <param name="exc">A exceção a ser tratada.</param>
        /// <returns>StackTrace em formato String.</returns>
        public static string GetStackTrace(Exception exc)
        {
            StringBuilder exception = new StringBuilder();
            if (exc.InnerException != null)
            {
                if (exc.InnerException.StackTrace != null)
                {
                    exception.AppendLine(exc.InnerException.StackTrace);
                    exception.AppendLine();
                }
            }

            if (exc.StackTrace != null)
            {
                exception.AppendLine(exc.StackTrace);
                exception.AppendLine();
            }

            return exception.ToString();
        }

        /// <summary>
        /// Grava a exceção no banco de dados.
        /// </summary>
        /// <param name="exc">A exceção a ser tratada.</param>
        /// <param name="source">Arquivo em que a exceção ocorreu.</param>
        public static void LogException(Exception exc, string source)
        {
            /*
            LogExcecao excecao = new LogExcecao();

            if (HttpContext.Current != null)
            {
                CustomIdentity usuario = HttpContext.Current.User.Identity as CustomIdentity;
                if (usuario != null)
                {
                    excecao.IdUsuario = usuario.IdUsuario;
                }

                excecao.Ip = HttpContext.Current.Request.UserHostAddress;
            }

            excecao.Url = source;
            excecao.DataHora = DateTime.Now;
            excecao.Mensagem = exc.Message;
            excecao.StackTrace = GetStackTrace(exc);

            new LogExcecaoBcl().Inserir(excecao);
            */
        }
    }
}
