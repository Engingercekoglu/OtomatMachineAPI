using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OtomatMachine.Bussiness.Repositories.PaymentType;
using OtomatMachine.Bussiness.Repositories.Product;
using OtomatMachine.Bussiness.Repositories.ReceiptTransaction;
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

namespace OtomatMachine.Bussiness.Services.ReceiptTransaction
{
    public class ReceiptTransactionService : IReceiptTransactionService
    {
        private readonly IReceiptTransactionRepository _reciptTransactionRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ReceiptTransactionService> _logger;
        private readonly IConfiguration _configuration;

        public ReceiptTransactionService(ILogger<ReceiptTransactionService> logger, IConfiguration configuration, IReceiptTransactionRepository reciptTransactionRepository, IPaymentTypeRepository paymentTypeRepository, IProductRepository productRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _paymentTypeRepository = paymentTypeRepository;
            _reciptTransactionRepository = reciptTransactionRepository;
            _productRepository = productRepository;
        }
        public async Task<IDataResult<ReceiptTransactionResponseDTO>> AddAsync(ReceiptTransactionRequestDTO receiptTransaction)
        {
            try
            {
                _logger.LogInformation("ReceiptTransactionService.AddAsync çalıştı.{@receiptTransaction}", receiptTransaction);

                var product = new EF.Product();
                var paymenttype = new EF.PaymentType();
                product = await _productRepository.GetByParam(a => a.Id == receiptTransaction.ProductId);
                if (product == null)
                {
                    _logger.LogError("ReceiptTransactionService.AddAsync Seçtiğiniz ürün  sistemde mevcut değil.{@receiptTransactionProductId}", receiptTransaction.ProductId);

                    return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Error, "Seçtiğiniz ürün  sistemde mevcut değil", new ReceiptTransactionResponseDTO
                    {
                        PaymentTypeName = paymenttype.Name,
                        ProductCount = receiptTransaction.ProductCount,
                        ReceiptTransactionId = -1,
                        ProductName = product?.Name,
                        RefundedAmount = 0
                        ,
                        TotalPrice = 0
                    });

                }
                decimal totalPrice = product.Price * receiptTransaction.ProductCount;
                decimal refundedAmount = receiptTransaction.PaymentAmount == 0 ? 0 : (totalPrice < receiptTransaction.PaymentAmount ? receiptTransaction.PaymentAmount - totalPrice : 0);
                var errorReceiptItem = new ReceiptTransactionResponseDTO
                {
                    PaymentTypeName = paymenttype.Name,
                    ProductCount = receiptTransaction.ProductCount,
                    ReceiptTransactionId = -1,
                    ProductName = product?.Name,
                    RefundedAmount = refundedAmount
                           ,
                    TotalPrice = totalPrice,
                    SugarCount = receiptTransaction.SugarCount
                };
                try
                {

                    paymenttype = await _paymentTypeRepository.GetByParam(a => a.Id == receiptTransaction.PaymentTypeId);
                    if (paymenttype == null)
                    {
                        _logger.LogError("ReceiptTransactionService.AddAsync Seçtiğiniz ödeme tipi  sistemde mevcut değil.{@receiptTransactionProductIdPaymentTypeId}", receiptTransaction.PaymentTypeId);

                        return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Error, "Seçtiğiniz ödeme tipi  sistemde mevcut değil", new ReceiptTransactionResponseDTO
                        {
                            PaymentTypeName = paymenttype.Name,
                            ProductCount = receiptTransaction.ProductCount,
                            ReceiptTransactionId = -1,
                            ProductName = product?.Name,
                            RefundedAmount = 0
                            ,
                            TotalPrice = 0,
                            SugarCount = receiptTransaction.SugarCount
                        });

                    }
                    if (paymenttype.IsCard && receiptTransaction.PaymentAmount > 0)
                    {
                        _logger.LogWarning("ReceiptTransactionService.AddAsync Ödeme tipi  kartlı ödeme olduğundan servis tarafına  tutar gönderilmemelidir.Girilen Tutar.{@receiptTransactionPaymentAmount}", receiptTransaction.PaymentAmount);

                        return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Warning, "Ödeme tipi  kartlı ödeme olduğundan servis tarafına  tutar gönderilmemelidir.Girilen Tutar " + receiptTransaction.PaymentAmount, new ReceiptTransactionResponseDTO { });

                    }
                    else if (!paymenttype.IsCard && receiptTransaction.PaymentAmount <= 0)
                    {
                        _logger.LogWarning("ReceiptTransactionService.AddAsync Ödeme tipi  kartlı ödeme dışında olduğundan servis tarafına  tutar gönderilmelidir.Girilen Tutar.{@receiptTransactionPaymentAmount}", receiptTransaction.PaymentAmount);


                        return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Warning, "Ödeme tipi  kartlı ödeme dışında olduğundan servis tarafına  tutar gönderilmelidir.Girilen Tutar " + receiptTransaction.PaymentAmount, new ReceiptTransactionResponseDTO { });

                    }
                    else if (!paymenttype.IsCard && receiptTransaction.PaymentAmount < totalPrice)
                    {

                        _logger.LogWarning("ReceiptTransactionService.AddAsync Girilen tutar Toplam tutar dan küçük olduğu için ödeme yapılamaz.{@receiptTransactionPaymentAmount},{@totalPrice}", receiptTransaction.PaymentAmount, totalPrice);

                        return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Warning, "Girilen tutar " + receiptTransaction.PaymentAmount + "Toplam tutar dan küçük olduğu için ödeme yapılamaz" + totalPrice, new ReceiptTransactionResponseDTO { });

                    }
                    if (product.IsHotDrink && receiptTransaction.SugarCount > 0)
                    {
                        _logger.LogWarning("ReceiptTransactionService.AddAsync Sıcak içecek olduğundan şeker adet seçimi yapılmalıdır.{@SugarCount}", receiptTransaction.SugarCount);

                        return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Warning, "Sıcak içecek olduğundan şeker adet seçimi yapılmalıdır.", new ReceiptTransactionResponseDTO { });
                    }
                    else if (!product.IsHotDrink && receiptTransaction.SugarCount > 0)
                    {
                        _logger.LogWarning("ReceiptTransactionService.AddAsync  Sıcak içecek dışında şeker adet seçimi yapılmamalıdır.Lütfen kontrol edin!.{@SugarCount}", receiptTransaction.SugarCount);

                        return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Warning, "Sıcak içecek dışında şeker adet seçimi yapılmamalıdır.Lütfen kontrol edin!", new ReceiptTransactionResponseDTO { });


                    }
                    var result = await _reciptTransactionRepository.Add(new EF.ReceiptTransaction
                    {
                        CreatedDate = DateTime.Now,
                        PaymentTypeId = receiptTransaction.PaymentTypeId,
                        ProductId = receiptTransaction.ProductId,
                        ProductCount = receiptTransaction.ProductCount,
                        RefundedAmount = refundedAmount,
                        TotalPrice = totalPrice,
                        Status = Status.Active,
                        SugarCount = receiptTransaction.SugarCount
                    });
                    _logger.LogInformation("ReceiptTransactionService.AddAsync ürün başarılı bir şekilde eklendi.{@result}", result);
                    var receiptitem = new ReceiptTransactionResponseDTO
                    {
                        PaymentTypeName = result.PaymentType.Name,
                        ProductCount = result.ProductCount,
                        ReceiptTransactionId = result.Id,
                        ProductName = result.Product.Name,
                        RefundedAmount = result.RefundedAmount
                        ,
                        TotalPrice = result.TotalPrice,
                        SugarCount = receiptTransaction.SugarCount

                    };
                    _logger.LogInformation("ReceiptTransactionService.AddAsync fiş oluşturuldu.{@receiptitem}", receiptitem);

                    return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Success,receiptitem);


                }
                catch (TimeoutException ex)
                {
                    _logger.LogError("ProductService.AddAsync TimeoutException occured!!!.{@errorReceiptItem},{@exception}", errorReceiptItem, ex);

                    return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Error, "TimeoutException occured!!!", errorReceiptItem, ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError("ProductService.AddAsync Exception occured!!!.{@errorReceiptItem},{@exception}", errorReceiptItem, ex);

                    return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Error, "Exception occured!!!", errorReceiptItem, ex);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductService.AddAsync Hesaplama yapılamadı!!!.{@errorReceiptItem},{@exception}", new ReceiptTransactionResponseDTO
                {

                }, ex);


                return new DataResult<ReceiptTransactionResponseDTO>(ResultStatus.Error, "Hesaplama yapılamadı!!!", new ReceiptTransactionResponseDTO
                {

                }, ex);
            }
        }


        public async Task<IDataResult<List<ReceiptTransactionResponseDTO>>> GetList(ReceiptTransactionListRequestDTO request)
        {
            try
            {
                _logger.LogInformation("ReceiptTransactionService.GetList çalıştı.{@receiptTransactionListRequestDTO}", request);

                var vList = await _reciptTransactionRepository.GetAll(a => (a.CreatedDate >= request.StartDate && a.CreatedDate <= request.EndDate) && (request.PaymentTypeId == 0 || a.PaymentTypeId == request.PaymentTypeId) && (request.ProductId == 0 || a.ProductId == request.ProductId), a => a.Product, a => a.PaymentType).ToListAsync();
                _logger.LogInformation("ReceiptTransactionService.GetAll dan dönecek fiş listesi oluşturulmaya başlandı.{@receipttransactionList}", vList);

                var returnList = vList.Select(a => new ReceiptTransactionResponseDTO
                {
                    PaymentTypeName = a.PaymentType.Name,
                    ProductCount = a.ProductCount,
                    ProductName = a.Product.Name,
                    ReceiptTransactionId = a.Id,
                    RefundedAmount = a.RefundedAmount,
                    SugarCount = a.SugarCount,
                    TotalPrice = a.TotalPrice


                }).ToList();
                _logger.LogInformation("ReceiptTransactionService.Getlist ten dönece fiş listesi oluşturuldu.{@returnList}", returnList);

                return new DataResult<List<ReceiptTransactionResponseDTO>>(ResultStatus.Success, returnList);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("ReceiptTransactionService.GetList entity TimeoutException occured!!.{@request}", request);


                return new DataResult<List<ReceiptTransactionResponseDTO>>(ResultStatus.Error, "TimeoutException occured!!!", new List<ReceiptTransactionResponseDTO>(), ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("ReceiptTransactionService.GetList entity Exception occured!!.{@request}", request);

                return new DataResult<List<ReceiptTransactionResponseDTO>>(ResultStatus.Error, "Exception occured!!!", new List<ReceiptTransactionResponseDTO>(), ex);
            }
        }
    }
}