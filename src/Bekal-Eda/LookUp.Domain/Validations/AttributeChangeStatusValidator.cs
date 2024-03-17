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
    public class AttributeChangeStatusValidator : AbstractValidator<AttributeStatusDto>
    {
        public AttributeChangeStatusValidator(LookUpDbContext dbContext)
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
