using OtomatMachine.Entity.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace OtomatMachine.Entity.SeedData
{
    public class PaymentTypeSeedData
    {
        public  void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                DataContext context = serviceScope.ServiceProvider.GetService<DataContext>();
                if (!context.PaymentTypes.Any())
                {
                    context.PaymentTypes.AddRange(
                        new Entities.PaymentType { Name = "Temassız Kredi kartı", Status = Shared.Enum.Enum.Status.Active, CreatedDate = System.DateTime.Now, IsCard = true },
                                                                 new Entities.PaymentType { Name = "Temaslı Kredi kartı", Status = Shared.Enum.Enum.Status.Active, CreatedDate = System.DateTime.Now, IsCard = true },
                                                                                         new Entities.PaymentType { Name = "Nakit Bozuk Para", Status = Shared.Enum.Enum.Status.Active, CreatedDate = System.DateTime.Now, IsCard = false }
                                                                                         ,
                                                                                         new Entities.PaymentType { Name = "Nakit kağıt Para", Status = Shared.Enum.Enum.Status.Active, CreatedDate = System.DateTime.Now, IsCard = false }
                        );
                    context.SaveChanges();
                }
            }
        }
    }
}
