using System.Text.Json;

namespace SportsStore.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T? GetJson<T>(this ISession session, string key)
        {
            var datosSession = session.GetString(key);
            return datosSession == null
            ? default(T) : JsonSerializer.Deserialize<T>(datosSession);
        }
    }
}