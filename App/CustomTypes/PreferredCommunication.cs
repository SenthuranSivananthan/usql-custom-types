using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;

namespace CustomTypes
{
    [SqlUserDefinedType(typeof(PreferredCommunicationFormatter))]

    public class PreferredCommunication
    {
        public SqlArray<string> Channels { get; set; }

        public static PreferredCommunication ParseFrom(string input, char delimiter)
        {
            return new PreferredCommunication { Channels = new SqlArray<string>(input.Split(delimiter)) };
        }
    }
}