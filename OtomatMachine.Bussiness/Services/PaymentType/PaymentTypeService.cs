using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OtomatMachine.Bussiness.Repositories.PaymentType;
using OtomatMachine.Shared.Utilities.Results.Abstract;
using OtomatMachine.Shared.Utilities.Results.ComplexTypes;
using OtomatMachine.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OtomatMachine.Shared.Enum.Enum;
using EF = OtomatMachine.Entity.Entities;

namespace OtomatMachine.Bussiness.Services.PaymentType
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly ILogger<PaymentTypeService> _logger;
        private readonly IConfiguration _configuration;
        public PaymentTypeService(ILogger<PaymentTypeService> logger, IConfiguration configuration, IPaymentTypeRepository paymentTypeRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _paymentTypeRepository = paymentTypeRepository;
        }
        public async Task<IDataResult<EF.PaymentType>> AddAsync(EF.PaymentType paymentType)
        {
            try
            {
                _logger.LogInformation("PaymentTypeService.AddAsync çalıştı.{@paymentType}", paymentType);

                if (await _paymentTypeRepository.Any(a => a.Name == paymentType.Name && a.IsCard == paymentType.IsCard && a.Status == Status.Active))
                    return new DataResult<EF.PaymentType>(ResultStatus.Warning, "Aynı ödeme tipinden sistemde mevcut", paymentType);
                var result = await _paymentTypeRepository.Add(paymentType);
                _logger.LogInformation("PaymentTypeService.AddAsync ürün  tipi başarılı bir şekilde eklendi.{@result}", result);

                return new DataResult<EF.PaymentType>(ResultStatus.Success, result);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("PaymentTypeService.AddAsync Error aldı.{@paymentType},{@exception}", paymentType,ex);

                return new DataResult<EF.PaymentType>(ResultStatus.Error, "TimeoutException occured!!!", paymentType, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("PaymentTypeService.AddAsync Error aldı.{@paymentType},{@exception}", paymentType, ex);

                return new DataResult<EF.PaymentType>(ResultStatus.Error, "Exception occured!!!", paymentType, ex);
            }
        }

        public async Task<IDataResult<EF.PaymentType>> DeleteAsync(int Id)
        {
            var entity = new EF.PaymentType();
            try
            {
                _logger.LogInformation("PaymentTypeService.DeleteAsync çalıştı.{@Id}", Id);

                entity = _paymentTypeRepository.GetByParam(a => a.Id == Id).Result;
                if (entity==null)
                {
                    _logger.LogError("PaymentTypeService.DeleteAsync Silmek istediğiniz ödeme tipi sistemde bulunamadı.{@Id}", Id);

                    return new DataResult<EF.PaymentType>(ResultStatus.Error, "Silmek istediğiniz ödeme tipi sistemde bulunamadı.", new EF.PaymentType { Id=Id});

                }

                if (entity.Status == Status.Passive)
                {
                    _logger.LogError("ProductService.DeleteAsync Silmek istediğiniz ödeme türü pasiftedir.Tekrar silemezsiniz.{@Id}", Id);

                    return new DataResult<EF.PaymentType>(ResultStatus.Warning, "Silmek istediğiniz ödeme türü pasiftedir.Tekrar silemezsiniz", new EF.PaymentType { Id = Id });
                }
                entity.Status = Status.Passive;

                _logger.LogInformation("PaymentTypeService.DeleteAsync entity için update  çalıştı.{@entity}", entity);

                var result = await _paymentTypeRepository.Update(entity);
                _logger.LogInformation("PaymentTypeService.DeleteAsync entity Başarılı ile pasife çekild.{@entity}", entity);

                return new DataResult<EF.PaymentType>(ResultStatus.Success,"Başarılı ile pasife çekildi", entity);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("PaymentTypeService.DeleteAsync entity TimeoutException occured!!.{@entity}", entity);

                return new DataResult<EF.PaymentType>(ResultStatus.Error, "TimeoutException occured!!!", entity, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("PaymentTypeService.DeleteAsync entity Exception occured!!.{@entity}", entity);

                return new DataResult<EF.PaymentType>(ResultStatus.Error, "Exception occured!!!", entity, ex);
            }
        }

        public async Task<IDataResult<List<EF.PaymentType>>> GetList(Status status)
        {
            try
            {
                _logger.LogInformation("PaymentTypeService.GetList çalıştı.{@Status}", status);

                return new DataResult<List<EF.PaymentType>>(ResultStatus.Success, await _paymentTypeRepository.GetAll(a => (status == Status.All || a.Status == status)).ToListAsync());

            }
            catch (TimeoutException ex)
            {
                _logger.LogError("PaymentTypeService.GetList entity TimeoutException occured!!.{@status}", status);

                return new DataResult<List<EF.PaymentType>>(ResultStatus.Error, "TimeoutException occured!!!", new List<EF.PaymentType>(), ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("PaymentTypeService.GetList entity Exception occured!!.{@status}", status);

                return new DataResult<List<EF.PaymentType>>(ResultStatus.Error, "Exception occured!!!", new List<EF.PaymentType>(), ex);
            }
        }
    }
}