using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Dtos;
using Volo.Abp.Application.Services;

namespace TodoApp
{
    public interface ITodoAppService : IApplicationService
    {
        Task<List<TodoItemInputDto>> GetListAsync(TodoInputFilter filter);
        Task<TodoItemInputDto> CreateAsync(TodoItemInputDto input);
        Task<TodoItemInputDto> UpdateAsync(TodoItemInputDto input);
        Task DeleteAsync(Guid id);
        Task MarkAsComplete(Guid id);
    }
}