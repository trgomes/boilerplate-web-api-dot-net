using AutoMapper;
using Solution.Application.ViewModels;
using Solution.Domain.Enums;
using Solution.Domain.Models;
using Solution.Domain.UseCases.AccountUseCases.RegisterNewAccount;

namespace Solution.Application.AutoMappers
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserRegisterViewModel, User>()
                .ConvertUsing(c => new User(c.Name, c.Email, c.Password, EUserProfile.User, true));

            CreateMap<UserRegisterViewModel, RegisterNewAccountCommand>();
        }
    }
}
