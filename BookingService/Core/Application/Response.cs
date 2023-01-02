namespace Application
{
    public enum ErrorCodes
    {
        // Guests related codes 1 to 99
        NOT_FOUND = 1,
        COULDNOT_STORE_DATA = 2,
        INVALID_PERSON_ID = 3,
        MISSING_REQUIRED_INFORMATION = 4,
        INVALID_EMAIL = 5,
        GUEST_NOT_FOUND = 6,

        // Rooms related codes 100 to 199
        ROOM_NOT_FOUND = 100,
        ROOM_COULD_NOT_STORE = 101,
        ROOM_PERSONAL_ID = 102,
        ROOM_MISSING_REQUIRED_INFORMATION = 103,
        ROOM_INVALID_EMAIL = 104,

        // Bookings related codes 200 to 499
        BOOKING_NOT_FOUND = 200,
        BOOKING_COULD_NOT_STORE = 201,
        BOOKING_PLACEDAT_MISSING_REQUIRED_INFOMRATION = 202,
        BOOKING_START_MISSING_REQUIRED_INFOMRATION = 203,
        BOOKING_END_MISSING_REQUIRED_INFOMRATION = 204,
        BOOKING_GUEST_MISSING_REQUIRED_INFORMATION = 205,
        BOOKING_ROOM_MISSING_REQUIRED_INFORMATION = 206,
        BOOKING_ROOM_CANNOT_BE_BOOKED = 207,
    }
    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
