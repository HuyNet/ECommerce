using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Catalog.Products.Private
{
    public class ProductUpdateRequest
    {
        public int ProductId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string LanguageId { set; get; }
        public string SeoAlias { get; set; }
        public DateTime updateDate { get; set; }
        public IFormFile Thumbnailimage { set; get; }
    }
}
