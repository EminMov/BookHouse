﻿using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Persistance.Implementetions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookHouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await _authorService.GetAllAuthorsAsync();
            //return Ok(result);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("/api/Author/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAuthorById([FromRoute] int id)
        {
            var result = await _authorService.AuthorGetByIDAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorAddDTO authorAddDTO)
        {
            var result = await _authorService.AuthorAddAsync(authorAddDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAuthor([FromBody] AuthorUpdateDTO authorUpdateDTO, int id) 
        {
            var result = await _authorService.AuthorUpdateAsync(authorUpdateDTO, id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("/api/Author/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id) 
        { 
            var result = await _authorService.AuthorDeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("most-popular")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetMostPopularAuthor()
        {
            var result = await _authorService.GetMostPopularAuthorAsync();
            return StatusCode(result.StatusCode, result);
        }
    }
}
