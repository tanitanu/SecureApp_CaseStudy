using AutoMapper;
using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;

namespace DiscussionForumAPI.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<DisussionForumUserTwoFactorCodeDTO, DisussionForumUserTwoFactorCode>().ReverseMap();
        CreateMap<DiscussionForumQuestionDTO, DiscussionForumQuestion>().ReverseMap();
        CreateMap<AddQuestionDTO, DiscussionForumQuestion>().ReverseMap();
        CreateMap<UpdateQuestionDTO, DiscussionForumQuestion>().ReverseMap();
        CreateMap<DiscussionForumAnswerDTO, DiscussionForumAnswer>().ReverseMap();
        CreateMap<AddAnswerDTO, DiscussionForumAnswer>().ReverseMap();
        CreateMap<UpdateAnswerDTO, DiscussionForumAnswer>().ReverseMap();
        CreateMap<DiscussionForumLikeDislikeDTO, DiscussionForumLikesDislike>().ReverseMap();
        CreateMap<AddLikeDislikeQuestionDTO, DiscussionForumLikesDislike>().ReverseMap();
        CreateMap<DiscussionForumUserReportedDTO, DiscussionForumUserReported>().ReverseMap();
        CreateMap<AddUpdateUserReporterDTO, DiscussionForumUserReported>().ReverseMap();
        CreateMap<DiscussionForumCategory, DiscussionForumCategoriesDTO>().ReverseMap();
    }
}
