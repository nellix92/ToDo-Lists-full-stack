using AutoMapper;
using Shared.DTOS.ToDoItems.Update;
using Shared.DTOS.ToDoLists.GetItemsByListId;
using System.Text.Json.Serialization;
using webapi.Domains.Entities.Common;

namespace webapi.Domains.Entities;

public class ToDoItem : AuditableBaseEntity<Guid>
{
    public string Text { get; set; }
    public Guid ToDoListId { get; set; }
    [JsonIgnore]
    public ToDoList ToDoList { get; set; }
    public bool IsDone { get; set; } = false;

    protected ToDoItem()
    {

    }
    [JsonConstructor]
    protected ToDoItem(Guid id, string text, Guid toDoListId) : base(id)
    {
        Text = text;
        ToDoListId = toDoListId;
    }

    private ToDoItem(string text, Guid toDoListId) : base()
    {
        Text = text;
        ToDoListId = toDoListId;
    }

    public static ToDoItem Create(string text, Guid toDoListId)
    {
        return new ToDoItem(Guid.NewGuid(), text, toDoListId);
    }

    public static ToDoItem CreateNoGuid(string text, Guid toDoListId)
    {
        return new ToDoItem(text, toDoListId);
    }

    public static ToDoItem Update(ToDoItem toDoItem, UpdateToDoItemRequest r)
    {
        toDoItem.IsDone = r.IsDone;
        return toDoItem;
    }
}

public class ToDoItemDtoProfile : Profile
{
    public ToDoItemDtoProfile()
    {
        CreateMap<ToDoItem, ToDoItemDto > ();
    }
}

