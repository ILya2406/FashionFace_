using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Filters;
using FashionFace.Repositories.Context.Models.Locations;
using FashionFace.Repositories.Context.Models.Portfolios;
using FashionFace.Repositories.Context.Models.Profiles;

namespace FashionFace.Repositories.Context.Models.Talents;

public sealed class Talent : EntityBase,
    IWithIsDeleted
{
    public required bool IsDeleted { get; set; }
    public required string Description { get; set; }
    public required TalentType TalentType { get; set; }

    public Portfolio? Portfolio { get; set; }
    public ProfileTalent? ProfileTalent { get; set; }
    public TalentMediaAggregate? TalentMediaAggregate { get; set; }
    public ICollection<Location> LocationCollection { get; set; }
    public ICollection<FilterResultTalent> FilterResultTalentCollection { get; set; }
}