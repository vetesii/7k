using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model
{
    class MultiLanguageTextProxy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="">Language key not found exception</exception>
        public static String getText(String key)
        {
            
            // throw 
            // TODO ha nincs meg  a value -> throw exception

            return String.Empty;
        }

        public static String getTextOrEmptyString(String key)
        {
            try
            {
                return getText(key);
            }
            catch
            {
                return String.Empty;
            }
        }

        public static String getTextOrDefaultText(String key, String def)
        {
            try
            {
                return getText(key);
            }
            catch
            {
                return def;
            }
        }
    }
}
