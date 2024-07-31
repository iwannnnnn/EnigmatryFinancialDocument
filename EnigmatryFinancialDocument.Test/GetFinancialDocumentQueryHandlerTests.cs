using AutoMapper;
using Bogus.DataSets;
using EnigmatryFinancialDocument.Application.Handlers.FinancialDocument.Query;
using EnigmatryFinancialDocument.Core.DTOs;
using EnigmatryFinancialDocument.Core.Entities;
using EnigmatryFinancialDocument.Core.Exceptions;
using EnigmatryFinancialDocument.Core.Interfaces.Repositories;
using EnigmatryFinancialDocument.Infrastructure.Services;
using Moq;
using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace EnigmatryFinancialDocument.Test
{
    public class GetFinancialDocumentQueryHandlerTests
    {
        private readonly Mock<IFinancialDocumentRepository> _financialDocumentRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<IFinancialDocumentService> financialDocumentServiceMock;
        private readonly Mock<IProductService> productServiceMock;
        private readonly Mock<ITenantService> tenantServiceMock;
        private readonly Mock<IClientService> clientServiceMock;
        private readonly GetFinancialDocumentQueryHandler _handler;

        public GetFinancialDocumentQueryHandlerTests()
        {
            _financialDocumentRepositoryMock = new Mock<IFinancialDocumentRepository>();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FinancialDocument, FinancialDocumentDto>();
                cfg.CreateMap<Company, CompanyDto>();
            });
            _mapper = configuration.CreateMapper();
             financialDocumentServiceMock = new Mock<IFinancialDocumentService>();
             productServiceMock = new Mock<IProductService>();
             tenantServiceMock = new Mock<ITenantService>();
            clientServiceMock = new Mock<IClientService>();
            _handler = new GetFinancialDocumentQueryHandler(
                financialDocumentServiceMock.Object,
                productServiceMock.Object,
                tenantServiceMock.Object,
                clientServiceMock.Object);
        }

   

        [Fact]
        public async Task Handle_ReturnsFinancialDocumentResponse()
        {
            // Arrange
            var financialDocumentServiceMock = new Mock<IFinancialDocumentService>();
            var productServiceMock = new Mock<IProductService>();
            var tenantServiceMock = new Mock<ITenantService>();
            var clientServiceMock = new Mock<IClientService>();
            var docId = Guid.NewGuid();
            var handler = new GetFinancialDocumentQueryHandler(
                financialDocumentServiceMock.Object,
                productServiceMock.Object,
                tenantServiceMock.Object,
                clientServiceMock.Object);

            var query = new GetFinancialDocumentQuery
            {
                ProductCode = "TestProduct",
                TenantId = Guid.NewGuid(),
                DocumentId = docId
            };

            var financialDocument = new FinancialDocument { DocumentId = docId };
            var registrationNumber = "123456";
            var companyType = "LLC";

            productServiceMock.Setup(x => x.ValidateProductAsync(query.ProductCode))
                .Returns(Task.CompletedTask);

            tenantServiceMock.Setup(x => x.IsTenantWhitelistedAsync(query.TenantId))
                .Returns(Task.CompletedTask);

            clientServiceMock.Setup(x => x.GetValidateClient(query.TenantId, query.DocumentId))
                .ReturnsAsync((registrationNumber, companyType));

            financialDocumentServiceMock.Setup(x => x.GetFinancialDocumentAsync(query.TenantId, query.DocumentId, query.ProductCode))
                .ReturnsAsync(financialDocument);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(JsonConvert.SerializeObject(financialDocument), result.Data);
            Assert.Equal(companyType, result.Company.CompanyType);
            Assert.Equal(registrationNumber, result.Company.RegistrationNumber);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenProductValidationFails()
        {
            // Arrange
            var financialDocumentServiceMock = new Mock<IFinancialDocumentService>();
            var productServiceMock = new Mock<IProductService>();
            var tenantServiceMock = new Mock<ITenantService>();
            var clientServiceMock = new Mock<IClientService>();

            var handler = new GetFinancialDocumentQueryHandler(
                financialDocumentServiceMock.Object,
                productServiceMock.Object,
                tenantServiceMock.Object,
                clientServiceMock.Object);

            var query = new GetFinancialDocumentQuery
            {
                ProductCode = "TestProduct",
                TenantId = Guid.NewGuid(),
                DocumentId = Guid.NewGuid()
            };

            productServiceMock.Setup(x => x.ValidateProductAsync(query.ProductCode))
                .ThrowsAsync(new Exception("Product validation failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenTenantNotWhitelisted()
        {
            // Arrange
            var financialDocumentServiceMock = new Mock<IFinancialDocumentService>();
            var productServiceMock = new Mock<IProductService>();
            var tenantServiceMock = new Mock<ITenantService>();
            var clientServiceMock = new Mock<IClientService>();

            var handler = new GetFinancialDocumentQueryHandler(
                financialDocumentServiceMock.Object,
                productServiceMock.Object,
                tenantServiceMock.Object,
                clientServiceMock.Object);

            var query = new GetFinancialDocumentQuery
            {
                ProductCode = "TestProduct",
                TenantId = Guid.NewGuid(),
                DocumentId = Guid.NewGuid()
            };

            productServiceMock.Setup(x => x.ValidateProductAsync(query.ProductCode))
                .Returns(Task.CompletedTask);

            tenantServiceMock.Setup(x => x.IsTenantWhitelistedAsync(query.TenantId))
                .ThrowsAsync(new Exception("Tenant not whitelisted"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenClientValidationFails()
        {
            // Arrange
            var financialDocumentServiceMock = new Mock<IFinancialDocumentService>();
            var productServiceMock = new Mock<IProductService>();
            var tenantServiceMock = new Mock<ITenantService>();
            var clientServiceMock = new Mock<IClientService>();

            var handler = new GetFinancialDocumentQueryHandler(
                financialDocumentServiceMock.Object,
                productServiceMock.Object,
                tenantServiceMock.Object,
                clientServiceMock.Object);

            var query = new GetFinancialDocumentQuery
            {
                ProductCode = "TestProduct",
                TenantId = Guid.NewGuid(),
                DocumentId = Guid.NewGuid()
            };

            productServiceMock.Setup(x => x.ValidateProductAsync(query.ProductCode))
                .Returns(Task.CompletedTask);

            tenantServiceMock.Setup(x => x.IsTenantWhitelistedAsync(query.TenantId))
                .Returns(Task.CompletedTask);

            clientServiceMock.Setup(x => x.GetValidateClient(query.TenantId, query.DocumentId))
                .ThrowsAsync(new Exception("Client validation failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ReturnsNull_WhenFinancialDocumentNotFound()
        {
            // Arrange
            var financialDocumentServiceMock = new Mock<IFinancialDocumentService>();
            var productServiceMock = new Mock<IProductService>();
            var tenantServiceMock = new Mock<ITenantService>();
            var clientServiceMock = new Mock<IClientService>();

            var handler = new GetFinancialDocumentQueryHandler(
                financialDocumentServiceMock.Object,
                productServiceMock.Object,
                tenantServiceMock.Object,
                clientServiceMock.Object);

            var query = new GetFinancialDocumentQuery
            {
                ProductCode = "TestProduct",
                TenantId = Guid.NewGuid(),
                DocumentId = Guid.NewGuid()
            };

            productServiceMock.Setup(x => x.ValidateProductAsync(query.ProductCode))
                .Returns(Task.CompletedTask);

            tenantServiceMock.Setup(x => x.IsTenantWhitelistedAsync(query.TenantId))
                .Returns(Task.CompletedTask);

            clientServiceMock.Setup(x => x.GetValidateClient(query.TenantId, query.DocumentId))
                .ReturnsAsync(("123456", "LLC"));

            financialDocumentServiceMock.Setup(x => x.GetFinancialDocumentAsync(query.TenantId, query.DocumentId, query.ProductCode))
                .ReturnsAsync((FinancialDocument)null);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data=="null");
        }
    }
}