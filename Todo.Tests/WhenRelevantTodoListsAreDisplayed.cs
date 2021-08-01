using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Interfaces;
using Todo.Services;
using Xunit;

namespace Todo.Tests
{
    public class WhenRelevantTodoListsAreDisplayed
    {
        private readonly DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDb")
                .Options;
        private readonly ITodoListService sut;

        public WhenRelevantTodoListsAreDisplayed()
        {
            sut = new TodoListService(new ApplicationDbContext(options));
        }

        private void DbSeed()
        {
            using var context = new ApplicationDbContext(options);
            var todoLists = new List<TodoList>()
            {
                new TestTodoListBuilder(new IdentityUser("first@example.com"), "first list").WithItem("test", Importance.High).Build(),
                new TestTodoListBuilder(new IdentityUser("second@example.com"), "second list").WithItem("test 2", Importance.Low).Build()
            };

            context.AddRange(todoLists);
            context.SaveChanges();
        }

        [Fact]
        public void ShouldNotReturnsAnyTodoListWhenUserIsNotOwningAny()
        {
            using var context = new ApplicationDbContext(options);
            DbSeed();

            context.Users.Add(new IdentityUser("empty@example.com"));
            context.SaveChanges();

            var ownerId = context.Users.First(x => x.UserName == "empty@example.com").Id;

            var result = sut.GetRelevantTodoLists(ownerId);

            Assert.Empty(result);
        }

        [Fact]
        public void ShouldReturnsAllRelevantTodoLists()
        {
            using var context = new ApplicationDbContext(options);
            DbSeed();

            var ownerId = context.Users.First(x => x.UserName == "first@example.com").Id;

            //Update ResponisbleParyId
            var toDoItemToUpdate = context.TodoItems.First(x => x.ResponsiblePartyId != ownerId);
            toDoItemToUpdate.ResponsiblePartyId = ownerId;

            context.SaveChanges();


            var result = sut.GetRelevantTodoLists(ownerId);

            Assert.Equal(2, result.Count());
        }
    }
}
