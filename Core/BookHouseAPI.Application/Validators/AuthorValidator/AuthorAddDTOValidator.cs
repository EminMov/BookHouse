using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.Validators.BookValidator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Validators.AuthorValidator
{
    public class AuthorAddDTOValidator : AbstractValidator<AuthorAddDTO>
    {
        public AuthorAddDTOValidator() 
        {
            RuleFor(author => author.FirstName)
                .NotNull().NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters");

            RuleFor(author => author.LastName)
                .NotNull().NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters");

            RuleFor(author => author.Country)
                .NotNull().NotEmpty().WithMessage("Country is required")
                .MaximumLength(100).WithMessage("Country cannot be longer than 100 characters");

            RuleFor(author => author.Biography)
                .NotNull().MaximumLength(500).WithMessage("Biography cannot be longer than 500 characters");

            RuleFor(author => author.Books)
                .NotNull().WithMessage("Books collection is required")
                .NotEmpty().WithMessage("Books collection is required")
                .ForEach(book => book.SetValidator(new BookDTOValidator()));

        }
    }
}
