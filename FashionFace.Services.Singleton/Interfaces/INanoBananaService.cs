using System.Threading.Tasks;

using FashionFace.Services.Singleton.Args;
using FashionFace.Services.Singleton.Models;

namespace FashionFace.Services.Singleton.Interfaces;

public interface INanoBananaService
{
    Task<string> GenerateImageAsync(
        GenerateImageArgs args
    );

    Task<TaskStatusResponse?> GetTaskStatusAsync(
        TaskStatusArgs args
    );
}