using BookHouseAPI.Application.DTOs.UserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Validators.UserValidator
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator() 
        {
            RuleFor(b => b.UserName)
                .NotNull().NotEmpty().WithMessage("Username cannot be empty.");

            RuleFor(b => b.FirstName)
                .NotEmpty().NotNull().WithMessage("Firstname cannot be empty.")
                .MaximumLength(50).WithMessage("Name cannot be more than 50 characters");

            RuleFor(b => b.LastName)
                .NotNull().NotEmpty().WithMessage("Lastname cannot be empty.")
                .MaximumLength(50).WithMessage("Lastname cannot be more than 50 characters");

            RuleFor(b => b.Password)
                .NotEmpty().NotNull().WithMessage("Password is required.");

            RuleFor(b => b.Email)
                .NotNull().NotEmpty().WithMessage("Enter valid email.")
                .EmailAddress().WithMessage("Invalid email format.");

        }
    }
}
