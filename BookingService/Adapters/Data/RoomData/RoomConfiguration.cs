﻿using Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.RoomData
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Price)
                .Property(x => x.Currency);
            builder.OwnsOne(x => x.Price)
                .Property(x => x.Value);
        }
    }
}