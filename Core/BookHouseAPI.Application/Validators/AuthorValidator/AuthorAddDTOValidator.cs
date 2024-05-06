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
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters");

            RuleFor(author => author.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters");

            RuleFor(author => author.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(100).WithMessage("Country cannot be longer than 100 characters");

            RuleFor(author => author.Biography)
                .MaximumLength(500).WithMessage("Biography cannot be longer than 500 characters");

            RuleForEach(author => author.Books)
                .SetValidator(new BookDTOValidator());
        }
    }
}
