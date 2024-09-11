using Application.Features.Messages.Commands.SendMessage;
using Application.Features.Messages.Queires.GetListByChatId;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Message, SentMessageResponse>();
        CreateMap<SendMessageCommand, Message>();
        CreateMap<Message, GetListByChatIdMessageListItemDto>().ForMember(dest => dest.SenderName,
            opt => opt.MapFrom(src => src.User.Nickname));
    }
}
