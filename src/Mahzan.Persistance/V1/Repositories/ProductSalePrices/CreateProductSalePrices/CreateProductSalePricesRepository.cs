using System;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Persistance.V1.Dto.ProductSalePrices;
using Mahzan.Persistance.V1.Repositories._Base;
using Mahzan.Persistance.V1.Repositories.ProductSaleTaxes.CreateProductSaleTax;
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

        protected override Task<CreateSalePricesDto> InsertInternal(
            CreateSalePricesDto dto)
        {
            throw new System.NotImplementedException();
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
                 price_purchase,
                 price_net,
                 price_purchase_unit_without_taxes,
                 price_sale_unit_without_taxes,
                 utility,
                 price,
                 cost,
                 product_id
                )
                values
                (
                 @product_sale_price_id,
                 @price_type,
                 @price_purchase,
                 @price_net,
                 @price_purchase_unit_without_taxes,
                 @price_sale_unit_without_taxes,
                 @utility,
                 @price,
                 @cost,
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
                            price_purchase = priceDto.PricePurchase,
                            price_net = priceDto.PriceNet,
                            price_purchase_unit_without_taxes = priceDto.PricePurchaseUnitWitoutTaxes,
                            price_sale_unit_without_taxes = priceDto.PriceSaleUnitWitoutTaxes,
                            utility = priceDto.Utility,
                            price = priceDto.Price,
                            cost = priceDto.Cost,
                            product_id = dto.ProductId
                        }
                    );
            }
        }

        #endregion
    }
}