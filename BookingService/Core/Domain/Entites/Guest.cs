using Domain.Exceptions;
using Domain.ValueObjects;
using Domain.UtilsTools;
using Domain.Ports;

namespace Domain.Entites
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public PersonId DocumentId { get; set; }
        
        private void ValidateState()
        {
            if (DocumentId == null ||
                DocumentId.IdNumber.Length <= 3 ||
                DocumentId.DocumentType == 0)
            {
                throw new InvalidPersonDocumentIdException();
            }

            if(Name == null || SurName == null || Email == null)
            {
                throw new MissingRequiredInformation();
            }

            if (!Utils.ValidateEmail(this.Email))
            {
                throw new InvalidEmailException();
            }
        }

        public async Task Save(IGuestRepository guestRepository)
        {
            this.ValidateState();

            if (this.Id == 0)
            {
                this.Id = await guestRepository.Create(this);
            } else
            {
                // await guestRepository.Update(this);
            }
        }
    }
}
