using Domain.GuestDomain.Enuns;

namespace Domain.GuestDomain.ValueObjects
{
    public class PersonId
    {
        public string IdNumber { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}
