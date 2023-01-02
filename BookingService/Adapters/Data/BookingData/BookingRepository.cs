using Domain.BookingDomain.Entities;
using Domain.BookingDomain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.BookingData
{
    public class BookingRepository : IBookingRepository
    {
        private HotelDbContext _hotelDbContext;

        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<int> Create(Booking booking)
        {
            _hotelDbContext.Bookings.Add(booking);
            await _hotelDbContext.SaveChangesAsync();
            return booking.Id;
        }
        
        public async Task<Booking?> Get(int Id)
        {
            return await _hotelDbContext.Bookings
                .Where(b => b.Id == Id).FirstOrDefaultAsync();
        }
    }
}
