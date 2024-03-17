using FluentValidation;
using Store.Domain.Dtos;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Validations
{
    public class CategoryCreateValidator : AbstractValidator<CategoryInputDto>
    {
        public CategoryCreateValidator(StoreDbContext dbContext)
        {
            RuleFor(x => new { x.Name })
               .Must(x =>
               {
                   return !dbContext.Categories.Where(o => o.Name == x.Name).Any();
               })
               .WithName("Name")
               .WithMessage("Name Already Exist");

            RuleFor(x => x.Name)
               .NotNull()
               .WithName("Name")
               .WithMessage("Name Is Required");

            RuleFor(x => x.Name )
               .Length(1, 50)
               .WithName("Name")
               .WithMessage("Length 1 - 50");

            RuleFor(x => x.Descriprion)
               .Length(1, 250)
               .WithName("Description")
               .WithMessage("Length 1 - 255");

            RuleFor(x => x.Descriprion)
               .NotNull()
               .WithName("Description")
               .WithMessage("Description Is Required");
        }
    }
}
