
public static class HelperMethods
{
    /// <summary>
    /// Identifies the state of the request
    /// and returns a string representation of it
    /// </summary>
    /// <param name="state">RequestState enum</param>
    /// <returns></returns>
    public static string GetStringFromRequestState(RequestState? state)
    {
        if (state != null)
            return state switch
            {
                RequestState.Free => "Free",
                RequestState.Booked => "Booked",
                RequestState.Pending => "Pending",
                RequestState.Cancelled => "Cancelled",
                _ => "Unknown"
            };
        return "Unknown";
    }
}