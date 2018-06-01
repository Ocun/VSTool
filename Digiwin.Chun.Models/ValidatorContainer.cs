using System.Xml.Serialization;

namespace Digiwin.Chun.Models
{
    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/ValidatorContainerElement")]
    [XmlRoot(Namespace = "http://schemas.dcms.com/configuration/ValidatorContainerElement", IsNullable = false)]
    public class ValidatorContainer {
        /// <remarks />
        public Validators Validators { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/ValidatorContainerElement")]
    public class Validators {
        /// <remarks />
        [XmlElement("LambdaValidator", typeof(LambdaValidator))]
        [XmlElement("RequiredFieldValidator", typeof(RequiredFieldValidator))]
        public object[] Items { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/ValidatorContainerElement")]
    public class LambdaValidator {
        /// <remarks />
        public string Id { get; set; }

        /// <remarks />
        public string DisplayName { get; set; }

        /// <remarks />
        public string Path { get; set; }

        /// <remarks />
        public string Message { get; set; }

        /// <remarks />
        public string Description { get; set; }

        /// <remarks />
        public string ResponseType { get; set; }

        /// <remarks />
        public string Expression { get; set; }

        /// <remarks />
        public string MessageIsExpression { get; set; }

        /// <remarks />
        public string ShowMessageTo { get; set; }

        /// <remarks />
        [XmlArrayItem("ValidateActivePoint", IsNullable = false)]
        public ValidateActivePoint[] ActivePoints { get; set; }

        /// <remarks />
        public PreValidators PreValidators { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/ValidatorContainerElement")]
    public class ValidateActivePoint {
        /// <remarks />
        public string ActivePoint { get; set; }

        /// <remarks />
        public string Enabled { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/ValidatorContainerElement")]
    public class PreValidators {
        /// <remarks />
        public PreValidator PreValidator { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/ValidatorContainerElement")]
    public class PreValidator {
        /// <remarks />
        public string ValidatorId { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/ValidatorContainerElement")]
    public class RequiredFieldValidator {
        /// <remarks />
        public string Id { get; set; }

        /// <remarks />
        public string DisplayName { get; set; }

        /// <remarks />
        public string Path { get; set; }

        /// <remarks />
        public string Message { get; set; }

        /// <remarks />
        public string Description { get; set; }

        /// <remarks />
        public string ApplyTo { get; set; }

        /// <remarks />
        public string Expression { get; set; }

        /// <remarks />
        [XmlArrayItem("ValidateActivePoint", IsNullable = false)]
        public ValidateActivePoint[] ActivePoints { get; set; }
    }
    
}