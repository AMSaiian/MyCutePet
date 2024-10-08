﻿using FluentValidation;

namespace Taskio.Application.Tasks.Commands.Delete;

public sealed class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
