using Newtonsoft.Json;
using OtomatMachine.Entity.Dtos;
using OtomatMachine.Entity.Entities;
using OtomatMachine.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OtomatMachineIntegrationTest
{
    public class ReceiptTransactionControllerTest : IntegrationTest
    {


        [Fact]
        public async Task AddReceiptTransaction_IsSuccessFull()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                    
                     PaymentTypeId = 1,
                     ProductCount = 10,
                     ProductId = 1

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }

 


        [Fact]
        public async Task AddReceiptTransaction_IsFail()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     SugarCount=10,
                     PaymentTypeId = 1,
                     ProductCount = 10,
                     ProductId = 1

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Warning).ToString());


        }
        [Fact]
        public async Task AddReceiptTransaction_SelectedPaymentNotExists()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     SugarCount = 10,
                     PaymentTypeId = 122,
                     ProductCount = 10,
                     ProductId = 1

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Seçtiğiniz ödeme tipi  sistemde mevcut değil");


        }


        [Fact]
        public async Task AddReceiptTransaction_SelectedPaymentIsCard_Fail()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     PaymentTypeId = 1,
                     ProductCount = 10,
                     ProductId = 1
                     ,PaymentAmount=100

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString().Contains( "Ödeme tipi  kartlı ödeme olduğundan servis tarafına tutar gönderilmemelidir.Girilen Tutar"));


        }

        [Fact]
        public async Task AddReceiptTransaction_SelectedPaymentIsCard_Success()
        {

        
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     PaymentTypeId = 1,
                     ProductCount = 10,
                     ProductId = 1

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());



        }
        [Fact]
        public async Task AddReceiptTransaction_SelectedPaymentNotCard_Failed()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     PaymentTypeId = 3,
                     ProductCount = 10,
                     ProductId = 1
                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString().Contains("Ödeme tipi  kartlı ödeme dışında olduğundan servis tarafına  tutar gönderilmelidir.Girilen Tutar"));


        }


        [Fact]
        public async Task AddReceiptTransaction_SelectedPaymentNotCardPayamountMoreThanTotalPrice_Failed()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     PaymentTypeId = 3,
                     ProductCount = 10,
                     ProductId = 1
                     ,PaymentAmount=5
                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString().Contains("Toplam tutar dan küçük olduğu için ödeme yapılamaz"));


        }

        [Fact]
        public async Task AddReceiptTransaction_SelectedPaymentNotCardPayamountMoreThanTotalPrice_Sucess()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     PaymentTypeId = 3,
                     ProductCount = 10,
                     ProductId = 1
                     ,
                     PaymentAmount = 5222
                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }
        [Fact]
        public async Task AddReceiptTransaction_SelectedPaymentNotCard_SuccessFul()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     PaymentTypeId = 3,
                     ProductCount = 10,
                     ProductId = 1
                     ,PaymentAmount=500
                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }

        [Fact]
        public async Task AddReceiptTransaction_SelectedProductNotExists()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     SugarCount = 10,
                     PaymentTypeId = 1,
                     ProductCount = 10,
                     ProductId = 221

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Seçtiğiniz ürün  sistemde mevcut değil");


        }

        [Fact]
        public async Task AddReceiptTransaction_SelectedProductIsHotDrink_Success()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     SugarCount = 10,
                     PaymentTypeId = 1,
                     ProductCount = 10,
                     ProductId = 22

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }

        [Fact]
        public async Task AddReceiptTransaction_SelectedProductIsHotDrink_Fail()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     PaymentTypeId = 1,
                     ProductCount = 10,
                     ProductId = 22

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Sıcak içecek olduğundan şeker adet seçimi yapılmalıdır.");


        }

        [Fact]
        public async Task AddReceiptTransaction_SelectedProductIsNotHotDrink_Success()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     PaymentTypeId = 1,
                     ProductCount = 10,
                     ProductId = 6

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }

        [Fact]
        public async Task AddReceiptTransaction_SelectedProductIsNotHotDrink_Fail()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ReceiptTransaction/AddReceiptTransaction"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ReceiptTransactionRequestDTO()
                 {
                     PaymentTypeId = 1,
                     ProductCount = 10,
                     ProductId = 6
                     ,SugarCount=100

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Sıcak içecek dışında şeker adet seçimi yapılmamalıdır.Lütfen kontrol edin!");


        }
     


    }
}
