﻿namespace Domain.UtilsTools
{
    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            return true;
        }
    }
}
