using AdessoWorldLeague.Domain.Common.Abstractions;
using AdessoWorldLeague.Domain.Models;
using AdessoWorldLeague.Domain.Models.DrawResults;
using AdessoWorldLeague.Domain.Models.Teams;
using AdessoWorldLeague.Infrastructure.Persistence;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdessoWorldLeague.Application.Features.Commands;

public sealed record DrawGroupsCommand(int NumberOfGroups, string PersonName, string PersonSurname) : IRequest<Result<Guid>>;

public sealed class DrawGroupsCommandValidator : AbstractValidator<DrawGroupsCommand>
{
    public DrawGroupsCommandValidator()
    {
        RuleFor(x => x.PersonName).NotEmpty().WithMessage("Person name is required.");
        RuleFor(x => x.PersonSurname).NotEmpty().WithMessage("Person surname is required.");
        RuleFor(x => x.NumberOfGroups).NotEmpty().WithMessage("Number of groups is required.");
    }
}

public sealed class DrawGroupsCommandHandler(AdessoWorldLeagueDbContext context,IDateTimeProvider dateTimeProvider) : IRequestHandler<DrawGroupsCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DrawGroupsCommand request, CancellationToken cancellationToken)
    {
        List<Team> teams = await context.Teams.AsQueryable().ToListAsync(cancellationToken);
        Ruler ruler = new Ruler(request.PersonName,request.PersonSurname);
        DrawResultDate date = new DrawResultDate(dateTimeProvider.Now);
        Result<DrawResult> result = DrawResult.Create(ruler,date,teams,request.NumberOfGroups);
        if (!result.IsSuccess)
        {
            return Result.Fail(result.Errors);
        }
        await context.DrawRecords.AddAsync(result.Value,cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return result.IsSuccess ? Result.Ok(result.Value.Id.Value) : Result.Fail(result.Errors);
        
    }
}

