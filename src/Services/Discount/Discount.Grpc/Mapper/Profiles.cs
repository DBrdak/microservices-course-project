using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}

