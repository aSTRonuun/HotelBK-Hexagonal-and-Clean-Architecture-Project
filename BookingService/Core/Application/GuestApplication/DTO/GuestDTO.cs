using Domain.GuestDomain.Enuns;

namespace Application.GuestApplication.DTO
{
    public class GuestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }
        public static Domain.GuestDomain.Entities.Guest MapToEntity(GuestDTO guestDTO)
        {
            return new Domain.GuestDomain.Entities.Guest
            {
                Id = guestDTO.Id,
                Name = guestDTO.Name,
                SurName = guestDTO.Surname,
                Email = guestDTO.Email,
                DocumentId = new Domain.GuestDomain.ValueObjects.PersonId
                {
                    IdNumber = guestDTO.IdNumber,
                    DocumentType = (DocumentType)guestDTO.IdTypeCode
                }
            };
        }
        public static GuestDTO MapToDto(Domain.GuestDomain.Entities.Guest guest)
        {
            return new GuestDTO
            {
                Id = guest.Id,
                Name = guest.Name,
                Surname = guest.SurName,
                Email = guest.Email,
                IdNumber = guest.DocumentId.IdNumber,
                IdTypeCode = (int)guest.DocumentId.DocumentType
            };
        }
    }
}
