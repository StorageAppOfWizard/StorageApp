using Ardalis.Result;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Extensions
{
    public static class ManagementStockExtension
    {
        public static async Task<Result> RestoreProductStock(this Order order, Product product)
        {
            product.Quantity += order.QuantityProduct;
            return Result.Success();
        }
        public static async Task<Result> ReserveProductStock(this Order order, Product product)
        {
            if (product.Quantity < order.QuantityProduct)
                return Result.Error("Insufficient Stock");

            product.Quantity -= order.QuantityProduct;
            return Result.Success();
        }


    }
}
