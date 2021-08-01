using System.Collections.Generic;
using Todo.Data.Entities;

namespace Todo.Interfaces
{
    public interface ITodoListService
    {
        IEnumerable<TodoList> GetRelevantTodoLists(string userId);
    }
}
