using AutoMapper;
using Movie.Core.DomainEntities;
using Movie.Core.Dtos;


namespace Movie.Data.DataConfigurations;

public class MapperProfile : Profile
{
    public MapperProfile()
    {

        CreateMap<Core.DomainEntities.Movie, MovieDto>();
        CreateMap<Core.DomainEntities.Movie, MovieCreateDto>().ReverseMap();
        CreateMap<Core.DomainEntities.Movie, MovieUpdateDto>().ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Detailes, opt => opt.Ignore())
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.Actors, opt => opt.Ignore())
            .ForMember(dest => dest.Genres, opt => opt.Ignore());
        CreateMap<Core.DomainEntities.Movie, MoviePatchDto>()
            .ForMember(dest => dest.Detailes, opt => opt.MapFrom(src => src.Detailes));
        CreateMap<Core.DomainEntities.Movie, MovieAllDetailsDto>()
            .AfterMap((src, dest) =>
            {
                if (src.Actors?.Count == 0)
                    dest.Actors = null;

                if (src.Genres?.Count == 0)
                    dest.Genres = null;

                if (src.Reviews?.Count == 0)
                    dest.Reviews = null;
            });
        CreateMap<MoviePatchDto, Core.DomainEntities.Movie>().ForMember(dest => dest.Detailes, opt => opt.Ignore());
        CreateMap<MovieDetailes, MovieDetailesDto>();
        CreateMap<MovieDetailes, MovieDetailesCreateDto>().ReverseMap();
        CreateMap<MovieDetailes, MovieDetailesPatchDto>().ReverseMap();

        CreateMap<Actor, ActorDto>();

        CreateMap<Genre, GenreDto>();

        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewCreateDto, Review>();
        CreateMap<Review, ReviewDetailsDto>();
    }
}
