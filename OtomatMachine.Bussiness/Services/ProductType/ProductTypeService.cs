using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OtomatMachine.Bussiness.Repositories.ProductType;
using OtomatMachine.Shared.Utilities.Results.Abstract;
using OtomatMachine.Shared.Utilities.Results.ComplexTypes;
using OtomatMachine.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OtomatMachine.Shared.Enum.Enum;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.ProductType
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _ProductTypeRepository;
        private readonly ILogger<ProductTypeService> _logger;
        private readonly IConfiguration _configuration;

        public ProductTypeService(ILogger<ProductTypeService> logger, IConfiguration configuration, IProductTypeRepository ProductTypeRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _ProductTypeRepository = ProductTypeRepository;
        }
        public async Task<IDataResult<EF.ProductType>> AddAsync(EF.ProductType ProductType)
        {
            try
            {
                _logger.LogInformation("ProductTypeService.AddAsync çalıştı.{@productType}", ProductType);
                if (await _ProductTypeRepository.Any(a => a.Name == ProductType.Name  && a.Status == Status.Active))
                {
                    _logger.LogWarning("ProductTypeService.AddAsync aynı ürün tipinden sistemde mevcut.{@productType}", ProductType);
                    return new DataResult<EF.ProductType>(ResultStatus.Warning, "Aynı ürün tipinden sistemde mevcut", ProductType);
                }
                var result = await _ProductTypeRepository.Add(ProductType);
                _logger.LogInformation("ProductTypeService.AddAsync ürün tipi başarılı bir şekilde eklendi.{@result}", result);
                return new DataResult<EF.ProductType>(ResultStatus.Success, result);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("ProductTypeService.AddAsync TimeoutException occured!!!.{@productType},{@exception}", ProductType, ex);

                return new DataResult<EF.ProductType>(ResultStatus.Error, "TimeoutException occured!!!", ProductType, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductTypeService.AddAsync Exception occured!!!.{@productType},{@exception}", ProductType, ex);

                return new DataResult<EF.ProductType>(ResultStatus.Error, "Exception occured!!!", ProductType, ex);
            }
        }

        public async Task<IDataResult<EF.ProductType>> DeleteAsync(int Id)
        {
            _logger.LogInformation("ProductTypeService.DeleteAsync çalıştı.{@Id}", Id);

            var entity = new EF.ProductType();
            try
            {
                entity = _ProductTypeRepository.GetByParam(a => a.Id == Id).Result;
                if (entity == null)
                {
                    _logger.LogError("ProductTypeService.DeleteAsync Silmek istediğiniz ürün tipi sistemde bulunamadı.{@Id}", Id);

                    return new DataResult<EF.ProductType>(ResultStatus.Error, "Silmek istediğiniz ürün tipi sistemde bulunamadı.", new EF.ProductType { Id = Id });

                }

                if (entity.Status == Status.Passive)
                {
                    _logger.LogError("ProductService.DeleteAsync Silmek istediğiniz ürün tipi pasiftedir.{@Id}", Id);

                    return new DataResult<EF.ProductType>(ResultStatus.Warning, "Silmek istediğiniz ürün tipi pasiftedir.Tekrar silemezsiniz", new EF.ProductType { Id = Id });
                }
                entity.Status = Status.Passive;
                _logger.LogInformation("ProductTypeService.DeleteAsync entity için update  çalıştı.{@entity}", entity);

                var result = await _ProductTypeRepository.Update(entity);
                _logger.LogInformation("ProductTypeService.DeleteAsync entity Başarılı ile pasife çekild.{@entity}", entity);

                return new DataResult<EF.ProductType>(ResultStatus.Success,"Başarılı ile pasife çekildi", entity);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("ProductTypeService.DeleteAsync entity TimeoutException occured!!.{@entity}", entity);

                return new DataResult<EF.ProductType>(ResultStatus.Error, "TimeoutException occured!!!", entity, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductTypeService.DeleteAsync entity Exception occured!!.{@entity}", entity);

                return new DataResult<EF.ProductType>(ResultStatus.Error, "Exception occured!!!", entity, ex);
            }
        }

        public async Task<IDataResult<List<EF.ProductType>>> GetList(Status status)
        {
            try
            {
                _logger.LogInformation("ProductTypeService.GetList çalıştı.{@status}", status);

                return new DataResult<List<EF.ProductType>>(ResultStatus.Success, await _ProductTypeRepository.GetAll(a => (status == Status.All || a.Status == status)).ToListAsync());

            }
            catch (TimeoutException ex)
            {
                _logger.LogError("ProductTypeService.GetList entity TimeoutException occured!!.{@status}", status);

                return new DataResult<List<EF.ProductType>>(ResultStatus.Error, "TimeoutException occured!!!", new List<EF.ProductType>(), ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductTypeService.GetList entity Exception occured!!.{@status}", status);

                return new DataResult<List<EF.ProductType>>(ResultStatus.Error, "Exception occured!!!", new List<EF.ProductType>(), ex);
            }
        }
    }
}