using MediatR;
using Movies.Application.Commands.Movies.CreateMovie;
using Movies.Application.Commands.Movies.DeleteMovie;
using Movies.Application.Commands.Movies.UpdateMovie;
using Movies.Application.Queries.Movies.GetMovieById;
using Movies.Application.Queries.Movies.GetMovies;
using Movies.Contracts.Requests.Movies;

namespace Movies.Server.EndPoints;

public static class MoviesEndPoints
{
    public static void MapMoviesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/movies", async (IMediator mediator, CancellationToken ct) =>
        {
            var movies = await mediator.Send(new GetMoviesQuery(), ct);
            return Results.Ok(movies);
        }).WithTags("Movies");

        app.MapGet("/api/movies/{id}", async (int id, IMediator mediator, CancellationToken ct) =>
        {
            var movie = await mediator.Send(new GetMovieByIdQuery(id), ct);
            return movie is not null ? Results.Ok(movie) : Results.NotFound();
        }).WithTags("Movies");

        app.MapPost("/api/movies", async (IMediator mediator, CreateMovieRequest createMovieRequest, CancellationToken ct) =>
        {
            var command = new CreateMovieCommand(createMovieRequest.Title, createMovieRequest.Description, createMovieRequest.Category);
            var result = await mediator.Send(command, ct);
            return Results.Created($"/api/movies/{result}", new { Id = result });
        }).WithTags("Movies");

        app.MapPut("/api/movies/{id}", async (int id, IMediator mediator, UpdateMovieRequest updateMovieRequest, CancellationToken ct) =>
        {
            var command = new UpdateMovieCommand(id, updateMovieRequest.Title, updateMovieRequest.Description, updateMovieRequest.Category);
            var result = await mediator.Send(command, ct);
            return Results.Ok(result);

        }).WithTags("Movies");

        app.MapDelete("/api/movies/{id}", async (int id, IMediator mediator, CancellationToken ct) =>
        {
            var command = new DeleteMovieCommand(id);
            await mediator.Send(command, ct);
            return Results.NoContent();
        }).WithTags("Movies");
    }
}
