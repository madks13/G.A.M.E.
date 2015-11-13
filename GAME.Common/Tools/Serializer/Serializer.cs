using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GAME.Common.Core.Tools.Serializer
{
    public class Serializer<T>
        where T : class
    {
        public static T Deserialize(string file)
        {
            var serializer = new XmlSerializer(typeof(T));
            try
            {
                using (var fs = new FileStream(file, FileMode.Open))
                {
                    var result = (T)serializer.Deserialize(fs);

                    return result;
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        private static string Serialize(XmlSerializer serializer, string file, T serializable)
        {
            string originalMessage = String.Empty;

            using (var fs = new FileStream(file, FileMode.Create))
            {
                serializer.Serialize(fs, serializable);
                fs.Position = 0;
                var document = new XmlDocument();
                document.Load(fs);

                originalMessage = document.OuterXml;
            }

            return originalMessage;
        }

        public static string Serialize(string file, T serializable)
        {
            var serializer = new XmlSerializer(typeof(T));

            return Serialize(serializer, file, serializable);
        }

        public static string Serialize(string file, T serializable, Type[] extraTypes)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T), extraTypes);
                return Serialize(serializer, file, serializable);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error caught : {0}", e.Message);
            }
            return String.Empty;
        }

        public static string Serialize(string file, T serializable, string nameSpace)
        {
            var serializer = new XmlSerializer(typeof(T), nameSpace);
            return Serialize(serializer, file, serializable);
        }
    }
}
