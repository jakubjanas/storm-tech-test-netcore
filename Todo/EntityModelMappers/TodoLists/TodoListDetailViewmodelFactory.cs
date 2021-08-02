using System.Linq;
using Todo.Common;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, SortOrder sortOrder)
        {
            var items = todoList.Items.Select(TodoItemSummaryViewmodelFactory.Create);
            items = sortOrder == SortOrder.Importance
                ? items.OrderBy(x => x.Importance)
                : items.OrderBy(x => x.Rank);

            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items.ToList());
        }
    }
}