using System.Collections.Generic;

namespace FashionFace.Services.Singleton.Models;

public sealed record ErrorModel(
    string Code,
    IDictionary<string, object> Data
);