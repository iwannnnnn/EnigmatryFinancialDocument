using Bogus;
using Bogus.Extensions.UnitedStates;
using EnigmatryFinancialDocument.Core.Entities;
using EnigmatryFinancialDocument.Core.Entities.FinDoc.ProductA;
using EnigmatryFinancialDocument.Core.Entities.FinDoc.ProductB;

namespace EnigmatryFinancialDocument.Infrastructure
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;

        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            var tenants = GenerateTenants(100);
            _context.Tenants.AddRange(tenants);

            var products = GenerateProducts();
            _context.Products.AddRange(products);

            var clients = GenerateClients(tenants);
            _context.Clients.AddRange(clients);

            var financialDocuments = GenerateFinancialDocuments(tenants.Where(t=>t.IsInWhitelist), clients.Where(c=>c.IsInWhitelist), products.Where(p => p.IsSupported));
            _context.FinancialDocuments.AddRange(financialDocuments);

            _context.SaveChanges();
        }

        private List<Tenant> GenerateTenants(int count)
            => new Faker<Tenant>()
                .RuleFor(t => t.TenantId, f => Guid.NewGuid())
                .RuleFor(t => t.Name, f => f.Company.CompanyName())
                .RuleFor(t => t.Description, f => f.Random.AlphaNumeric(100))
            .RuleFor(c => c.IsInWhitelist, f => f.Random.Bool()).Generate(count);

        private List<Product> GenerateProducts()
            => new()
            {
                new() { ProductCode = "ProductA", Name = "Product A" ,Description="Product A",IsSupported=true},
                new() { ProductCode = "ProductB", Name = "Product B",Description="Product B" ,IsSupported=true},
                new() { ProductCode = "ProductC", Name = "Product C" ,Description="Product C", IsSupported = false}
            };

        private List<Client> GenerateClients(List<Tenant> tenants)
            => new Faker<Client>()
                .RuleFor(c => c.ClientId, f => Guid.NewGuid())
                .RuleFor(c => c.TenantId, f => f.PickRandom(tenants).TenantId)
                .RuleFor(c => c.ClientVAT, f => f.Company.Ein())
                .RuleFor(c => c.RegistrationNumber, f => f.Random.AlphaNumeric(10))
                .RuleFor(c => c.CompanyType, f => f.PickRandom(new[] { "small", "medium", "large" }))
                .RuleFor(c => c.IsInWhitelist, f => f.Random.Bool()).Generate(10);

        private List<FinancialDocument> GenerateFinancialDocuments(IEnumerable<Tenant> tenants, IEnumerable<Client> clients, IEnumerable<Product> products)
        {
            var documents = new Faker<FinancialDocument>()
                .RuleFor(fd => fd.DocumentId, f => Guid.NewGuid())
                .RuleFor(fd => fd.TenantId, f => f.PickRandom(tenants).TenantId)
                .RuleFor(fd => fd.ClientId, f => f.PickRandom(clients).ClientId)
                .RuleFor(fd => fd.ProductCode, f => f.PickRandom(products).ProductCode)
                .RuleFor(fd => fd.AccountNumber, f => f.Finance.Account())
                .RuleFor(fd => fd.Balance, f => f.Finance.Amount())
                .RuleFor(fd => fd.Currency, f => f.Finance.Currency().Code)
                .RuleFor(fd => fd.Transactions, f => GenerateTransactions()).Generate(20);

            foreach (var doc in documents)
            {
                if (doc is ProductAFinancialDocument productADoc)
                {
                    productADoc.ProductCode = "ProductA";
                }
                else if (doc is ProductBFinancialDocument productBDoc)
                {
                    productBDoc.ProductCode = "ProductB";
                    productBDoc.AdditionalFieldB = "Some additional field value for Product B";
                }
            }

            return documents;
        }

        private List<Transaction> GenerateTransactions()
            => new Faker<Transaction>()
                .RuleFor(t => t.TransactionId, f => Guid.NewGuid().ToString())
                .RuleFor(t => t.Amount, f => f.Finance.Amount())
                .RuleFor(t => t.Date, f => f.Date.Past())
                .RuleFor(t => t.Description, f => f.Commerce.ProductName())
                .RuleFor(t => t.Category, f => f.Commerce.Categories(1)[0]).Generate(5);
    }
}