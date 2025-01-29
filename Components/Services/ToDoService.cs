using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Services;

public class ToDoService
{
    private readonly AppDbContext _dbContext;

    public ToDoService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TodoItem>> GetTodosAsync(int userId)
    {
        return await _dbContext.ToDoItems.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task AddTodoAsync(TodoItem todo)
    {
        _dbContext.ToDoItems.Add(todo);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTodoAsync(TodoItem todo)
    {
        _dbContext.ToDoItems.Update(todo);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(TodoItem todo)
    {
        _dbContext.ToDoItems.Remove(todo);
        await _dbContext.SaveChangesAsync();
    }
}
