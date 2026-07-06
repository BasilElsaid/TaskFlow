import { TaskPriority } from "../../../features/task/enums/task-priority-enum";
import { TaskStatus } from "../../../features/task/enums/task-status-enum";

export interface Task {
  id: number;
  title: string;
  description: string;

  taskStatus: TaskStatus;
  taskPriority: TaskPriority;

  dueDate: string | null;
  createdAt: string | null;

  projectId: number;
  assignedUserId: string | null;
}
