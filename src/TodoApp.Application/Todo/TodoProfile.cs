using AutoMapper;
using TodoApp.Dtos;

namespace TodoApp.Todo
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoItemInputDto, TodoItem>().ReverseMap();
        }
    }

}
