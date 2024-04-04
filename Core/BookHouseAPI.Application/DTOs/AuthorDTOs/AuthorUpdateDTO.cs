using BookHouseAPI.Application.DTOs.BookDTOs;

namespace BookHouseAPI.Application.DTOs.AuthorDTOs
{
    public class AuthorUpdateDTO
    {
        public List<BookDTO> Books { get; set; }
    }
}
