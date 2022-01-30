using OtomatMachine.Entity.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace OtomatMachine.Entity.SeedData
{
    public class ProductTypeSeedData
    {
        public  void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                DataContext context = serviceScope.ServiceProvider.GetService<DataContext>();
                if (!context.ProductTypes.Any())
                {
                    context.ProductTypes.AddRange(
                        new Entities.ProductType { Name = "Yiyecek", Status = Shared.Enum.Enum.Status.Active, CreatedDate = System.DateTime.Now, SlotCount =20  },
                                                               new Entities.ProductType { Name = "İçecek", Status = Shared.Enum.Enum.Status.Active, CreatedDate = System.DateTime.Now, SlotCount = 10 }
                        );
                    context.SaveChanges();
                }
            }
        }
    }
}
