using System;

namespace FashionFace.Executable.Worker.UserEvents.Args;

public sealed record EventHandlerBuilderArgs(
    IServiceProvider ServiceProvider
);