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
    public class PaymentTypeControllerTest : IntegrationTest
    {


        [Fact]
        public async Task AddPamentType_IsSuccessFull()
        {
            var response = await _client.PostAsync(applicationUrl + "api/PaymentType/AddPaymentType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new PaymentTypeDTO()
                 {
                     IsCard = true,
                     Name = "CreditCard"

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }
        [Fact]
        public async Task AddPamentType_SamePaymentTypeExistSucessFull()
        {
            var response = await _client.PostAsync(applicationUrl + "api/PaymentType/AddPaymentType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new PaymentTypeDTO()
                 {
                     IsCard = true,
                     Name = "CreditCard"

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Aynı ödeme tipinden sistemde mevcut");


        }


        [Fact]
        public async Task AddPamentType_SamePaymentTypeExistFailed()
        {
            var response = await _client.PostAsync(applicationUrl + "api/PaymentType/AddPaymentType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new PaymentTypeDTO()
                 {
                     IsCard = true,
                     Name = "CreditCard"

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Aynı ödeme tipinden sistemde mevcut");


        }

        [Fact]
        public async Task AddPamentType_TimeoutOccured()
        {
            var response = await _client.PostAsync(applicationUrl + "api/PaymentType/AddPaymentType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new PaymentTypeDTO()
                 {
                     IsCard = true,
                     Name = "CreditCard"

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "TimeoutException occured!!!");


        }

        [Fact]
        public async Task AddPamentType_ExceptionOccured()
        {
            var response = await _client.PostAsync(applicationUrl + "api/PaymentType/AddPaymentType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new PaymentTypeDTO()
                 {
                     IsCard = true,
                     Name = "CreditCard"

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Exception occured!!!");


        }


        [Fact]
        public async Task Delete_PaymentType_IsSuccess()
        {
            var response = await _client.PostAsync(applicationUrl + "api/PaymentType/DeletePaymentTye"
                 , new StringContent(
                 JsonConvert.SerializeObject(new DeletePaymentTypeDTO()
                 {
                    Id=2,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }

        [Fact]
        public async Task Delete_PaymentType_IsFail()
        {

            var response = await _client.PostAsync(applicationUrl + "api/PaymentType/DeletePaymentTye"
                 , new StringContent(
                 JsonConvert.SerializeObject(new DeletePaymentTypeDTO()
                 {
                     Id = -1,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Error).ToString());

        }

        [Fact]
        public async Task Delete_PaymentType_PassiveNotDeleted()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/DeleteProduct"
                 , new StringContent(
                 JsonConvert.SerializeObject(new DeletePaymentTypeDTO()
                 {
                     Id = 2,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Silmek istediğiniz ödeme türü pasiftedir.Tekrar silemezsiniz");

        }


        [Fact]
        public async Task GetList_PaymentType_StatusActive()
        {

            var response = await _client.PostAsync(applicationUrl + "api/PaymentType/GetPaymentTypeList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new PaymentTypeListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.Active,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && result.data.ToString().Length > 3);

        }

        [Fact]
        public async Task GetList_PaymentType_StatusAll()
        {

            var response = await _client.PostAsync(applicationUrl + "api/PaymentType/GetPaymentTypeList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new PaymentTypeListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.All,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && result.data.ToString().Length > 3);

        }


        [Fact]
        public async Task GetList_PaymentType_StatusPassive()
        {

            var response = await _client.PostAsync(applicationUrl + "api/PaymentType/GetPaymentTypeList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new PaymentTypeListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.Passive,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && result.data.ToString().Length > 3);

        }
    }
}
