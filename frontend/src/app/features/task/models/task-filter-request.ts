import { TaskPriority } from "../../../features/task/enums/task-priority-enum";
import { TaskStatus } from "../../../features/task/enums/task-status-enum";

export interface TaskFilterRequest {
  taskStatus?: TaskStatus;
  taskPriority?: TaskPriority;
  assignedToMe?: boolean;
  dueBefore?: string | null;
  search?: string;
}
