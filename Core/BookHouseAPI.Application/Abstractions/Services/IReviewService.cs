using BookHouseAPI.Application.DTOs.ReviewDTOs;
using BookHouseAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHouseAPI.Application.Abstractions.Services
{
    public interface IReviewService
    {
        public Task<ResponseModel<ReviewAddDTO>> AddReviewAsync(ReviewAddDTO reviewDTO);
        public Task<ResponseModel<bool>> UpdateReviewAsync(int reviewId, ReviewUpdateDTO reviewDTO);
        public Task<ResponseModel<bool>> DeleteReviewAsync(int reviewId);
        public Task<ResponseModel<List<ReviewGetDTO>>> GetReviewsByBookIdAsync(int bookId);
    }
}
