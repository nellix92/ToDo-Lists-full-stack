namespace Shared.DTOS.ToDoLists.GetItemsByListId;

public record GetItemsByListIdRequest(Guid Id);
public record GetItemsByListIdResponse(List<ToDoItemDto> ToDoItems);

public record ToDoItemDto(Guid Id, string Text, bool IsDone, string CreatedBy, DateTimeOffset Created);
