using AutoMapper;
using PentiaCase.Web.DTOs;
using PentiaCase.Web.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PentiaCase.Web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SalesPersonDto, SalesPerson>();
        CreateMap<OrderLineDto, OrderLine>();
    }
}
