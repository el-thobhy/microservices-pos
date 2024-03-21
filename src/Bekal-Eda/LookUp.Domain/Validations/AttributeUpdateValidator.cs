using FluentValidation;
using LookUp.Domain.Dtos;
using LookUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUp.Domain.Validations
{
    public class AttributeUpdateValidator: AbstractValidator<AttributeExceptStatusDto>
    {
        public AttributeUpdateValidator(LookUpDbContext dbContext)
        {
            RuleFor(x => x.Type)
                .NotNull()
                .WithName("Type")
                .WithMessage("Type Is Required");

            RuleFor(x => x.Unit)
                .NotNull()
                .NotEmpty()
                .WithName("Unit")
                .WithMessage("Unit Is Required");

            RuleFor(x => x.Id)
                .NotNull()
                .WithName("Id")
                .WithMessage("Id Is Required");

            RuleFor(x => x.Unit)
               .Length(1, 100)
               .WithName("Unit")
               .WithMessage("Length 1 - 100");

            RuleFor(x => new { x.Unit, x.Id})
                .Must(x =>
                {
                    return !dbContext.Attributes.Where(o => o.Unit == x.Unit && o.Id != x.Id).Any();
                })
                .WithName("Unit")
                .WithMessage("Unit Already Exist");
        }
    }
}
