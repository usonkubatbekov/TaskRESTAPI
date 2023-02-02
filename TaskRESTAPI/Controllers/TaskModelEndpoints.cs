using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using TaskRESTAPI.Data;
using TaskRESTAPI.Models;
namespace TaskRESTAPI;

public static class TaskModelEndpoints
{
    public static void MapTaskModelEndpoints (this IEndpointRouteBuilder routes)
    {

        routes.MapGet("/api/Все задачи", async (TaskRESTAPIContext db) =>
        {
            return await db.TaskModel.ToListAsync();
        })
        .WithName("GetAllTaskModels");

        routes.MapGet("/api/Получение задачи по/{Id:int}", async (int Id, TaskRESTAPIContext db) =>
        {
            return await db.TaskModel.FindAsync(Id)
                is TaskModel model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetTaskModelById");

        routes.MapPut("/api/Измение задачи по/{Id:int}", async (int Id, TaskModel taskModel, TaskRESTAPIContext db) =>
        {
            var foundModel = await db.TaskModel.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(taskModel);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateTaskModel");

        routes.MapPost("/api/Добавление задачи/", async (TaskModel taskModel, TaskRESTAPIContext db) =>
        {
            db.TaskModel.Add(taskModel);
            await db.SaveChangesAsync();
            return Results.Created($"/TaskModels/{taskModel.Id}", taskModel);

        })
        .WithName("CreateTaskModel");

        routes.MapPost("/api/Загрузка файла/", (HttpRequest request) =>
        {
            if (!request.Form.Files.Any())
                return Results.BadRequest("At least one file is needed");
            string path = Directory.GetCurrentDirectory();
            string subputh = @"\wwwroot\";
            var filename = string.Concat(path, subputh);
            Console.WriteLine(filename);
            foreach (var file in request.Form.Files)
            {
                using (var stream = new FileStream(filename + file.FileName, FileMode.Create))
                {
                    file.CopyTo(stream);
                    path = filename +file.FileName;
                }
            }
            return Results.Ok(path);
        })
    .Accepts<List<IFormFile>>("multipart/form-data");


        routes.MapDelete("/api/Удаление задачи/{Id:int}", async (int Id, TaskRESTAPIContext db) =>
        {
            if (await db.TaskModel.FindAsync(Id) is TaskModel taskModel)
            {
                db.TaskModel.Remove(taskModel);
                await db.SaveChangesAsync();
                return Results.Ok(taskModel);
            }

            return Results.NotFound();
        })
        .WithName("DeleteTaskModel");
    }
}
