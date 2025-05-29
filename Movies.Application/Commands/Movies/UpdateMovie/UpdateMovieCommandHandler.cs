using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure;

namespace Movies.Application.Commands.Movies.UpdateMovie;
public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Unit>
{
    private readonly MoviesDbContext _moviesDbContext;

    public UpdateMovieCommandHandler(MoviesDbContext moviesDbContext)
    {
        _moviesDbContext = moviesDbContext;
    }
    public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movieToUpdate = await _moviesDbContext.Movies
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (movieToUpdate == null)
        {
            throw new KeyNotFoundException($"Movie with ID {request.Id} not found.");
        }

        movieToUpdate.Title = request.Title;
        movieToUpdate.Description = request.Description;
        movieToUpdate.Category = request.Category;

        _moviesDbContext.Movies.Update(movieToUpdate);
        await _moviesDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
