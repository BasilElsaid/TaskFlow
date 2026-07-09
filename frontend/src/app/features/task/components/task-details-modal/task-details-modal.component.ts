import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Input, Output } from "@angular/core";
import { Task } from "../../models/task";
import { TaskStatus } from "../../enums/task-status-enum";
import { TaskPriority } from "../../enums/task-priority-enum";

@Component({
  selector: "app-task-details-modal",
  standalone: true,
  imports: [CommonModule],
  templateUrl: "./task-details-modal.component.html",
})
export class TaskDetailsModalComponent {
  @Input() task?: Task;
  @Output() close = new EventEmitter<void>();

  taskPriority = TaskPriority;
  taskStatus = TaskStatus;
}
