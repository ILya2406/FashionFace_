using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterCriteria : EntityBase
{
    public TalentType? TalentType { get; set; }
    public FilterCriteriaLocation? FilterCriteriaLocation { get; set; }
    public FilterCriteriaAppearanceTraits? FilterCriteriaAppearanceTraits { get; set; }
    public ICollection<FilterCriteriaTag> FilterCriteriaTagCollection { get; set; }
}