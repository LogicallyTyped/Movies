using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Contracts.Responses;
using Movies.Infrastructure;

namespace Movies.Application.Queries.Movies.GetMovieById;
public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, GetMovieByIdResponse>
{
    private readonly MoviesDbContext _moviesDbContext;

    public GetMovieByIdQueryHandler(MoviesDbContext moviesDbContext)
    {
        _moviesDbContext = moviesDbContext;
    }

    public async Task<GetMovieByIdResponse> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _moviesDbContext.Movies.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (movie is null)
        {
            throw new Exception($"Movie with ID {request.Id} not found.");
        }
        return movie.Adapt<GetMovieByIdResponse>();
    }
}