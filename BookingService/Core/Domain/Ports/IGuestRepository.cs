﻿using Domain.Entites;

namespace Domain.Ports
{
    public interface IGuestRepository
    {
        Task<Guest> Get(int id);
        Task<int> Create(Guest guest);
    }
}
