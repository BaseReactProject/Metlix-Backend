using Application.Features.Movies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using Application.Features.OperationClaims.Constants;
using Microsoft.AspNetCore.Http;
using Application.Services.VideoService;

namespace Application.Features.Movies.Commands.Create;

public class CreateMovieCommand : IRequest<CreatedMovieResponse>, /*ISecuredRequest,*/ ILoggableRequest, ITransactionalRequest
{
    public IFormFile Movie { get; set; }

    public CreateMovieCommand(IFormFile movie)
    {
        Movie = movie;
    }

    public CreateMovieCommand()
    {
        Movie = new FormFile(Stream.Null,0, 0, "", "");
    }

    public string[] Roles => new[] { GeneralOperationClaims.Admin };

    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, CreatedMovieResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly MovieBusinessRules _movieBusinessRules;
        private readonly VideoServiceBase videoServiceBase;

        public CreateMovieCommandHandler(IMapper mapper, IMovieRepository movieRepository,
                                         MovieBusinessRules movieBusinessRules, VideoServiceBase videoServiceBase)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _movieBusinessRules = movieBusinessRules;
            this.videoServiceBase = videoServiceBase;
        }

        public async Task<CreatedMovieResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            Movie movie = _mapper.Map<Movie>(request);
            movie.MovieUrl=await videoServiceBase.UploadAsync(request.Movie);
            await _movieRepository.AddAsync(movie);

            CreatedMovieResponse response = _mapper.Map<CreatedMovieResponse>(movie);
            return response;
        }
    }
}