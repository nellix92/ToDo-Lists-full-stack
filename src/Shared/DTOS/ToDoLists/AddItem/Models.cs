namespace Shared.DTOS.ToDoLists.AddItem
{
    public record AddToDoItemRequest(Guid Id, string Text);
    public record AddToDoItemResponse(Guid Id);

}
