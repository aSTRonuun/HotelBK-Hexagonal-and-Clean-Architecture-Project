using Application.BookingApplication.Dtos;
using Application.BookingApplication.Responses;
using MediatR;

namespace Application.BookingApplication.Queries
{
    public class GetBookingQuery : IRequest<BookingResponse>
    {
        public int Id { get; set; }
    }
}
