using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OtomatMachine.Bussiness.Repositories.Product;
using OtomatMachine.Bussiness.Repositories.ProductType;
using OtomatMachine.Entity.Dtos;
using OtomatMachine.Shared.Utilities.Results.Abstract;
using OtomatMachine.Shared.Utilities.Results.ComplexTypes;
using OtomatMachine.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OtomatMachine.Shared.Enum.Enum;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly IConfiguration _configuration;
        public ProductService(ILogger<ProductService> logger, IConfiguration configuration, IProductRepository ProductRepository, IProductTypeRepository productTypeRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _ProductRepository = ProductRepository;
            _productTypeRepository = productTypeRepository;
        }
        public async Task<IDataResult<EF.Product>> AddAsync(EF.Product Product)
        {
            try
            {
                _logger.LogInformation("ProductService.AddAsync çalıştı.{@product}", Product);

                if (await _ProductRepository.Any(a => a.Name == Product.Name  && a.Status == Status.Active))
                {
                    _logger.LogWarning("ProductService.AddAsync Aynı ürün sistemde mevcut.{@product}", Product);
                    return new DataResult<EF.Product>(ResultStatus.Warning, "Aynı ürün sistemde mevcut", Product);
                }

                var productType = await _productTypeRepository.GetByParam(a => a.Id == Product.ProductTypeId);
                var productSlotCount = _ProductRepository.GetAll(a => a.ProductTypeId == Product.ProductTypeId && a.Status==Status.Active)?.Count();
                if (productType==null)
                {
                    _logger.LogError("ProductService.AddAsync Sistemde ürün tipi mevcut değil.{@product}", Product);

                    return new DataResult<EF.Product>(ResultStatus.Error, "Sistemde ürün tipi mevcut değil.", Product);
                }
                if (productSlotCount <= productType?.SlotCount)
                {
                    var result = await _ProductRepository.Add(Product);
                    _logger.LogInformation("ProductService.AddAsync ürün başarılı bir şekilde eklendi.{@result}", result);

                    return new DataResult<EF.Product>(ResultStatus.Success, result);
                }
                else 
                {
                    _logger.LogWarning("ProductService.AddAsync Eklenmek istenen ürünün slot sayısına ulaştığı için yeni ürün eklenemektedir.{@product}", Product);

                    return new DataResult<EF.Product>(ResultStatus.Warning, "Eklenmek istenen ürünün slot sayısına ulaştığı için yeni ürün eklenemektedir.", Product);

                }
              

            }
            catch (TimeoutException ex)
            {
                _logger.LogError("ProductService.AddAsync TimeoutException occured!!!.{@product},{@exception}", Product, ex);

                return new DataResult<EF.Product>(ResultStatus.Error, "TimeoutException occured!!!", Product, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductService.AddAsync Exception occured!!!.{@product},{@exception}", Product, ex);

                return new DataResult<EF.Product>(ResultStatus.Error, "Exception occured!!!", Product, ex);
            }
        }

        public async Task<IDataResult<EF.Product>> DeleteAsync(int Id)
        {
            _logger.LogInformation("ProductService.DeleteAsync çalıştı.{@Id}", Id);

            var entity = new EF.Product();
            try
            {
                entity = _ProductRepository.GetByParam(a => a.Id == Id).Result;
                if (entity == null)
                {
                    _logger.LogError("ProductService.DeleteAsync Silmek istediğiniz ürün sistemde bulunamadı.{@Id}", Id);

                    return new DataResult<EF.Product>(ResultStatus.Error, "Silmek istediğiniz ürün sistemde bulunamadı.", new EF.Product { Id = Id });

                }
                if (entity.Status == Status.Passive)
                {
                    _logger.LogError("ProductService.DeleteAsync Silmek istediğiniz ürün pasiftedir.{@Id}", Id);

                    return new DataResult<EF.Product>(ResultStatus.Warning, "Silmek istediğiniz ürün pasiftedir.Tekrar silemezsiniz", new EF.Product { Id = Id });
                }
                entity.Status = Status.Passive;
                _logger.LogInformation("ProductService.DeleteAsync entity için update  çalıştı.{@entity}", entity);
              
                var result = await _ProductRepository.Update(entity);
                _logger.LogInformation("ProductService.DeleteAsync entity Başarılı ile pasife çekild.{@entity}", entity);

                return new DataResult<EF.Product>(ResultStatus.Success,"Başarılı ile pasife çekildi", entity);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("ProductService.DeleteAsync entity TimeoutException occured!!.{@entity}", entity);

                return new DataResult<EF.Product>(ResultStatus.Error, "TimeoutException occured!!!", entity, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductService.DeleteAsync entity Exception occured!!.{@entity}", entity);

                return new DataResult<EF.Product>(ResultStatus.Error, "Exception occured!!!", entity, ex);
            }
        }

        public async Task<IDataResult<List<EF.Product>>> GetList(ProductListDTO productList)
        {
            try
            {
                _logger.LogInformation("PaymentTypeService.GetList çalıştı.{@productList}", productList);

                return new DataResult<List<EF.Product>>(ResultStatus.Success, await _ProductRepository.GetAll(a => (productList.Status == Status.All || a.Status == productList.Status) && (productList.ProductTypeId == 0 || a.ProductTypeId == productList.ProductTypeId),a=>a.ProductType).ToListAsync());
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("PaymentTypeService.GetList entity TimeoutException occured!!.{@productList}", productList);

                return new DataResult<List<EF.Product>>(ResultStatus.Error, "TimeoutException occured!!!", new List<EF.Product>(), ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("PaymentTypeService.GetList entity Exception occured!!.{@productList}", productList);

                return new DataResult<List<EF.Product>>(ResultStatus.Error, "Exception occured!!!", new List<EF.Product>(), ex);
            }
        }
    }
}