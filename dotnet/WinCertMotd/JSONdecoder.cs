using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace WinCertMotd
{
    [DataContract]
    public class RuleSet
    {
        [DataMember(Name = "rules")]
        public Rule[] rules { get; set; }
    }

    [DataContract]
    public class Rule
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "num")]
        public string Number { get; set; }

        [DataMember(Name = "name")]
        public string Name{ get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "section")]
        public string Section { get; set; }

        [DataMember(Name = "sec-num")]
        public string SectionNumber { get; set; }
    }
}
