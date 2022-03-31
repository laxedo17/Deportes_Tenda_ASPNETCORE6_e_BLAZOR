using System.Text.Json.Serialization;
using SportsStore.Infrastructure;
using TendaDeportes.Infrastructura;

namespace TendaDeportes.Models
{
    /// <summary>
    /// Subclase da clase Cesta, fai override aos metodos AddItem, RemoveLinea e Clear, chamando a implementacions base e gardando despois o estado actualizado na session usando os extension methods na interface ISession
    /// </summary>
    public class SessionCesta : Cesta
    {
        /// <summary>
        /// Este metodo e unha factory por crear obxectos SessionCesta e proporcionarlle un obxecto ISession de maneira que poden gardarse a sí mismos.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static Cesta GetCesta(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?.Session;
            SessionCesta cesta = session?.GetJson<SessionCesta>("Cesta") ?? new SessionCesta();

            cesta.Session = session;
            return cesta;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }

        public override void AddItem(Producto producto, int cantidade)
        {
            base.AddItem(producto, cantidade);
            Session?.SetJson("Cesta", this);
        }

        public override void RemoveLinea(Producto producto)
        {
            base.RemoveLinea(producto);
            Session?.SetJson("Cesta", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove("Cesta");
        }
    }
}