using System.Text;

namespace SaucyCapstone.Static
{
    public static class SessionExtensions
    {
        public static void SetString(this ISession session, string key, string value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(value ));
        }

        public static string GetString(this ISession session, string key)
        {
            return Encoding.UTF8.GetString(session.Get(key));
        }

        public static void SetInt(this ISession session, string key, int value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(value.ToString()));
        }

        public static int GetInt(this ISession session, string key)
        {
            var parsed = int.TryParse(Encoding.UTF8.GetString(session.Get(key)), out int result);
            if (parsed)
            {
                return result;
            }
            return 0;  
        }
    }
}
