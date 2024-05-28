namespace Shared.DTOS.ToDoLists.GetAll;
public record GetAllToDoListResponse
{
    public List<ToDoListDto> ToDoLists { get; set; }
    public GetAllToDoListResponse()
    {

    }
}   

public record ToDoListDto(Guid Id, string Title, bool IsDone, DateTimeOffset Created, string CreatedBy);




