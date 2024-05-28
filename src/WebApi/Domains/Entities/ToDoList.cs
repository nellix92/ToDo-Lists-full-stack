using AutoMapper;
using Shared.DTOS.ToDoLists.GetAll;
using System.Text.Json.Serialization;
using webapi.Domains.Entities.Common;
using webapi.Services;

namespace webapi.Domains.Entities;

public class ToDoList : AuditableBaseEntity<Guid>, IProgressive
{ 
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public List<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    public int Progressive { get; set; }

    protected ToDoList()
    {

    }

    [JsonConstructor]
    protected ToDoList(Guid id, string title) : base(id)
    {
        Title = title;
    }

    public static ToDoList Create(string title)
    {
        return new ToDoList(Guid.NewGuid(), title);
    }

    public ToDoItem AddToDoItem(string text)
    {
        var toDoItem = ToDoItem.CreateNoGuid(text, this.Id);
        toDoItem.CreatedBy = "Alessio";
        toDoItem.Created = DateTime.UtcNow;
        ToDoItems.Add(toDoItem);
        return toDoItem;
    }

    public void CheckDone()
    {
        foreach(var item in ToDoItems)
        {
            if (!item.IsDone) 
            {
                IsDone = false;
                return;
            }
        }
        IsDone = true;
    }
}

public class GetAllProfile : Profile
{
    public GetAllProfile()
    {
        CreateMap<ToDoList, ToDoListDto>();
    }
}


