# OtomatMachineAPI

Proje .net core 3.1  rest api   olarak oluşturuldu.Solid prensiplerine uygun yazıldı. Dökümantasyon olarak swagger UI kullanıldı.Swagger UI arayüzünden
json formatında rest istekleri yapıp deneme yapılabilmektedir.Projede serilog lama kullandım.Serilog kurulduğunda info error ve warning loglar otomatik loglanmaktadır.Tüm request ve reponselarda loglanmaktadır.


Proje Bussiness ,Entity,Shared ,API  ve Integration Test  katmanından oluşmaktadır.

#OtomatMachine.Bussiness
Bussiness  katmanı servis ve repository lerin olduğu katman .

Repository ler  IRepository   den üretildi.IRepository add,update,delete,getbyparam,Getall,any ve save gibi generic type dan türetilmiş methodlar bulunmaktadır.

PaymentTypeRepository (Ödeme Tipi),ProductRepository(Ürünler), ProductTypeRepository (ürün tipi),ReceiptTransactionRepository (Ödeme Fiş Bilgisi):  datacontect i  ve generic repository kullanarak  db işlemlerini gerçekleştirmektedir.

Servis Katmanı:Kontrollerlar tarafından Post edilen datayı servis katmanı alıp repository ye ileterek db ile ilgili işlemleri gerçekleşir.Db ile ilgili
işlemleri repositoryler halleder.Reponse ları DataResult   objesinin içine T tipinde classları oluşturup response u controller döner.

PaymentTypeService : Birden fazla ödeme tipi  ekleme AddAsync.Ödeme tipi silme DeleteAsync.Ödeme tiplerini listelemek için all,active,passive gibi statuslerini kullanarak GetList methodu sağlar.

ProductTypeService : Birden fazla ürün tipi  ekleme AddAsync.ürün tipi silme DeleteAsync.ürün tiplerini listelemek için all,active,passive gibi statuslerini kullanarak GetList methodu sağlar.

ProductService : Birden fazla ürün   ekleme AddAsync.ürün  silme DeleteAsync.ürünleri  listelemek için all,active,passive gibi statuslerini ve ürün tipine göre kullanarak GetList methodu sağlar.

ReceiptTransactionService: AddAsync  ürün seçimi, adet seçimi, ödeme seçimi, para iadesi ve bilgi fişi bilgisini üretir. ve kontroller a ödeme fiş bilgisini dönmektedir.
GetList :Transactionın oluşturma zamanına göre ,ürüne göre ve ödeme bilgisine göre fiş bilgisi içeren tüm bilgileri dönen transaction listesidir.

#OtomatMachine.Entity

DataContext.cs  dbset leri ve modelleri db de tabloları oluşturan classtir. 
DTOs bölümü controller larda kullanılacak post edilen request classlardır.

Entities db tablolarıdır.

PaymentTypes (ödeme tipi) baseentity den türettim.Id,CreatedDate,Status,Name,IsCard( kartla ödeme tiplerinde true yapılmalıdırı)
ProductTypes (ürün tipi) baseentity den türettim.Id,CreatedDate,Status,Name,SlotCount(ürün tiplerinin slot sayısını tutmaktadır.)
Products (ürün ) baseentity den türettim.Id,CreatedDate,Status,Name,ProductTypeId,Price,IsHotDrink(Sıcak içeceklerde otomat makinasında satın alırken şeker mikarını girdirmek için böyle bir alan koydum.)
ReceiptTransactions (Ödeme Fiş bilgisi transaction ) baseentity den türettim.Id,CreatedDate,Status,ProductId,ProductCount,PaymentTypeId,SugarCount(Sıcak içeceklerde şeker adeti girilmesi sağlanır.),TotalPrice (toplam tutar),RefundedAmount(nakit para vaya bozuk para alındığında iade edilen tutarı tuttum)

SeedData da  api de seeddata classları çağrılmış durumda.Uygulama ayağa kalktığında önce ödeme tipleri ,ürün tipleri en sonunda dökümanda belirtilmiş ürün tipi slot sayısına göre   sisteme ürünler oluşturulmaktadır.

#OtomatMachine.Shared

Controllera dönene Dataresult bu katmandan kullanmaktayım ve Enum status buradadır.

#OtomatMachineAPI
PaymentTypeController:AddPaymentType,DeletePaymentTye,GetPaymentTypeList
ProductController:AddProduct,DeleteProduct,GetProductList
PaymentTypeController:AddPaymentType,DeletePaymentType,GetPaymentTypeList
ReceiptTransactionController:AddReceiptTransaction,GetReceiptTransactionList

#OtomatMachineIntegrationTest
Entegrasyon testlerini yazdığım katman.



