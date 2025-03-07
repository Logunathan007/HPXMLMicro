﻿namespace EnergyScore.Application.Mappers.DTOS.AddressDTOS
{
    public class AddressDTO
    {
        public Guid Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
    }
}
