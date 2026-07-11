import { TaskPriority } from "../../../features/task/enums/task-priority-enum";
import { TaskStatus } from "../../../features/task/enums/task-status-enum";

export interface TaskFilterRequest {
  taskStatus?: TaskStatus | null;
  taskPriority?: TaskPriority | null;
  assignedToMe?: boolean | null;
  dueBefore?: string | null;
  search?: string;
}
