using Data.EF;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Utilities.Exceptions;
using ViewModels.Catalog.Products;
using ViewModels.Catalog.Products.Private;
using ViewModels.Common;
using Application.Common;
using Microsoft.Data.SqlClient;
using Serilog.Sinks.File;
using System;
using Azure.Core;

namespace Application.Catalog.Products
{
    public class PrivateProductService : IPrivateProductService
    {
        private readonly ECommerceDbContext _context;
        private readonly IStorageService _storageService;
        public PrivateProductService(ECommerceDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;   
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                CreatedDate = DateTime.Now,
                updateDate = null,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId,
                    }
                }

            };
            //save image
            if(request.Thumbnailimage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail Image",
                        CreateDate = DateTime.Now,
                        FileSize = request.Thumbnailimage.Length,
                        imagePath = await this.SaveFile(request.Thumbnailimage),
                        IsDedault=true,
                        SortOrder = 1,
                    }
                };
            }
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ECommerceException($"Cannot find a product: {productId}");
            var images = _context.ProductImages.Where(x=>x.ProductId == productId);
            foreach(var image in images) 
            {
               await _storageService.DeleteFileAsync(image.imagePath);
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();

        }

        public async Task<PagedResult<ProductViewModel>> GetAllPagding(GetProductPagingRequest request)
        {
            //1.join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategories on p.ProductId equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.CategoryId
                        select new { p, pt, pic };

            //2.filter
            if (!string.IsNullOrEmpty(request.Keywork))
                query = query.Where(x => x.pt.Name.Contains(request.Keywork));
            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(x => request.CategoryIds.Contains(x.pic.CategoryId));
            }

            //3.paging and select
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ProductId = x.p.ProductId,
                    Name = x.pt.Name,
                    CreatedDate = x.p.CreatedDate,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    updateDate = (DateTime)x.p.updateDate,
                }).ToListAsync();

            //4. projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,
            };
            return pagedResult;

        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.ProductId
            && x.ProductId == request.ProductId);
            if (product == null || productTranslations == null) throw new ECommerceException($"Cannot find a product: {request.ProductId}");

            productTranslations.Name = request.Name;
            productTranslations.Description = request.Description;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Details = request.Details;
            productTranslations.SeoAlias = request.SeoAlias;
            product.updateDate = DateTime.Now;


            if (request.Thumbnailimage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDedault == true && i.ProductId == request.ProductId);
                if(thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.Thumbnailimage.Length;
                    thumbnailImage.imagePath = await this.SaveFile(request.Thumbnailimage);
                    _context.ProductImages.Update(thumbnailImage);   
                }               
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ECommerceException($"Cannot find a product: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ECommerceException($"Cannot find a product: {productId}");
            product.OriginalPrice = addQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<int> AddImages(int productId, List<IFormFile> files)
        {
            throw new NotImplementedException();
        }
        public Task<int> DeleteImages(int imageId)
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateImages(int imageId, string caption, bool isDerault)
        {
            throw new NotImplementedException();
        }
        public Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            throw new NotImplementedException();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}.{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
