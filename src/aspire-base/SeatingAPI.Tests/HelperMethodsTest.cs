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
}
