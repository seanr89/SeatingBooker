
public record LocationBookingDTO(int Id, string Name, List<LocationDeskDTO> Desks);

public record LocationDeskDTO(int Id, string Name, bool IsHotDesk, string StaffName, bool Active, 
    List<BookingRequestDTO> Bookings);