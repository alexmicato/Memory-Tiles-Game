using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace PairGameApplication.Data
{
    public class xmlSerializationUtility
    {
        public static void Serialize<T>(T obj, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
            }
        }

        public static T Deserialize<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
