﻿using Application.Features.Messages.Commands.SendMessage;
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
        CreateMap<Message, SendMessageResponse>();
        CreateMap<SendMessageCommand, Message>();
    }
}
