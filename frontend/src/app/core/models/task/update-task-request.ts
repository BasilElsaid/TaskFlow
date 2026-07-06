import { TaskPriority } from "../../enums/task-priority-enum";
import { TaskStatus } from "../../enums/task-status-enum";

export interface UpdateTaskRequest {
  title: string;
  description: string;
  taskStatus: TaskStatus;
  taskPriority: TaskPriority;
  dueDate: string | null;
  assignedUserId: string | null;
}
