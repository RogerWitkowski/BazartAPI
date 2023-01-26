using Bazart.DataAccess.DataAccess;
using Bazart.Models.Model;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Bazart.DataAccess.Seeder
{
    public class DataGenerator
    {
        private readonly BazartDbContext _dbContext;

        public DataGenerator(BazartDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task GenerateData()
        {
            if (_dbContext.Database.CanConnectAsync().Result)
            {
                if (!_dbContext.Users.AnyAsync().Result)
                {
                    await GetData();
                }
            }
        }

        private async Task GetData()
        {
            var categories = new string[6]
            {
                "Malarstwo",
                "Rzeźba",
                "Fotografia",
                "Rękodzieło",
                "Grafika Komputerowa",
                "Inne"
            };

            var categoryData = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Random.ArrayElement(categories))
                .RuleFor(c => c.Description, f => f.Lorem.Sentence(2, 1))
                .RuleFor(c => c.ImageUrl, f => f.Image.PlaceImgUrl(640, 480, "category"));

            var productData = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.IsForSell, f => f.Random.Bool())
                .RuleFor(p => p.ImageUrl, f => f.Image.PlaceImgUrl(640, 480, "painting"))
                .RuleFor(p => p.Category, f => categoryData.Generate());

            var addressData = new Faker<Address>()
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Street, f => f.Address.StreetAddress())
                .RuleFor(a => a.HouseOrFlatNumber, f => int.Parse(f.Address.BuildingNumber()))
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode());

            var eventDetailData = new Faker<EventDetail>()
                .RuleFor(ed => ed.Country, f => f.Address.Country())
                .RuleFor(ed => ed.City, f => f.Address.City())
                .RuleFor(ed => ed.Street, f => f.Address.StreetAddress())
                .RuleFor(ed => ed.HouseOrFlatNumber, f => int.Parse(f.Address.BuildingNumber()))
                .RuleFor(ed => ed.PostalCode, f => f.Address.ZipCode())
                .RuleFor(ed => ed.ImageUrl, f => f.Image.LoremPixelUrl("event", 640, 480, true, true));

            var eventData = new Faker<Event>()
                .RuleFor(e => e.Name, f => f.Lorem.Word())
                .RuleFor(e => e.Description, f => f.Lorem.Sentence(3))
                .RuleFor(e => e.EventDetails, f => eventDetailData.Generate());

            var userData = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(p => p.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("(##)-###-###-###"))
                .RuleFor(p => p.Nationality, f => f.Address.Country())
                .RuleFor(u => u.Products, f => productData.Generate(10))
                .RuleFor(u => u.Events, f => eventData.Generate(3))
                .RuleFor(u => u.Address, f => addressData.Generate());

            var users = userData.Generate(20);
            await _dbContext.Users.AddRangeAsync(users);
            await _dbContext.SaveChangesAsync();
        }
    }
}