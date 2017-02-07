using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System.IO;
using System.Text;

namespace CustomTypes
{
    public class PreferredCommunicationFormatter : IFormatter<PreferredCommunication>
    {
        private readonly char DELIMITER = '|';

        public PreferredCommunication Deserialize(IColumnReader reader, ISerializationContext context)
        {
            var preferredComm = new PreferredCommunication();
            using (var binaryReader = new BinaryReader(reader.BaseStream))
            {
                preferredComm.Channels = new SqlArray<string>(binaryReader.ReadString().Split(DELIMITER));
            }

            return preferredComm;
        }

        public void Serialize(PreferredCommunication instance, IColumnWriter writer, ISerializationContext context)
        {
            using (var binaryWriter = new BinaryWriter(writer.BaseStream))
            {
                StringBuilder sb = new StringBuilder();

                foreach (var channel in instance.Channels)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(DELIMITER);
                    }

                    sb.Append(channel);
                }

                binaryWriter.Write(sb.ToString());
                binaryWriter.Flush();
            }
        }
    }
}
