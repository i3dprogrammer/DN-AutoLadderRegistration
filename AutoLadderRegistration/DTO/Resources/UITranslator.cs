using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutoLadderRegistration.DTO.Resources
{
    class UITranslator
    {
        private static Dictionary<ulong, string> uistring = new Dictionary<ulong, string>();

        public static bool CheckID(ulong id)
        {
            return uistring.ContainsKey(id);
        }

        public static string GetString(ulong id)
        {
            if (CheckID(id))
                return uistring[id];

            throw new Exception("ID not found.");
        }

        public static void LoadUIString(string path)
        {
            if (!path.EndsWith(".xml"))
                throw new Exception("Not an XML file.");

            XDocument doc = XDocument.Load(path);
            foreach (var el in doc.Descendants().Where(x => x.Name == "message"))
            {
                var uid = ulong.Parse(el.FirstAttribute.Value);
                if (!uistring.ContainsKey(uid))
                {
                    uistring.Add(uid, el.Value);
                }
            }
        }
    }
}
