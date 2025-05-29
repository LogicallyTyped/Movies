using FluentValidation;
using Movies.Domain.Entities;

namespace Movies.Application.Commands.Movies.CreateMovie;
public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage($"{nameof(Movie.Title)} cannot be empty")
            .MaximumLength(100).WithMessage($"{nameof(Movie.Title)} must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage($"{nameof(Movie.Description)} cannot be empty")
            .MaximumLength(1_000).WithMessage($"{nameof(Movie.Description)} cannot be empty");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage($"{nameof(Movie.Category)} cannot be empty")
            .MaximumLength(30).WithMessage($"{nameof(Movie.Category)} must not exceed 30 characters.");
    }
}
