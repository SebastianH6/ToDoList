using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

    public async Task<List<TodoItem>> GetTodosAsync(int userId, string sortOption = "creationTime", string queryString = "")
    {
        var query = _dbContext.ToDoItems.Where(t => t.UserId == userId);

        if(!string.IsNullOrEmpty(queryString))
        {
            query = query.Where(t => t.Title.ToLower().Contains(queryString) || t.Description.ToLower().Contains(queryString));
        }

        query = sortOption switch
        {
            "dueDate" => query.OrderBy(t => t.DueDate),
            "priority" => query.OrderByDescending(t => t.Priority),
            _ => query.OrderBy(t => t.CreationDate)
        };

        return await query.ToListAsync();

        // if(sortOption == "creationTime")
        // {
        //     return await _dbContext.ToDoItems.Where(t => t.UserId == userId).OrderBy(t => t.CreationDate).ToListAsync();
        // }
        // else if(sortOption == "dueDate")
        // {
        //     return await _dbContext.ToDoItems.Where(t => t.UserId == userId).OrderBy(t => t.DueDate).ToListAsync();
        // }
        // else if(sortOption == "priority")
        // {
        //     return await _dbContext.ToDoItems.Where(t => t.UserId == userId).OrderByDescending(t => t.Priority).ToListAsync();
        // }
        // else
        // {
        //     return await _dbContext.ToDoItems.Where(t => t.UserId == userId).OrderBy(t => t.CreationDate).ToListAsync();
        // }
    }

    public async Task AddTodoAsync(TodoItem todo)
    {
        _dbContext.ToDoItems.Add(todo);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTodoAsync(TodoItem todo)
    {
        var existingTodo = await _dbContext.ToDoItems.FindAsync(todo.Id);
        if(existingTodo == null)
        {
            _dbContext.ToDoItems.Add(todo);
        }
        else
        {
            _dbContext.ToDoItems.Update(todo);
        }
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(TodoItem todo)
    {
        _dbContext.ToDoItems.Remove(todo);
        await _dbContext.SaveChangesAsync();
    }
}
