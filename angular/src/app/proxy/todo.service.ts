import type { TodoInputFilter, TodoItemInputDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  apiName = 'Default';
  

  create = (input: TodoItemInputDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TodoItemInputDto>({
      method: 'POST',
      url: '/api/app/todo',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/todo/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (filter: TodoInputFilter, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TodoItemInputDto[]>({
      method: 'GET',
      url: '/api/app/todo',
      params: { status: filter.status, priority: filter.priority, startDate: filter.startDate, endDate: filter.endDate },
    },
    { apiName: this.apiName,...config });
  

  markAsCompleteById = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/todo/${id}/mark-as-complete`,
    },
    { apiName: this.apiName,...config });
  

  update = (input: TodoItemInputDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TodoItemInputDto>({
      method: 'PUT',
      url: '/api/app/todo',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
