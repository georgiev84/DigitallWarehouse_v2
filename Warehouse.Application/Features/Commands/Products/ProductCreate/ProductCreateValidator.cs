using FluentValidation;
using Warehouse.Application.Common.Validation;

namespace Warehouse.Application.Features.Commands.Products.ProductCreate;

public class ProductCreateValidator : AbstractValidator<ProductCreateCommand>
{
    public ProductCreateValidator()
    {
        RuleFor(command => command.BrandId)
             .NotEmpty().WithMessage(ValidationMessages.RequiredId(nameof(ProductCreateCommand.BrandId)));

        RuleFor(command => command.Title)
            .NotEmpty().WithMessage(ValidationMessages.RequiredItem(nameof(ProductCreateCommand.Title)))
            .MaximumLength(100).WithMessage(ValidationMessages.ItemExceedCharacters(nameof(ProductCreateCommand.Title), 100));

        RuleFor(command => command.Description)
            .NotEmpty().WithMessage(ValidationMessages.RequiredItem(nameof(ProductCreateCommand.Description)))
            .MaximumLength(500).WithMessage(ValidationMessages.ItemExceedCharacters(nameof(ProductCreateCommand.Description), 500));

        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage(ValidationMessages.ItemBiggerThanZero(nameof(ProductCreateCommand.Price)));

        RuleFor(command => command.GroupIds)
            .NotEmpty().WithMessage(ValidationMessages.IdMustBeProvided(nameof(ProductCreateCommand.GroupIds)));

        RuleFor(command => command.Sizes)
            .NotEmpty().WithMessage(ValidationMessages.IdMustBeProvided(nameof(ProductCreateCommand.Sizes)));
    }
}