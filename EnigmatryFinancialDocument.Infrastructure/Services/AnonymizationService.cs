using EnigmatryFinancialDocument.Core.Entities;
using System.Security.Cryptography;
using System.Text;

namespace EnigmatryFinancialDocument.Infrastructure.Services
{
    public class AnonymizationService : IAnonymizationService
    {
        public FinancialDocument Anonymize(FinancialDocument document)
        {
            document.AccountNumber = Hash(document.AccountNumber);
            foreach (var transaction in document.Transactions)
            {
                transaction.TransactionId = "#####";
                transaction.Description = "#####";
            }
            return document;
        }

        private string Hash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}