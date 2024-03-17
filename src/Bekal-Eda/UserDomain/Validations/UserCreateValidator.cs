using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Dtos;
using User.Domain.Entities;

namespace User.Domain.Validations
{
    public class UserCreateValidator : AbstractValidator<UserDto>
    {
        public UserCreateValidator(UserDbContext dbContext)
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .WithName("Username")
                .WithMessage("Username is Required");
            
            RuleFor(x => x.UserName)
                .Length(1,50)
                .WithName("Username")
                .WithMessage("Length 1-50");

            RuleFor(x => new { x.UserName })
                .Must(x =>
                {
                    return !dbContext.Users.Where(o => o.UserName == x.UserName).Any();
                })
                .WithName("Username")
                .WithMessage("Username Already Exists");


            RuleFor(x => x.Password)
                .NotNull()
                .WithName("Password")
                .WithMessage("Password is Required");

            RuleFor(x => x.Password)
                .Length(1,100)
                .WithName("Password")
                .WithMessage("Password Length 1 - 100 ");

            RuleFor(x => x.FirstName)
                .NotNull()
                .WithName("FirstName")
                .WithMessage("FirstName is Required");

            RuleFor(x => x.FirstName)
                .Length(1, 50)
                .WithName("FirstName")
                .WithMessage("FirstName Length 1-50");

            RuleFor(x => x.LastName)
                .NotNull()
                .WithName("LastName")
                .WithMessage("LastName is Required");

            RuleFor(x => x.LastName)
                .Length(1, 50)
                .WithName("LastName")
                .WithMessage("FirstName Length 1-50");

            RuleFor(x => x.Email)
                .NotNull()
                .WithName("Email")
                .WithMessage("Email is Required");

            RuleFor(x => x.Email)
                .Length(1, 100)
                .WithName("Email")
                .WithMessage("Email Length 1-50");

            RuleFor(x => new { x.Email })
                .Must(x =>
                {
                    return !dbContext.Users.Where(o => o.Email == x.Email).Any();
                })
                .WithName("Email")
                .WithMessage("Email Already Exists");

            RuleFor(x => x.Type)
                .NotNull()
                .WithName("Type")
                .WithMessage("Type is Required");

            RuleFor(x => x.Status)
                .NotNull()
                .WithName("Status")
                .WithMessage("Status is Required");
        }
    }
}
