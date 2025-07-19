using AutoMapper;
using EduConnect.Application.DTOs.Requests.ConversationRequests;
using EduConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.Mappings
{
    public class ConversationProfile : Profile
    {
        public ConversationProfile()
        {
            CreateMap<Conversation, Conversation>();
            CreateMap<MessageRequest, Message>();
            CreateMap<CreateConversationRequest, Conversation>()
            .ForMember(des => des.Messages, opt => opt.MapFrom(src => src.Messages));
        }

    }
}
