using Domain.GuestDomain.Enuns;
using Domain.RoomDomain.Entities;

namespace Application.RoomApplication.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public decimal Price { get; set; }
        public AcceptedCurrencies Currency { get; set; }
        public static Domain.RoomDomain.Entities.Room MapToEntity(RoomDTO dto)
        {
            return new Domain.RoomDomain.Entities.Room
            {
                Id = dto.Id,
                Name = dto.Name,
                Level = dto.Level,
                InMaintnance = dto.InMaintenance,
                Price = new Domain.GuestDomain.ValueObjects.Price
                {
                    Currency = dto.Currency,
                    Value = dto.Price
                }
            };
        }
    }
}
