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
    internal class CategoryChangeStatusValidator : AbstractValidator<CategoryStatusDto>
    {
        public CategoryChangeStatusValidator(StoreDbContext dbContext)
        {

            RuleFor(x => x.Id)
               .NotNull()
               .WithName("Id")
               .WithMessage("Id Is Required");

            RuleFor(x => x.Status)
               .NotNull()
               .WithName("Status")
               .WithMessage("Status Is Required");
        }

    } 
}
