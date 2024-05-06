using BookHouseAPI.Application.DTOs.BookDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Validators.BookValidator
{
    public class BookUpdateDTOValidator : AbstractValidator<BookUpdateDTO>
    {
        public BookUpdateDTOValidator() 
        {
            RuleFor(book => book.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(book => book.Description)
                .NotEmpty().WithMessage("Description cannot be empty");

            RuleFor(book => book.ReleaseDate)
                .NotEmpty().WithMessage("Release date cannot be empty")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Release date must be in the past");
        }
    }
}
