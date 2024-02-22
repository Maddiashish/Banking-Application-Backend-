using AutoMapper;
using Banking1.Dto;
using Banking1.Models;
namespace Banking1.Mapping

{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerBalance, CustomerBalanceDto>();
            CreateMap<CustomerBalanceDto, CustomerBalance>();
            CreateMap<CustomerPoint, CustomerPointDto>();
            CreateMap<CustomerBalanceDto, CustomerBalance>();

        }
    }
}
