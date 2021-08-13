
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace SocketApp
{
    public static class ProcessData
    {
        public static byte[] SerializeData(object o)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf1 = new BinaryFormatter();
                try
                {
                    bf1.Serialize(ms, o);
                    return ms.ToArray();
                }
                catch(SerializationException)
                {
                    return null;
                }
            }
        }

        public static object DeserializeData(byte[] theByteArray)
        {
            using (MemoryStream ms = new MemoryStream(theByteArray))
            {
                BinaryFormatter bf1 = new BinaryFormatter();

                ms.Position = 0;
                try
                {
                    return bf1.Deserialize(ms);
                }
                catch(SerializationException)
                {
                    return null;
                }
            }
        }
    }
}
