
export interface TodoInputFilter {
  status?: number;
  priority?: number;
  startDate?: string;
  endDate?: string;
}

export interface TodoItemInputDto {
  id?: string;
  title: string;
  description?: string;
  status: number;
  priority: number;
  dueDate?: string;
}
