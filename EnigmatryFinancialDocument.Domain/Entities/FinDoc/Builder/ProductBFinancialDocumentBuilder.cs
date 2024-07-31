using EnigmatryFinancialDocument.Core.Entities.FinDoc.ProductB;

namespace EnigmatryFinancialDocument.Core.Entities.FinDoc.Builder
{
    public class ProductBFinancialDocumentBuilder : FinancialDocumentBuilderBase
    {
        public ProductBFinancialDocumentBuilder():base(new ProductBFinancialDocument())
        {
        }

        public override ProductBFinancialDocument Build(FinancialDocument document)
        {
            ((ProductBFinancialDocument)base.Build(document)).AdditionalFieldB = "AdditionalFieldB value";
            return (ProductBFinancialDocument)GetFinancialDocument();
        }

    }
}