using BookHouseAPI.Application.DTOs.UserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Validators.UserValidator
{
    public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateDTOValidator() 
        {
            RuleFor(x => x.UserName)
                .NotEmpty().NotNull().WithMessage("Username cannot be empty.");

            RuleFor(x => x.Password)
                .NotNull().NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(8).MaximumLength(20).WithMessage("Password must have minimum 8 an maximum 20 characters.");

            RuleFor(x => x.Email)
                .NotNull().NotEmpty().WithMessage("Enter valid email.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.BirthDate)
                .NotNull().NotEmpty().WithMessage("BirthDate cannot be empty.");

            RuleFor(x => x.FirstName)
                .NotNull().NotEmpty().WithMessage("Firstname cannot be empty")
                .MaximumLength(50).WithMessage("Firstname cannot be more than 50 characters.");

            RuleFor(x => x.LastName)
                .NotNull().NotEmpty().WithMessage("Lastname cannot be empty.")
                .MaximumLength(50).WithMessage("Lastname cannot be more than 50 characters.");

            RuleFor(x => x.UserId)
                .NotEmpty().NotNull().WithMessage("UserID cannot be empty.");

        }
    }
}
