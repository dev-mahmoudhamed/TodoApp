using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace TodoApp.Todo
{
    public class TodoAppService : ApplicationService, ITodoAppService
    {
        private readonly IRepository<TodoItem, Guid> _todoItemRepository;

        public TodoAppService(IRepository<TodoItem, Guid> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task<List<TodoItemInputDto>> GetListAsync(TodoInputFilter filter)
        {
            var items = (await _todoItemRepository.GetListAsync())
                .WhereIf(filter.Status != null, td => (int)td.Status == filter.Status)
                .WhereIf(filter.Priority != null, td => (int)td.Priority == filter.Priority)
                .WhereIf(filter.StartDate != null, td => td.DueDate >= filter.StartDate)
                .WhereIf(filter.EndDate != null, td => td.DueDate <= filter.EndDate);

            return items
                .Select(item => new TodoItemInputDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Status = (int)item.Status,
                    DueDate = item.DueDate,
                    Priority = (int)item.Priority
                }).ToList();
        }

        public async Task<TodoItemInputDto> CreateAsync(TodoItemInputDto input)
        {
            try
            {
                if (input.DueDate is not null && input.DueDate < DateTime.Today)
                {
                    throw new UserFriendlyException("The date must be today or a future date");
                }

                var todoItem = await _todoItemRepository.InsertAsync(
                    new TodoItem
                    {
                        Title = input.Title,
                        Description = input.Description,
                        Status = (TodoStatus)input.Status,
                        DueDate = input.DueDate,
                        Priority = (TodoPriority)input.Priority
                    });

                var dto = ObjectMapper.Map<TodoItem, TodoItemInputDto>(todoItem);
                return dto;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }

        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _todoItemRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        public async Task<TodoItemInputDto> UpdateAsync(TodoItemInputDto input)
        {
            try
            {
                var todoItem = await _todoItemRepository.FindAsync(input.Id);
                todoItem.Update(input.Title, input.Description, input.Status, input.Priority, input.DueDate);

                return new TodoItemInputDto
                {
                    Id = todoItem.Id,
                    Title = todoItem.Title,
                    Description = todoItem.Description,
                    DueDate = input.DueDate,
                    Status = input.Status,
                    Priority = input.Priority
                };
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        public async Task MarkAsComplete(Guid id)
        {
            var todoItem = await _todoItemRepository.FindAsync(id);
            if (todoItem is null)
            {
                throw new Exception("Todo item not found");
            }
            todoItem.MarkAsComplete();
        }
    }
}
