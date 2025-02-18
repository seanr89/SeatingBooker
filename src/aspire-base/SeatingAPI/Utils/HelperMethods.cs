
public static class HelperMethods
{
    public static string GetStringFromRequestState(RequestState? state)
    {
        if (state == null)
        {
            return "Unknown";
        }
        return state switch
        {
            RequestState.Free => "Free",
            RequestState.Booked => "Booked",
            RequestState.Pending => "Pending",
            RequestState.Cancelled => "Cancelled",
            _ => "Unknown"
        };
    }
}