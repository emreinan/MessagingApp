using AutoMapper;
using WebMVC.Models;
using WebMVC.Services.DTOs;

namespace WebMVC.Services.Auth.Profiles;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<LoginDto, LoginViewModel>().ReverseMap();
	}
}
