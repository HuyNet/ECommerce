using Microsoft.AspNetCore.Http;
using ViewModels.Catalog.Products;
using ViewModels.Catalog.Products.Private;
using ViewModels.Common;

namespace Application.Catalog.Products
{
    public interface IPrivateProductService
    {
        Task<int> Create (ProductCreateRequest request);
        Task<int> Update (ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addQuantity);
        Task AddViewCount(int productId);
        Task<PagedResult<ProductViewModel>> GetAllPagding(GetProductPagingRequest request);
        Task<int> AddImages(int productId,List<IFormFile> files);
        Task<int> UpdateImages(int imageId, string caption, bool isDerault);
        Task<int> DeleteImages(int imageId);
        Task<List<ProductImageViewModel>> GetListImages(int productId);
    }
}
