import { TaskPriority } from "../../enums/task-priority-enum";
import { TaskStatus } from "../../enums/task-status-enum";

export interface TaskFilterRequest {
  taskStatus?: TaskStatus;
  taskPriority?: TaskPriority;
  assignedToMe?: boolean;
  dueBefore?: string | null;
  search?: string;
}
