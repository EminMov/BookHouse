using BookHouseAPI.Application.DTOs.GenreDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Validators.GenreValidator
{
    public class GenreDTOValidator : AbstractValidator<GenreDTO>
    {
        public GenreDTOValidator() 
        {
            RuleFor(genre => genre.Name)
                .NotEmpty().WithMessage("Genre name cannot be empty");

            RuleFor(genre => genre.GenreDescription)
                .NotEmpty().WithMessage("Genre description cannot be empty");
        }
    }
}
