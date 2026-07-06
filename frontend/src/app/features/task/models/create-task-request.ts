import { TaskPriority } from "../../../features/task/enums/task-priority-enum";

export interface CreateTaskRequest {
  title: string;
  description: string;
  taskPriority: TaskPriority;
  dueDate: string | null;
  assignedUserId: string | null;
}
