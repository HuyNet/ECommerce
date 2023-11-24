using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;

namespace ViewModels.Catalog.Products.Private
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keywork { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
