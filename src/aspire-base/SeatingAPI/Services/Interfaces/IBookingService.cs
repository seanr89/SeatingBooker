using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBookingService
{
    Task<List<BookingRequest>> GetBookings();
    Task<BookingRequest?> GetBooking(int id);
    Task<List<BookingRequest>> GetBookingsForLocation(int locationId);
    Task<List<BookingRequest>> GetBookingsForDesk(int deskId);
    Task<BookingRequest?> CreateBooking(CreateBookingRequestDTO booking);
    Task<List<BookingRequest>> GetBookingsForLocationOnDate(int locationId, DateTime date);
    Task<bool> CancelBooking(int id);
}
