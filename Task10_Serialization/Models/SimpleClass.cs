using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Models
{
    [Serializable]
    public class SimpleClass : ISerializable
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public SimpleClass()
        {

        }

        protected SimpleClass(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("NameProp");
            Count = info.GetInt32("CountProp");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("NameProp", Name);
            info.AddValue("CountProp", Count);
        }
    }
}
