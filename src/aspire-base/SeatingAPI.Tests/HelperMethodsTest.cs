using Xunit;
using SeatingAPI.Utils;
using SeatingAPI.Entities.Enums;

public class HelperMethodsTest
{
    [Theory]
    [InlineData(RequestState.Free, "Free")]
    [InlineData(RequestState.Booked, "Booked")]
    [InlineData(RequestState.Pending, "Pending")]
    [InlineData(RequestState.Cancelled, "Cancelled")]
    [InlineData(RequestState.Denied, "Unknown")]
    [InlineData(null, "Unknown")]
    public void GetStringFromRequestState_ReturnsExpectedString(RequestState? state, string expected)
    {
        var result = HelperMethods.GetStringFromRequestState(state);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(SeatTypes.Unknown, "Unknown")]
    [InlineData(SeatTypes.Standard, "Standard")]
    [InlineData(SeatTypes.Reserved, "Reserved")]
    [InlineData(SeatTypes.HotDesk, "Hot Desk")]
    [InlineData(SeatTypes.Standing, "Standing")]
    [InlineData(SeatTypes.MeetingRoom, "Meeting Room")]
    [InlineData(SeatTypes.BreakoutArea, "Breakout Area")]
    [InlineData((SeatTypes)99, "Unknown")]
    public void GetStringFromSeatType_ReturnsExpectedString(SeatTypes type, string expected)
    {
        var result = HelperMethods.GetStringFromSeatType(type);
        Assert.Equal(expected, result);
    }
}
