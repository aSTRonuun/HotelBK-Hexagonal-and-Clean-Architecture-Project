using Domain.ValueObjects;

namespace Domain.Entites
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public PersonId DocumentId { get; set; }
    }
}
