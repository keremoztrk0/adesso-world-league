using AdessoWorldLeague.Application.DTOs;
using AdessoWorldLeague.Domain.Models.DrawResults;
using AdessoWorldLeague.Infrastructure.Persistence;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdessoWorldLeague.Application.Features.Queries;

public sealed record GetDrawResultQuery: IRequest<DrawResultDto?>
{
    public Guid Id { get; init; }
}

public sealed class GetDrawResultQueryHandler(AdessoWorldLeagueDbContext dbContext) : IRequestHandler<GetDrawResultQuery, DrawResultDto?>
{
    public async Task<DrawResultDto?> Handle(GetDrawResultQuery request, CancellationToken cancellationToken)
    {
        var id = new DrawResultId(request.Id);
        var result = await dbContext.DrawRecords.AsNoTracking().Where(m=>m.Id == id).Select(dr => new DrawResultDto
        {
            Id = dr.Id.Value,
            RulerName = dr.Ruler.Name,
            RulerSurname = dr.Ruler.Surname,
            DrawDate = dr.Date.Value,
            Groups = dr.Groups.Select(g => new GroupDto
            {
                Id = g.Id.Value,
                Name = g.Name.Value,
                Teams = g.Teams.Select(t => new TeamDto
                {
                    Id = t.Id.Value,
                    Name = t.Name.Value,
                    CountryName = t.Country.Name.Value
                }).ToList()
            }).ToList()
        }).FirstOrDefaultAsync(cancellationToken);
        return result;
    }
}

