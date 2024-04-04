using AutoMapper;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Application.DTOs.GenreDTOs;
using BookHouseAPI.Application.DTOs.ReviewDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using BookHouseAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Persistance.Implementetions.Services
{
    internal class ReviewService : IReviewService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper) 
        { 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseModel<ReviewAddDTO>> AddReviewAsync(ReviewAddDTO reviewAdd)
        {
            ResponseModel<ReviewAddDTO> response = new ResponseModel<ReviewAddDTO>();
            Review review = new Review();

            var book = await _unitOfWork.GetRepository<Book>().GetByIdAsync(reviewAdd.BookId);
            if (book == null)
            {
                response.Success = false;
                response.Message = "Book not found";
            }

            review.Title = reviewAdd.Title;
            review.Comment = reviewAdd.Comment;
            review.DateCreated = DateTime.Now;
            review.BookID = reviewAdd.BookId;

            var addedReview = await _unitOfWork.GetRepository<Review>().AddAsync(review);
            var saveData = await _unitOfWork.SaveChangesAsync();

            if (saveData > 0)
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Data = reviewAdd;
                response.Message = "Review successfully added";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = reviewAdd;
                response.Message = "Unfortunatly review was not saved, please try again";
            }
            return response;
        }

        public async Task<ResponseModel<bool>> DeleteReviewAsync(int reviewId)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            var data = await _unitOfWork.GetRepository<Review>().GetByIdAsync(reviewId);
            var result = _unitOfWork.GetRepository<Review>().Remove(data);
            var rowAffected = await _unitOfWork.SaveChangesAsync();

            if(rowAffected > 0)
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Data = true;
                response.Message = "Review successfully deleted";
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = false;
                response.Message = "There are an error with save changes";
            }
            return response;
        }

        public async Task<ResponseModel<List<ReviewGetDTO>>> GetReviewsByBookIdAsync(int bookId)
        {
            ResponseModel<List<ReviewGetDTO>> response = new ResponseModel<List<ReviewGetDTO>>();

            var reviews  = _unitOfWork.GetRepository<Review>().GetAll().Where(r => r.BookID == bookId);
            if (reviews != null)
            {
                var get = _mapper.Map<List<ReviewGetDTO>>(reviews);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Reviews retrieved successfully";
                response.Data = get;
            }
            else
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Data = null;
                response.Message = "No reviews found for this book";
            }
            return response;
        }

        public async Task<ResponseModel<bool>> UpdateReviewAsync(int reviewId, ReviewUpdateDTO reviewUpdate)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();
            var data = await _unitOfWork.GetRepository<Review>().GetByIdAsync(reviewId);

            if (data != null)
            {
                data.Grade = reviewUpdate.Grade;
                data.Comment = reviewUpdate.Comment;

                await _unitOfWork.GetRepository<Review>().AddAsync(data);
                var rawAffected = await _unitOfWork.SaveChangesAsync();
                if (rawAffected > 0)
                {
                    response.Success = true;
                    response.StatusCode = 200;
                    response.Data = true;
                    response.Message = "Review successfully updated";
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.Data = false;
                    response.Message = "There are an error in Save Changes";
                }
            }
            else
            {
                response.Success = false;
                response.StatusCode = 401;
                response.Data = false;
                response.Message = "Using this ID, the review was not found";
            }

            return response;
        }
    }
}
