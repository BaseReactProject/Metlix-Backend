using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.ForgetPassword;

public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordCommand>
{
    public ForgetPasswordValidator()
    {
        RuleFor(fp => fp.Email).NotEmpty().EmailAddress();
    }
}
