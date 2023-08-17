using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Price).InclusiveBetween(1,int.MaxValue).WithMessage("{PropertyName} must be greater than 0.");
            RuleFor(x => x.Stock).InclusiveBetween(1,int.MaxValue).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
