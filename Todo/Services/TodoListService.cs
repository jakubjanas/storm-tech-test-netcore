using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Interfaces;

namespace Todo.Services
{
    //Added TodoListService as an example of good practises
    public class TodoListService : ITodoListService
    {
        private readonly ApplicationDbContext dbContext;

        public TodoListService(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<TodoList> GetRelevantTodoLists(string userId)
        {
            var relevantTodoLists = dbContext.TodoLists
                .Include(x => x.Owner)
                .Include(x => x.Items)
                .Where(x => x.Owner.Id == userId || x.Items.Any(item => item.ResponsiblePartyId == userId))
                .ToList();

            return relevantTodoLists;
        }
    }
}
