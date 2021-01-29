using AutoMapper;
using Solution.Application.ViewModels;
using Solution.Domain.Enums;
using Solution.Domain.Models;

namespace Solution.Application.AutoMappers
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserRegisterViewModel, User>()
                .ConvertUsing(c => new User(c.Name, c.Email, c.Password, EUserProfile.User, true));
        }
    }
}
