using OtomatMachine.Entity.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;

namespace OtomatMachine.Entity.SeedData
{
    public class ProductSeedData
    {
        public void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                DataContext context = serviceScope.ServiceProvider.GetService<DataContext>();
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Entities.Product { Name = "CocaCola", ProductTypeId = 2, IsHotDrink = false, Price = Convert.ToDecimal(5.25), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
   new Entities.Product { Name = "Redbull", ProductTypeId = 2, IsHotDrink = false, Price = Convert.ToDecimal(14.25), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
      new Entities.Product { Name = "PortakalSuyu", ProductTypeId = 2, IsHotDrink = false, Price = Convert.ToDecimal(25.25), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
         new Entities.Product { Name = "Cappy Şeftali", ProductTypeId = 2, IsHotDrink = false, Price = Convert.ToDecimal(7.25), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
            new Entities.Product { Name = "Starbuks Soğu Coffee", ProductTypeId = 2, IsHotDrink = false, Price = Convert.ToDecimal(25.11), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
               new Entities.Product { Name = "Mocha Coffee", ProductTypeId = 2, IsHotDrink = true, Price = Convert.ToDecimal(35.25), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                  new Entities.Product { Name = "Çay", ProductTypeId = 2, IsHotDrink = true, Price = Convert.ToDecimal(4), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                     new Entities.Product { Name = "Türk kahvesi", ProductTypeId = 2, IsHotDrink = true, Price = Convert.ToDecimal(25), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                        new Entities.Product { Name = "Sprite", ProductTypeId = 2, IsHotDrink = false, Price = Convert.ToDecimal(5.25), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                           new Entities.Product { Name = "Gazoz", ProductTypeId = 2, IsHotDrink = false, Price = Convert.ToDecimal(5.25), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },


                            new Entities.Product { Name = "Çizi", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(4), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
   new Entities.Product { Name = "İkram", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(7.11), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
      new Entities.Product { Name = "Çubuk kraker", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(2), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
         new Entities.Product { Name = "Kek", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(10), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
            new Entities.Product { Name = "Gong", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(11), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
               new Entities.Product { Name = "Haribo Ayıcık", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(12), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                  new Entities.Product { Name = "Hariba yılan", ProductTypeId = 2, IsHotDrink = false, Price = Convert.ToDecimal(13), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                     new Entities.Product { Name = "Kinder Sıpriz yumurta", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(14), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                        new Entities.Product { Name = "Tadella", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(15), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                           new Entities.Product { Name = "Çikolatalı Gofret", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(16), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                           new Entities.Product { Name = "Susamlı Çizi", ProductTypeId = 2, IsHotDrink = false, Price = Convert.ToDecimal(5.25), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
   new Entities.Product { Name = "Lays", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(20), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
      new Entities.Product { Name = "Doritos", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(21), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
         new Entities.Product { Name = "Cheetos", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(22), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
            new Entities.Product { Name = "Portakallo Kek", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(23), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
               new Entities.Product { Name = "baharatlı Çubuk Kraker", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(24), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                  new Entities.Product { Name = "Peynirli Soğuk Sandiviç", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(25), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                     new Entities.Product { Name = "kaşar Peynirli Soğuk Sandiviç", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(26), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                        new Entities.Product { Name = "Dondurulmuş peperonni Pizza", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(27), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active },
                           new Entities.Product { Name = "Hazır Donmuş Kaşarlı Tost", ProductTypeId = 1, IsHotDrink = false, Price = Convert.ToDecimal(28), CreatedDate = System.DateTime.Now, Status = Shared.Enum.Enum.Status.Active }



                           );
                    context.SaveChanges();
                }
            }
        }
    }
}
