import { ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TodoItemDto, TodoService } from '../proxy';
import { TodoInputFilter, TodoItemInputDto } from '../proxy/dtos';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  todoForm: FormGroup;
  filter: TodoInputFilter = {};
  todoItems: TodoItemInputDto[] = [];
  todoToDelete: TodoItemInputDto | null = null;
  statusOptions = [
    { value: null, label: 'All' },
    { value: 0, label: 'Pending' },
    { value: 1, label: 'In Progress' },
    { value: 2, label: 'Completed' }
  ];

  constructor(
    private todoService: TodoService,
    private toasterService: ToasterService,
    private fb: FormBuilder
  ) {
    this.initForm();
  }

  private initForm(): void {
    this.todoForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      dueDate: [null],
      status: [0],
      priority: [0]
    });
  }

  ngOnInit(): void {
    this.loadTodos(this.filter);
  }

  loadTodos(filter: TodoInputFilter) {
    this.todoService.getList(filter).subscribe(response => {
      this.todoItems = response;
    });
  }

  searchTodos() {
    this.loadData({
      status: this.filter.status ? Number(this.filter.status) : null,
      priority: this.filter.priority ? Number(this.filter.priority) : null,
      startDate: this.filter.startDate || null,
      endDate: this.filter.endDate || null
    });
  }

  loadData(filters: TodoInputFilter): void {
    this.loadTodos(filters);
    let filteredItems = this.todoItems;
    if (filters.status !== null && filters.status !== undefined) {
      filteredItems = filteredItems.filter(item => item.status === filters.status);
    }

    if (filters.priority !== null && filters.priority !== undefined) {
      filteredItems = filteredItems.filter(item => item.priority === filters.priority);
    }

    if (filters.startDate) {
      filteredItems = filteredItems.filter(item =>
        new Date(item.dueDate) >= new Date(filters.startDate));
    }

    if (filters.endDate) {
      filteredItems = filteredItems.filter(item =>
        new Date(item.dueDate) <= new Date(filters.endDate));
    }

    this.todoItems = filteredItems;
  }

  create(): void {
    if (this.todoForm.valid) {
      const todoItemInput: TodoItemInputDto = {
        ...this.todoForm.value
      };

      this.todoService.create(todoItemInput).subscribe({
        next: (result) => {
          this.loadTodos(this.filter);
          this.todoForm.reset();
          this.toasterService.success('Todo item created successfully!');
        }
      });
    }
  }

  setTodoToDelete(todo: TodoItemInputDto): void {
    this.todoToDelete = todo;
  }

  confirmDelete(): void {
    if (this.todoToDelete) {
      this.todoService.delete(this.todoToDelete.id).subscribe(() => {
        this.todoItems = this.todoItems.filter(item => item.id !== this.todoToDelete.id);
        this.toasterService.success('Todo item deleted successfully!', 'Success');
        this.todoToDelete = null;
      });
    }
  }

  delete(id: string): void {
    this.todoService.delete(id).subscribe(() => {
      this.todoItems = this.todoItems.filter(item => item.id !== id);
      this.toasterService.success('Todo item deleted successfully!', 'Success');
    });
  }

  markAsComplete(todo: TodoItemInputDto): void {
    this.todoService.markAsCompleteById(todo.id).subscribe((result) => {
      this.loadTodos(this.filter);
      this.toasterService.success('Todo marked as complete!', 'Success');
    });
  }
}

