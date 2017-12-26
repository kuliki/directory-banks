using System.Runtime.Serialization;

namespace MMS.Directory.Banks.Model
{
    [DataContract]
    public enum CreditKind
    {
        [EnumMember]
        None,

        [EnumMember]
        Promo
    }
}
