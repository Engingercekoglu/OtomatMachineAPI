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
    public class ProductTypeControllerTest : IntegrationTest
    {


        [Fact]
        public async Task AddProductType_IsSuccessFull()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ProductType/AddProductType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductTypeDTO()
                 {
                     SlotCount = 10,
                     Name = "YiyecekNew"

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }
        [Fact]
        public async Task AddProductType_SameProductTypeExistSucessFull()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ProductType/AddProductType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductTypeDTO()
                 {
                     SlotCount = 10,
                     Name = "YiyecekNew"

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Aynı ürün tipinden sistemde mevcut");


        }


        [Fact]
        public async Task AddProductType_SameProductTypeExistFailed()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ProductType/AddProductType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductTypeDTO()
                 {
                     SlotCount = 1,
                     Name = "YiyecekNew22"

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Aynı ödeme tipinden sistemde mevcut");


        }

        [Fact]
        public async Task AddProductType_TimeoutOccured()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ProductType/AddProductType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductTypeDTO()
                 {
                     SlotCount = 10,
                     Name = "YiyecekNew"

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "TimeoutException occured!!!");


        }

        [Fact]
        public async Task AddProductType_ExceptionOccured()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ProductType/AddProductType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductTypeDTO()
                 {
                     SlotCount = 10,
                     Name = "YiyecekNew"

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Exception occured!!!");


        }


        [Fact]
        public async Task Delete_ProductType_IsSuccess()
        {
            var response = await _client.PostAsync(applicationUrl + "api/ProductType/DeleteProductType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new DeleteProductTypeDTO()
                 {
                     Id = 2,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }

        [Fact]
        public async Task Delete_ProductType_PassiveNotDeleted()
        {

            var response = await _client.PostAsync(applicationUrl + "api/ProductType/DeleteProductType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new DeleteProductTypeDTO()
                 {
                     Id = 2,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Silmek istediğiniz ürün tipi pasiftedir.Tekrar silemezsiniz");

        }
        [Fact]
        public async Task Delete_ProductType_IsFail()
        {

            var response = await _client.PostAsync(applicationUrl + "api/ProductType/DeleteProductType"
                 , new StringContent(
                 JsonConvert.SerializeObject(new DeleteProductTypeDTO()
                 {
                     Id = -1,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Error).ToString());

        }



        [Fact]
        public async Task GetList_ProductType_StatusActive()
        {

            var response = await _client.PostAsync(applicationUrl + "api/ProductType/GetProductTypeList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductTypeListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.Active,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && result.data.ToString().Length > 3);

        }

        [Fact]
        public async Task GetList_ProductType_StatusAll()
        {

            var response = await _client.PostAsync(applicationUrl + "api/ProductType/GetProductTypeList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductTypeListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.All,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && result.data.ToString().Length > 3);

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
        public async Task GetList_ProductType_StatusPassive()
        {

            var response = await _client.PostAsync(applicationUrl + "api/ProductType/GetProductTypeList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductTypeListDTO()
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
