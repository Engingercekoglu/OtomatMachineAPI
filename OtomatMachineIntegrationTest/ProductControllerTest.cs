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
    public class ProductControllerTest : IntegrationTest
    {


        [Fact]
        public async Task AddProduct_IsSuccessFull()
        {
            var response = await _client.PostAsync(applicationUrl + "api/Product/AddProduct"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductDTO()
                 {
                     Name = "Armut",
                     IsHotDrink = false,
                     Price = Convert.ToDecimal(1.55),
                     ProductTypeId = 1

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }
        [Fact]
        public async Task AddProduct_SameProductExistSucessFull()
        {
            var response = await _client.PostAsync(applicationUrl + "api/Product/AddProduct"
                 , new StringContent(
                   JsonConvert.SerializeObject(new ProductDTO()
                   {
                       Name = "Armut",
                       IsHotDrink = false,
                       Price = Convert.ToDecimal(1.55),
                       ProductTypeId = 1

                   }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Aynı ürün sistemde mevcut");


        }
        [Fact]
        public async Task AddProduct_SlotCountMaxValueReached()
        {


            var response = await _client.PostAsync(applicationUrl + "api/Product/AddProduct"
                 , new StringContent(
                   JsonConvert.SerializeObject(new ProductDTO()
                   {
                       Name = "Armut",
                       IsHotDrink = false,
                       Price = Convert.ToDecimal(1.55),
                       ProductTypeId = 1

                   }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());


            Assert.True(result.message.ToString() == "Eklenmek istenen ürünün slot sayısına ulaştığı için yeni ürün eklenemektedir");


        }
        [Fact]
        public async Task AddProduct_ProdutTypeNotExist()
        {


            var response = await _client.PostAsync(applicationUrl + "api/Product/AddProduct"
                 , new StringContent(
                   JsonConvert.SerializeObject(new ProductDTO()
                   {
                       Name = "Armut22",
                       IsHotDrink = false,
                       Price = Convert.ToDecimal(1.55),
                       ProductTypeId = 5

                   }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());


            Assert.True(result.message.ToString() == "Sistemde ürün tipi mevcut değil");


        }
        [Fact]
        public async Task AddProduct_SameProductExistFailed()
        {
            var response = await _client.PostAsync(applicationUrl + "api/Product/AddProduct"
                 , new StringContent(
                  JsonConvert.SerializeObject(new ProductDTO()
                  {
                      Name = "Armut",
                      IsHotDrink = false,
                      Price = Convert.ToDecimal(1.55),
                      ProductTypeId = 1

                  }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Aynı ödeme tipinden sistemde mevcut");


        }

        [Fact]
        public async Task AddProduct_TimeoutOccured()
        {
            var response = await _client.PostAsync(applicationUrl + "api/Product/AddProduct"
                 , new StringContent(
                JsonConvert.SerializeObject(new ProductDTO()
                {
                    Name = "Armut",
                    IsHotDrink = false,
                    Price = Convert.ToDecimal(1.55),
                    ProductTypeId = 1

                }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "TimeoutException occured!!!");


        }

        [Fact]
        public async Task AddProduct_ExceptionOccured()
        {
            var response = await _client.PostAsync(applicationUrl + "api/Product/AddProduct"
                 , new StringContent(
                JsonConvert.SerializeObject(new ProductDTO()
                {
                    Name = "Armut",
                    IsHotDrink = false,
                    Price = Convert.ToDecimal(1.55),
                    ProductTypeId = 1

                }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Exception occured!!!");


        }


        [Fact]
        public async Task Delete_Product_IsSuccess()
        {
            var response = await _client.PostAsync(applicationUrl + "api/Product/DeleteProduct"
                 , new StringContent(
                 JsonConvert.SerializeObject(new DeleteProductDTO()
                 {
                     Id = 5,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString());


        }

        [Fact]
        public async Task Delete_Product_IsFail()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/DeleteProduct"
                 , new StringContent(
                 JsonConvert.SerializeObject(new DeleteProductDTO()
                 {
                     Id = -1,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Error).ToString());

        }
        [Fact]
        public async Task Delete_Product_PassiveNotDeleted()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/DeleteProduct"
                 , new StringContent(
                 JsonConvert.SerializeObject(new DeleteProductDTO()
                 {
                     Id = 2,

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());

            Assert.True(result.message.ToString() == "Silmek istediğiniz ürün pasiftedir.Tekrar silemezsiniz");

        }




        [Fact]
        public async Task GetList_Product_StatusActiveForProductTypeIdAll()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/GetProductList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.Active,ProductTypeId=0

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && result.data.ToString().Length > 3);

        }


        [Fact]
        public async Task GetList_Product_StatusActiveForProductTypeIdForFood()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/GetProductList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.Active,
                     ProductTypeId = 1

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && result.data.ToString().Length > 3);

        }

        [Fact]
        public async Task GetList_Product_StatusActiveForProductTypeIdForDrink()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/GetProductList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.Active,
                     ProductTypeId = 2

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && result.data.ToString().Length > 3);

        }



        [Fact]
        public async Task GetList_Product_StatusPassiveForProductTypeIdForFood()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/GetProductList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.Passive,
                     ProductTypeId = 1

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && result.data.ToString().Length > 3);

        }

        [Fact]
        public async Task GetList_Product_StatusPassiveForProductTypeIdForDrink()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/GetProductList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.Active,
                     ProductTypeId = 2

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && result.data.ToString().Length > 3);

        }

        [Fact]
        public async Task GetList_Product_StatusActiveNotExists()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/GetProductList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.Active,
                     ProductTypeId = 5

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && string.IsNullOrEmpty(result.data.ToString()));

        }

        [Fact]
        public async Task GetList_Product_StatusPassiveNotExists()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/GetProductList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.Passive,
                     ProductTypeId = 5

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && string.IsNullOrEmpty(result.data.ToString()));

        }

        [Fact]
        public async Task GetList_Product_StatusAllNotExists()
        {

            var response = await _client.PostAsync(applicationUrl + "api/Product/GetProductList"
                 , new StringContent(
                 JsonConvert.SerializeObject(new ProductListDTO()
                 {
                     Status = OtomatMachine.Shared.Enum.Enum.Status.All,
                     ProductTypeId = 5

                 }),
             Encoding.UTF8,
             "application/json"));
            var result = DeserializeJsonFromStream(await response.Content.ReadAsStreamAsync());
            Assert.True(result.resultStatus.ToString() == ((int)ResultStatus.Success).ToString() && string.IsNullOrEmpty(result.data.ToString()));

        }
    }
}
