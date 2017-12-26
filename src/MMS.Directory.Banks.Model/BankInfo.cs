using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMS.Directory.Banks.Model
{
    [DataContract]
    public class BankInfo : BankHeader
    {
        [DataMember(IsRequired = true, Name = "is_active")]
        public bool IsActive { get; set; }

        [DataMember(Name = "credit_articles")]
        public List<CreditKindArticle> CreditArticles { get; set; }
    }
}