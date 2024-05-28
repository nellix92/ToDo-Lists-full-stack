namespace Shared.DTOS.ToDoItems.Update;

public record UpdateToDoItemRequest(Guid id, bool IsDone);
