import { TaskPriority } from "../../../features/task/enums/task-priority-enum";
import { TaskStatus } from "../../../features/task/enums/task-status-enum";

export interface UpdateTaskRequest {
  title: string;
  description: string;
  taskStatus: TaskStatus;
  taskPriority: TaskPriority;
  dueDate: string | null;
  assignedUserId: string | null;
}
