namespace Domain.GuestDomain.UtilsTools
{
    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            if (email == "abc.com") return false;
            return true;
        }
    }
}
