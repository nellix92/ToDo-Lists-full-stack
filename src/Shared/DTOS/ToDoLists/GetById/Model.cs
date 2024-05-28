namespace Shared.DTOS.ToDoLists.GetById;
   public record GetToDoListByIdRequest(Guid Id);
public record GetToDoListByIdResponse
{
    public string ListTitle { get; set; }

    public GetToDoListByIdResponse()
    {

    }
    public GetToDoListByIdResponse(string ListTitle)
    {
       this.ListTitle = ListTitle;
    }
}

