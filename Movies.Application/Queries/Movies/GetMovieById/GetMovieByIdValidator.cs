using FluentValidation;
using Movies.Domain.Entities;

namespace Movies.Application.Queries.Movies.GetMovieById;
public class GetMovieByIdValidator : AbstractValidator<GetMovieByIdQuery>
{
    public GetMovieByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage($"{nameof(Movie.Id)} cannot be empty");
    }
}
