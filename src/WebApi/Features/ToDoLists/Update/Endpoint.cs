using FastEndpoints;
using Shared.DTOS.ToDoLists.Update;
using webapi.Infastructure.Data;

namespace webapi.Features.ToDoLists.Update
{
    internal sealed class Endpoint(ApplicationDbContext context) : Endpoint<UpdateToDoListRequest, EmptyResponse>
    {
        public override void Configure()
        {
            Put("/api/todo-list/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateToDoListRequest r, CancellationToken c)
        {
            var toDoList = await context.ToDoLists.FindAsync(r.id);
            if (toDoList is null)
            {
                await SendNotFoundAsync();
                return;
            }
            if (!string.IsNullOrWhiteSpace(r.Title)) 
            {
                toDoList.Title = r.Title;
            }
            await context.SaveChangesAsync();
            await SendNoContentAsync();
        }
    }
}