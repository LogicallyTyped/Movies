using MediatR;
using Movies.Domain.Entities;
using Movies.Infrastructure;

namespace Movies.Application.Commands.Movies.CreateMovie;
public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
{
    private readonly MoviesDbContext _moviesDbContext;

    public CreateMovieCommandHandler(MoviesDbContext moviesDBContext)
    {
        _moviesDbContext = moviesDBContext;
    }
    public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = new Movie
        {
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            CreateDate = DateTime.UtcNow.ToUniversalTime()
        };
        
        await _moviesDbContext.Movies.AddAsync(movie, cancellationToken);
        await _moviesDbContext.SaveChangesAsync(cancellationToken);

        return movie.Id;
    }
}