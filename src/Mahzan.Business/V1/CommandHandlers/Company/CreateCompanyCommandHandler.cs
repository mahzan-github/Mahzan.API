using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mahzan.Business.V1.Commands.Company;
using Mahzan.Business.V1.Exceptions.Company.CreateCompany;
using Mahzan.Persistance.V1.Dto.Company;
using Mahzan.Persistance.V1.Repositories.Company;
using Mahzan.Persistance.V1.ViewModel.Company;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Mahzan.Business.V1.CommandHandlers.Company
{
    public class CreateCompanyCommandHandler
    :CommandHandlerBase<CreateCompanyCommand,CreateCompanyViewModel>, ICreateCompanyCommandHandler
    {
        private readonly ICreateCompanyRepository _createCompanyRepository;
        
        public CreateCompanyCommandHandler(
            NpgsqlConnection connection, 
            ILogger<CommandHandlerBase<CreateCompanyCommand, CreateCompanyViewModel>> logger, 
            ICreateCompanyRepository createCompanyRepository) 
            : base(connection, logger)
        {
            _createCompanyRepository = createCompanyRepository;
        }

        protected override async Task<CreateCompanyViewModel> HandleTransaction(
            CreateCompanyCommand command)
        {
            var companyInserted  =await _createCompanyRepository
                .Insert(new CreateCompanyDto
                {
                    CompanyDto = new CompanyDto()
                    {
                        RFC = command.CompanyCommand.RFC,
                        CURP = command.CompanyCommand.CURP,
                        CommercialName = command.CompanyCommand.CommercialName,
                        BusinessName = command.CompanyCommand.BusinessName,
                        Email  = command.CompanyCommand.Email,
                        TaxRegimeCodeId = command.CompanyCommand.TaxRegimeCodeId,
                        OfficePhone = command.CompanyCommand.OfficePhone,
                        MobilePhone = command.CompanyCommand.MobilePhone,
                        AdditionalInformation = command.CompanyCommand.AdditionalInformation,
                        MemberId = command.CompanyCommand.MemberId
                    },
                    CompanyAdressesDto = command
                        .CompanyAdressesCommand
                        .Select(a => new CompanyAdressDto
                        {
                            AdressType = a.AdressType,
                            Street = a.Street,
                            ExteriorNumber = a.ExteriorNumber,
                            InternalNumber = a.InternalNumber,
                            PostalCode = a.PostalCode
                        })
                        .ToList()
                });

            return CreateCompanyViewModel.From(companyInserted);
        }

        protected override Task HandlePrevalidations(CreateCompanyCommand command)
        {
            if (!IsRfcValid(command.CompanyCommand.RFC))
            {
                throw new CreateCompanyeventHandlerArgumentException(
                    $"El RFC {command.CompanyCommand.RFC} no es válido."
                );
            }
            
            return Task.CompletedTask;
        }
        
        #region :: Prevalidations ::
        
        private static bool IsRfcValid(string rfc) 
        {
            return Regex.IsMatch(rfc,
                @"^(([ÑA-Z|ña-z|&]{3}|[A-Z|a-z]{4})\d{2}((0[1-9]|1[012])(0[1-9]|1\d|2[0-8])|(0[13456789]|1[012])(29|30)|(0[13578]|1[02])31)(\w{2})([A|a|0-9]{1}))$|^(([ÑA-Z|ña-z|&]{3}|[A-Z|a-z]{4})([02468][048]|[13579][26])0229)(\w{2})([A|a|0-9]{1})$",
                RegexOptions.IgnoreCase);
        }
        
        #endregion
    }
}
