using AutoMapper;
using TenantProductManager.Application.Dtos.User;
using TenantProductManager.Domain.Entities;

namespace TenantProductManager.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>()
           .ConstructUsing(src => new UserResponse(
               src.Id.ToString(),
               src.Name,
               src.Email,
               src.TenantId,
               src.IsAdmin
           ));
        }
    }
}
