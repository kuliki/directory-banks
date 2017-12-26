using System.Runtime.Serialization;

namespace MMS.Directory.Banks.Model
{
    [DataContract]
    public class BankHeader
    {
        [DataMember(IsRequired = true, Name = "oid")]
        public string Oid { get; set; }

        [DataMember(IsRequired = true, Name = "name")]
        public string Name { get; set; }
    }
}
