using FluentValidation;
using Store.Domain.Dtos;
using Store.Domain.Entities;

namespace Store.Domain.Validations
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidator(StoreDbContext dbContext)
        {
            RuleFor(x => x.CategoryId)
               .NotNull()
               .Must(x =>
               {
                   return dbContext.Categories.Where(o => o.Id == x).Any();
               })
                .WithName("categoryId")
               .WithMessage("Category not exists!");
            RuleFor(x => x.AttributeId)
               .NotNull()
               .Must(x =>
               {
                   return dbContext.Attributes.Where(o => o.Id == x).Any();
               })
                .WithName("attributeId")
               .WithMessage("Attribute not exists!");
            RuleFor(x => x.Name)
               .NotNull()
               .Length(1, 30)
                .WithName("name")
               .WithMessage("Length 1 - 30");
            RuleFor(x => x.Description)
               .NotNull()
               .Length(1, 255)
                .WithName("description")
               .WithMessage("Length 1 - 255");
            RuleFor(x => x.Sku)
               .NotNull()
               .Length(1, 20)
                .WithName("sku")
               .WithMessage("Length 1 - 20");

            RuleFor(x => new { x.Name })
                .Must(x =>
                {
                    return !dbContext.Products.Where(o => o.Name == x.Name).Any();
                })
                .WithName("name")
                .WithMessage("Name Already Exists");
        }
    }
}
