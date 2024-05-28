using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Shared.DTOS.ToDoLists.GetItemsByListId;
using webapi.Infastructure.Data;

namespace webapi.Features.ToDoLists.GetItemsByListId;

internal sealed class Endpoint(ApplicationDbContext context, AutoMapper.IMapper mapper) : Endpoint<GetItemsByListIdRequest, GetItemsByListIdResponse>
{
    public override void Configure()
    {
        Get("/api/todo-list/{id}/todo-item");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetItemsByListIdRequest r, CancellationToken c)
    {
        var toDoList = await context.ToDoLists.Include(x => x.ToDoItems).FirstOrDefaultAsync(x => x.Id == r.Id);
        List<ToDoItemDto> toDoItemDtos = new List<ToDoItemDto>();
        if (toDoList == null)
        {
            await SendNotFoundAsync(c);
            return;
        }
        foreach (var item in toDoList.ToDoItems) 
        {
            var toDoItemDto = mapper.Map<ToDoItemDto>(item);
            toDoItemDtos.Add(toDoItemDto);
        }
        await SendAsync(new GetItemsByListIdResponse(toDoItemDtos), cancellation: c);
    }
}