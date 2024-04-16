using Application.Features.Movies.Constants;
using Application.Features.Movies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Movies.Constants.MoviesOperationClaims;

namespace Application.Features.Movies.Queries.GetById;

public class GetByIdMovieQuery : IRequest<GetByIdMovieResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdMovieQueryHandler : IRequestHandler<GetByIdMovieQuery, GetByIdMovieResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly MovieBusinessRules _movieBusinessRules;

        public GetByIdMovieQueryHandler(IMapper mapper, IMovieRepository movieRepository, MovieBusinessRules movieBusinessRules)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _movieBusinessRules = movieBusinessRules;
        }

        public async Task<GetByIdMovieResponse> Handle(GetByIdMovieQuery request, CancellationToken cancellationToken)
        {
            Movie? movie = await _movieRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _movieBusinessRules.MovieShouldExistWhenSelected(movie);

            GetByIdMovieResponse response = _mapper.Map<GetByIdMovieResponse>(movie);
            return response;
        }
    }
}