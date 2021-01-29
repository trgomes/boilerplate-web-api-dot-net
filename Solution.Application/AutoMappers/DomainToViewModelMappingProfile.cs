using AutoMapper;
using Solution.Application.ViewModels;
using Solution.Domain.Models;

namespace Solution.Application.AutoMappers
{
    public class DomainToViewModelMappingProfile: Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
