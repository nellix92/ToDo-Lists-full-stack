using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Shared.DTOS.ToDoItems.DeleteById;
using webapi.Infastructure.Data;

namespace webapi.Features.ToDoItems.DeleteById
{
    internal sealed class Endpoint(ApplicationDbContext context) : Endpoint<DeleteItemByIdRequest, EmptyResponse>
    {
        public override void Configure()
        {
            Delete("/api/todo-item/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteItemByIdRequest r, CancellationToken c)
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
            toDoItem.ToDoList.ToDoItems.Remove(toDoItem);
            context.ToDoItems.Remove(toDoItem);
            toDoList.CheckDone();
            await context.SaveChangesAsync();
            await SendNoContentAsync();
        }
    }
}