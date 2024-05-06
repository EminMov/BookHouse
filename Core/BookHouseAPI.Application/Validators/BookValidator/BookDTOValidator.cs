using BookHouseAPI.Application.DTOs.BookDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Validators.BookValidator
{
    public class BookDTOValidator : AbstractValidator<BookDTO>
    {
        public BookDTOValidator() 
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters");

            RuleFor(book => book.ISBN)
                .NotEmpty().WithMessage("ISBN is required")
                .Length(13).WithMessage("ISBN must be 13 characters long");

            RuleFor(book => book.GenreId)
                .NotEmpty().WithMessage("GenreId is required")
                .GreaterThan(0).WithMessage("GenreId must be greater than 0");
        }
    }
}
