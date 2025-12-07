using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class Talent : EntityBase
{
    public required Guid ProfileId { get; set; }

    public required string Description { get; set; }
    public required TalentType Type { get; set; }

    public Portfolio? Portfolio { get; set; }
    public ICollection<TalentLocation> TalentLocationCollection { get; set; }

    public Profile? Profile { get; set; }
}