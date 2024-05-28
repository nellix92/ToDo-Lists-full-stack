using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Shared.DTOS.ToDoItems.Update;
using webapi.Domains.Entities;
using webapi.Infastructure.Data;

namespace webapi.Features.ToDoItems.Update
{
    internal sealed class Endpoint(ApplicationDbContext context) : Endpoint<UpdateToDoItemRequest, EmptyResponse>
    {
        public override void Configure()
        {
            Put("/api/todo-item/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateToDoItemRequest r, CancellationToken c)
        {
            var toDoItem = await context.ToDoItems.FirstOrDefaultAsync(x => x.Id == r.id);
            if (toDoItem is null)
            {
                await SendNotFoundAsync();
                return;
            }
            var toDoList = context.ToDoLists.Include(x => x.ToDoItems).FirstOrDefault(x => x.Id == toDoItem.ToDoListId);
            if (toDoList is null)
            {
                await SendNotFoundAsync();
                return;
            }
            toDoItem = ToDoItem.Update(toDoItem, r);
            toDoList.CheckDone();
            await context.SaveChangesAsync();
            await SendNoContentAsync();
        }
    }
}