using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Movie.Contracts;
using Movie.Core.DomainContracts;
using Movie.Core.Dtos;
using Movie.Core.Exceptions;

namespace Movie.Services;

public class GenreService : IGenreService
{
    private IUnitOfWork uow;
    private readonly IMapper mapper;


    public GenreService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<GenreDto>> GetGenresAsync(bool trackChanges = false)
    {
        return mapper.Map<IEnumerable<GenreDto>>(await uow.GenreRepository.GetAllAsync(trackChanges));
    }
}
