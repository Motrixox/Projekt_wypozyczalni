using AutoMapper;

namespace MC_Projekt_wypozyczalni.Models
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleDetailViewModel>();
            CreateMap<Vehicle, VehicleItemViewModel>();
        }
    }
}
