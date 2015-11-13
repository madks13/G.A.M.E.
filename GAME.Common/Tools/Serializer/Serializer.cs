using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace GAME.Common.Core.Tools.Serializer
{
    public class Serializer<T>
        where T : class
    {
        public static T Deserialize(string file)
        {
            try
            {
                using (var fs = new FileStream(file, FileMode.Open))
                {
                    //var result = (T)GetSerializer().Deserialize(fs);
                    var result = (T)GetSerializer().ReadObject(fs);

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

        private static Boolean Serialize(DataContractSerializer serializer, string file, T serializable)
        {
            using (var fs = XmlWriter.Create(file, new XmlWriterSettings() {Indent = true, IndentChars = "\t" }))
            {
                serializer.WriteObject(fs, serializable);
            }

            return true;
        }

        private static DataContractSerializer GetSerializer(Type[] extraTypes = null)
        {
            if (extraTypes != null && extraTypes.Length > 0)
            {
                return new DataContractSerializer(typeof(T), extraTypes);
                //return new XmlSerializer(typeof(T), extraTypes);
            }
            //return new XmlSerializer(typeof(T));
            return new DataContractSerializer(typeof(T));
        }

        public static Boolean Serialize(string file, T serializable)
        {
            return Serialize(GetSerializer() , file, serializable);
        }

        public static Boolean Serialize(string file, T serializable, Type[] extraTypes)
        {
            try
            {
                return Serialize(GetSerializer(extraTypes), file, serializable);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error caught : {0}", e.Message);
            }
            return false;
        }
    }
}
