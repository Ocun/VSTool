using System.Xml.Serialization;

namespace Digiwin.Chun.Models {

    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/FormulaContainerElement")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.dcms.com/configuration/FormulaContainerElement", IsNullable = false)]
    public partial class FormulaContainer
    {
        /// <remarks/>
        public Formulas Formulas { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FileVersion { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ProductVersion { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ProductName { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CopyRight { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/FormulaContainerElement")]
    public partial class Formulas
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LambdaFormula", typeof(LambdaFormula))]
        [System.Xml.Serialization.XmlElementAttribute("LambdaFormulaGroup", typeof(LambdaFormulaGroup))]
        [System.Xml.Serialization.XmlElementAttribute("ReversibleLambdaFormula", typeof(ReversibleLambdaFormula))]
        [System.Xml.Serialization.XmlElementAttribute("ControlLambdaFormula", typeof(ControlLambdaFormula))]
        public object[] Items { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/FormulaContainerElement")]
    public partial class LambdaFormula
    {
        /// <remarks/>
        public string Id { get; set; }

        /// <remarks/>
        public string DisplayName { get; set; }

        /// <remarks/>
        public string Path { get; set; }

        /// <remarks/>
        public string Description { get; set; }

        /// <remarks/>
        public string Target { get; set; }

        /// <remarks/>
        public string Expression { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FormulaActivePoint", IsNullable = false)]
        public FormulaActivePoint[] ActivePoints { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/FormulaContainerElement")]
    public partial class FormulaActivePoint
    {
        /// <remarks/>
        public string ActivePoint { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/FormulaContainerElement")]
    public partial class LambdaFormulaGroup
    {
        /// <remarks/>
        public string Id { get; set; }

        /// <remarks/>
        public string DisplayName { get; set; }

        /// <remarks/>
        public string Path { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FormulaItem", IsNullable = false)]
        public FormulaItem[] Items { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/FormulaContainerElement")]
    public partial class FormulaItem
    {
        /// <remarks/>
        public string Id { get; set; }

        /// <remarks/>
        public string DisplayName { get; set; }

        /// <remarks/>
        public string Description { get; set; }

        /// <remarks/>
        public string DependencyItems { get; set; }

        /// <remarks/>
        public string Target { get; set; }

        /// <remarks/>
        public string Expression { get; set; }


        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FormulaActivePoint", IsNullable = false)]
        public FormulaActivePoint[] ActivePoints { get; set; }
    }
    


    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/FormulaContainerElement")]
    public partial class ReversibleLambdaFormula
    {
        /// <remarks/>
        public string Id { get; set; }

        /// <remarks/>
        public string DisplayName { get; set; }

        /// <remarks/>
        public string Path { get; set; }

        /// <remarks/>
        public string Target { get; set; }

        /// <remarks/>
        public string Expression { get; set; }

        /// <remarks/>
        public string ReverseTarget { get; set; }

        /// <remarks/>
        public string ReverseExpression { get; set; }


        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FormulaActivePoint", IsNullable = false)]
        public FormulaActivePoint[] ActivePoints { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/FormulaContainerElement")]
    public partial class ControlLambdaFormula
    {
        /// <remarks/>
        public string Control { get; set; }
        /// <remarks/>
        public string Id { get; set; }

        /// <remarks/>
        public string DisplayName { get; set; }
        

        /// <remarks/>
        public string Target { get; set; }

        /// <remarks/>
        public string Expression { get; set; }
        


        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FormulaActivePoint", IsNullable = false)]
        public FormulaActivePoint[] ActivePoints { get; set; }
    }

}