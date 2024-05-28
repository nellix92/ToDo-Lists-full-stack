using FastEndpoints;
using Shared.DTOS.ToDoLists.DeleteById;
using webapi.Infastructure.Data;

namespace webapi.Features.ToDoLists
{
    internal sealed class Endpoint(ApplicationDbContext context) : Endpoint<DeleteToDoListByIdRequest, EmptyResponse>
    {
        public override void Configure()
        {
            Delete("api/todo-list/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteToDoListByIdRequest r, CancellationToken c)
        {
            var toDoList = await context.ToDoLists.FindAsync(r.Id);
            if (toDoList == null) 
            {
                await SendNotFoundAsync(c);
            }
            else
            {
                context.ToDoLists.Remove(toDoList);
                await context.SaveChangesAsync();
                await SendNoContentAsync(c);
            }
        }
    }
}