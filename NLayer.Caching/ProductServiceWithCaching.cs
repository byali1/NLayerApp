﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using NLayer.Service.Services;

namespace NLayer.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;

            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _productRepository.GetProductsWithCategoryAsync().Result);
            }
        }
        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return Task.FromResult(_memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).Any());
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {

            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            return Task.FromResult(products);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) not found");
            }

            return Task.FromResult(product);
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);

            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);

            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsWithCategoryDto));
        }

        public async Task RemoveAsync(Product entity)
        {
            _productRepository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _productRepository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }
        public async Task<Product> AddAsync(Product entity)
        {
            await _productRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _productRepository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entities;
        }





        public async Task CacheAllProductsAsync()
        {
            _memoryCache.Set(CacheProductKey, await _productRepository.GetAll().ToListAsync());
        }
    }
}
