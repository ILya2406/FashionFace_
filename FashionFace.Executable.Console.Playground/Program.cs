using System;

using FashionFace.Services.Singleton.Args;
using FashionFace.Services.Singleton.Implementations;

Console.WriteLine(
    "Hello, World!"
);

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