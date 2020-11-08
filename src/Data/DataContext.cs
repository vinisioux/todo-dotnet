using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskTodo> TaskTodos { get; set; }

  }
}
