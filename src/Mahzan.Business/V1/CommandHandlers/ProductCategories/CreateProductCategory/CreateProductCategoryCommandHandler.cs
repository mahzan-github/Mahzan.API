using System.Threading.Tasks;
using Mahzan.Business.V1.Commands.ProductCategories;
using Mahzan.Persistance.V1.Dto.ProductCategories;
using Mahzan.Persistance.V1.Repositories.ProductCategories.CreateProductCategory;
using Mahzan.Persistance.V1.ViewModel.ProductCategories;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Mahzan.Business.V1.CommandHandlers.ProductCategories.CreateProductCategory
{
    public class CreateProductCategoryCommandHandler
        :CommandHandlerBase<CreateProductCategoryCommand, CreateProductCategoryViewModel>, 
            ICreateProductCategoryCommandHandler
    {
        private readonly ICreateProductCategoryRepository _createProductCategoryRepository;
        
        public CreateProductCategoryCommandHandler(
            NpgsqlConnection connection, 
            ILogger<CommandHandlerBase<CreateProductCategoryCommand, CreateProductCategoryViewModel>> logger, 
            ICreateProductCategoryRepository createProductCategoryRepository) 
            : base(connection, logger)
        {
            _createProductCategoryRepository = createProductCategoryRepository;
        }

        protected override async Task<CreateProductCategoryViewModel> HandleTransaction(
            CreateProductCategoryCommand command)
        {
            CreateProductCategoryDto createProductCategoryDto =await _createProductCategoryRepository
                .Insert(new CreateProductCategoryDto
                {
                    CodeCategory = command.CodeCategory,
                    Description = command.Description,
                    CompanyId = command.CompanyId
                });

            return CreateProductCategoryViewModel.From(createProductCategoryDto);
        }
    }
}