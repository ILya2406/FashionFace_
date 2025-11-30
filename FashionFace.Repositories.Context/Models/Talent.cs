using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Repositories.Context.Models;

public sealed class Talent : EntityBase
{
    public required Guid ProfileId { get; set; }

    public required TalentType Type { get; set; }

    public Pseudonym? Pseudonym { get; set; }
    public Portfolio? Portfolio { get; set; }

    public Profile? Profile { get; set; }

    public ICollection<TalentLocation> TalentLocationCollection { get; set; }
}