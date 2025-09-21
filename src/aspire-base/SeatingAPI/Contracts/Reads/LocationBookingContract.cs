namespace SeatingAPI.Contracts.Reads
{
    public record LocationBookingContract(int Id, string Name, List<LocationDeskContract> Desks);

    public record LocationDeskContract(int Id, string Name, bool IsHotDesk, string StaffName, bool Active,
        List<BookingRequestContract> Bookings);
}
