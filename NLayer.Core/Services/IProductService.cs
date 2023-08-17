using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductService:IService<Product>
    {
        Task<List<ProductWithCategoryDto>> GetProductsWithCategoryAsync();
    }
}
