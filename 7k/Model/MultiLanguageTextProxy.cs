using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace _7k.Model
{
    class MultiLanguageTextProxy
    {
        static ResourceManager res_man= new ResourceManager("_7k.Resource.all", typeof(MultiLanguageTextProxy).Assembly);
        static CultureInfo cul_info = CultureInfo.CreateSpecificCulture("hu");

        public static void SwitchLanguage()
        {
             if (true) cul_info = CultureInfo.CreateSpecificCulture("hu");         
             else cul_info = CultureInfo.CreateSpecificCulture("en");         
        }

        public static String GetText(String key, String def = "")
        {
            try
            {
                String ret = res_man.GetString(key, cul_info);
                if (ret == null) throw new KeyNotFoundException();
                return ret;
            }
            catch
            {
                // TODO 4 make log
                return def;
            }
        }

        public static String GetText(CultureInfo cul, String key, String def = "")
        {
            if(cul != null) cul_info = cul;

            return GetText(key, def);
        }

    }
}
