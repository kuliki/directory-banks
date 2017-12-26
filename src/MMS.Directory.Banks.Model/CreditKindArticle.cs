using System.Runtime.Serialization;

namespace MMS.Directory.Banks.Model
{
    [DataContract]
    public class CreditKindArticle
    {
        [DataMember(IsRequired = true, Name = "article_no")]
        public int ArticleNo { get; set; }

        [DataMember(IsRequired = true, Name = "credit_kind")]
        public CreditKind CreditKind { get; set; }
    }
}