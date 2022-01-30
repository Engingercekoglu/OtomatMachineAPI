using OtomatMachine.Bussiness.Services.PaymentType;
using OtomatMachineAPI.Controllers;
using Xunit;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using OtomatMachine.Entity.Dtos;
using System.Threading.Tasks;
using OtomatMachine.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using OtomatMachine.Shared.Utilities.Results.Concrete;
using OtomatMachine.Shared.Utilities.Results.Abstract;
using OtomatMachine.Shared.Utilities.Results.ComplexTypes;
using OtomatMachine.Bussiness.Repositories.PaymentType;
using System.Linq;
using System.Collections.Generic;
using Moq;
using Microsoft.AspNetCore.Hosting;
using System;

namespace OtomatMachinexUnitTest
{
    public class PaymentTypeControllerTest {

      
        private readonly PaymentTypeController _controller;
 
        public PaymentTypeControllerTest()
        {
            var environment = A.Fake< Microsoft.AspNetCore.Hosting.IHostingEnvironment> ();
            var paymentTypeService = A.Fake<IPaymentTypeService>();
            var logger = A.Fake<ILogger<PaymentTypeController>>();

            _controller = new PaymentTypeController(environment, paymentTypeService, logger);
        }

        [Fact]
        public  void AddPaymentType_Returnpayment_IsTrueAsync()
        {

           
            
//            var paymentType = new PaymentTypeDTO();
//                      paymentType.Name = "Nakit kağıt Para";
//                    paymentType.IsCard = false;

//            CreatedAtActionResult result = _controller.AddPaymentType(paymentType) as CreatedAtActionResult;
//            //OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.ExecuteResult(null));
//            Assert.IsType<ActionResult>(result); // WORKS

//            var x = (result as CreatedAtActionResult).Value;
//            //Assert.Equal(paymentType, new PaymentTypeDTO { IsCard = x.Data. IsCard, Name = x.Data.Name });
////            bool v = paymentTypeService.Setup(a => a.AddAsync(new PaymentType { CreatedDate = DateTime.Now, IsCard = false, Name = "Nakit kağıt Para 2", Status = OtomatMachine.Shared.Enum.Enum.Status.Active })).ReturnsAsync<(Item)null>

////            
                
//                var mockRepo = new Mock<IPaymentTypeRepository>();
//            mockRepo.Setup(repo => repo.GetByParam(a=>a.IsCard==paymentType.IsCard && a.Name==paymentType
//                .ReturnsAsync((BrainstormSession)null);
//            var controller = new IdeasController(mockRepo.Object);

//            // Act
//            var result = await controller.ForSession(testSessionId);

//            // Assert
//            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
//            Assert.Equal(testSessionId, notFoundObjectResult.Value);
          
//                ;
   
//            OkObjectResult actionResult = (OkObjectResult)await _controller.AddPaymentType(paymentType);

//           var paymettype = actionResult.Value as DataResult<PaymentType>;
//;
//            Assert.True(paymettype.ResultStatus==ResultStatus.Success);

        }
    }
}
