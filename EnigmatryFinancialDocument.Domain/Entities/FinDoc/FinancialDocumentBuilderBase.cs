namespace EnigmatryFinancialDocument.Core.Entities.FinDoc
{


    public abstract class FinancialDocumentBuilderBase
    {
        protected FinancialDocumentBuilderBase(FinancialDocument document)
        {
            Document = document;
        }

        protected FinancialDocument Document { get; set; }

        public virtual void SetAccountNumber(string accountNumber) => Document.AccountNumber = accountNumber;

        public virtual void SetBalance(decimal Balance) => Document.Balance = Balance;

        public virtual void SetCurrency(string currency) => Document.Currency = currency;
        public virtual void SetDocumentId(Guid documentId) => Document.DocumentId = documentId;

        public virtual void SetTenantId(Guid tenantId) => Document.TenantId = tenantId;

        public virtual void SetProductCode(string accountNumber) => Document.ProductCode = accountNumber;



        public virtual void SetTransactions(List<Transaction> transactions) => Document.Transactions = transactions;

        public FinancialDocument GetFinancialDocument() => Document;


        public virtual FinancialDocument Build(FinancialDocument document)
        {
            SetAccountNumber(document.AccountNumber);
            SetBalance(document.Balance);
            SetCurrency(document.Currency);
            SetDocumentId(document.DocumentId);
            SetProductCode(document.ProductCode);
            SetTenantId(document.TenantId);

            SetTransactions(document.Transactions);
            return GetFinancialDocument();
        }
    }
}