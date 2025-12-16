using System;

using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Services.Singleton.Args;
using FashionFace.Services.Singleton.Implementations;

using Microsoft.AspNetCore.Identity;

/*
var key = "ApiKey";

var nanoBananaApi = new NanoBananaService();

//var taskId = await nanoBananaApi.GenerateImageAsync("Beautiful fury", new GenerateOptions());

//Console.WriteLine(taskId);

var taskStatusArgs =
    new TaskStatusArgs(
        key,
        "eabc4d7090d72086839a216b5a685436"
    );

var taskStatus =
    await
        nanoBananaApi
            .GetTaskStatusAsync(
                taskStatusArgs
            );

var resultImageUrl =
    taskStatus
        .Data
        .Response
        .ResultImageUrl;

Console.WriteLine(
    resultImageUrl
);
*/

var hasher = new PasswordHasher<ApplicationUser>();
var user = new ApplicationUser
{
    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
    UserName = "admin",
    NormalizedUserName = "ADMIN",
    Email = "admin@ff.ai",
    NormalizedEmail = "ADMIN@FF.AI",
    EmailConfirmed = true,
    SecurityStamp = "7c2c1d6e-8b3a-4f5e-9c6a-3f8d2e6b1a74",
    ConcurrencyStamp ="c91e4a52-1b77-4d0a-b8e3-5a2f9d6c4e18",
};

user.PasswordHash = hasher.HashPassword(user, "qwer");

Console.WriteLine(
    user.PasswordHash
);