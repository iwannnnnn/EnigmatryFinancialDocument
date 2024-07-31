using EnigmatryFinancialDocument.Core.Entities.FinDoc.Builder;

namespace EnigmatryFinancialDocument.Core.Entities.FinDoc
{
    public enum BuilderSupportedProductCode
    {
        ProductA,
        ProductB
    }

    public static class FinancialDocumentFactory
    {
        private static readonly Dictionary<BuilderSupportedProductCode, Func<FinancialDocumentBuilderBase>> Builders
            = new()
            {
                {
                    BuilderSupportedProductCode.ProductA,
                    () => new ProductAFinancialDocumentBuilder()
                },
                {
                    BuilderSupportedProductCode.ProductB,
                    () => new ProductBFinancialDocumentBuilder()
                }
            };
    

        public static FinancialDocumentBuilderBase CreateBuilder(BuilderSupportedProductCode productCode)
            => Builders.TryGetValue(productCode, out var builderFunc)
                ? builderFunc()
                : throw new ArgumentException($"Unsupported product code: {productCode}");
    }
}