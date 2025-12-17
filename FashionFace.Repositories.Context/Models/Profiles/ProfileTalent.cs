using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Talents;

namespace FashionFace.Repositories.Context.Models.Profiles;

public sealed class ProfileTalent : EntityBase,
    IWithPositionIndex
{
    public required Guid ProfileId { get; set; }
    public required Guid TalentId { get; set; }

    public Profile? Profile { get; set; }
    public Talent? Talent { get; set; }

    public required double PositionIndex { get; set; }
}