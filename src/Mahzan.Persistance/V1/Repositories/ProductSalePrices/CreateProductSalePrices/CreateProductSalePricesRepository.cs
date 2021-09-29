using System;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.ProductSalePrices;
using Mahzan.Persistance.V1.Exeptions.Products.CreateProduct;
using Mahzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.ProductSalePrices.CreateProductSalePrices
{
    public class CreateProductSalePricesRepository
        :BaseInsertRepository<CreateSalePricesDto>,
    ICreateProductSalePricesRepository
    {
        public CreateProductSalePricesRepository(
            NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task<CreateSalePricesDto> InsertInternal(
            CreateSalePricesDto dto)
        {
            await InsertInProductSalesPrices(dto);

            return dto;
        }

        #region :: Create Sale Prices Steps ::

        private async Task InsertInProductSalesPrices(
            CreateSalePricesDto dto)
        {
            foreach (var priceDto in dto.ListPriceDto)
            {
                string sql = @"
                insert into product_sale_prices
                (
                 product_sale_price_id,
                 price_type,
                 price,
                 cost,
                 utility,
                 utility_percentage,
                 product_id
                )
                values
                (
                 @product_sale_price_id,
                 @price_type,
                 @price,
                 @cost,
                 @utility,
                 @utility_percentage,
                 @product_id
                )
                returning product_sale_price_id;
                ";

                await Connection
                    .ExecuteScalarAsync<Guid>(
                        sql,
                        new
                        {
                            product_sale_price_id = Guid.NewGuid(),
                            price_type = priceDto.PriceTypeEnum.ToString(),
                            price = Math.Round((decimal)priceDto.Price, 2),
                            cost = Math.Round((decimal)priceDto.Cost, 2),
                            utility = Math.Round((decimal)(priceDto.Price - priceDto.Cost), 2),
                            utility_percentage = await CalculateUtilityPercentaje(priceDto.Price,priceDto.Cost),
                            product_id = dto.ProductId
                        }
                    );
            }
        }

        #endregion
        
        #region :: Private Metods ::

        private async Task<decimal> CalculateUtilityPercentaje(
            decimal price,
            decimal cost)
        {
            decimal result = 0;

            try
            {
                result = (((price - cost ) * 100)/price);
            }
            catch (Exception e)
            {
                throw new CreateProductInvalidOperationException(
                    $"Error al intentar calcular la utilidad en porcentaje {e.Message}"
                );
            }



            return Math.Round(result, 2);
        }

        #endregion
        
        
    }
}