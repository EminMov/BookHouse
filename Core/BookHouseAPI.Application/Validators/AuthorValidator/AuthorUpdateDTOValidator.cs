using BookHouseAPI.Application.DTOs.AuthorDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Validators.AuthorValidator
{
    public class AuthorUpdateDTOValidator : AbstractValidator<AuthorUpdateDTO>
    {
        public AuthorUpdateDTOValidator() 
        {
            RuleFor(author => author.Books)
                .NotEmpty().WithMessage("Books collection cannot be empty");
        }
    }
}
