using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure;

namespace Movies.Application.Commands.Movies.DeleteMovie;
public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Unit>
{
    private readonly MoviesDbContext _moviesDbContext;
    public DeleteMovieCommandHandler(MoviesDbContext moviesDbContext)
    {
        _moviesDbContext = moviesDbContext;
    }
    public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movieToDelete = await _moviesDbContext.Movies
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (movieToDelete == null)
        {
             throw new KeyNotFoundException($"Movie with ID {request.Id} not found.");
        }

        _moviesDbContext.Movies.Remove(movieToDelete);
        await _moviesDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}