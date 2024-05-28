using FastEndpoints;
using Shared.DTOS.ToDoLists.GetById;
using webapi.Infastructure.Data;

namespace webapi.Features.ToDoLists.GetById;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetToDoListByIdRequest, GetToDoListByIdResponse>
{
    public override void Configure()
    {
        Get("/api/todo-list/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetToDoListByIdRequest req, CancellationToken ct)
    {
        var toDoList = await context.ToDoLists.FindAsync(req.Id);
        if (toDoList == null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            await SendAsync(new GetToDoListByIdResponse(toDoList.Title));
        }
    }
}
