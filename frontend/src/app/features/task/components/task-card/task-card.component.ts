import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Input, Output } from "@angular/core";
import { RouterModule } from "@angular/router";
import { Task } from "../../models/task";
import { TaskPriority } from "../../enums/task-priority-enum";
import { TaskStatus } from "../../enums/task-status-enum";

@Component({
  selector: "app-task-card",
  imports: [CommonModule, RouterModule],
  templateUrl: "./task-card.component.html",
  styleUrl: "./task-card.component.css",
})
export class TaskCardComponent {
  @Input() task!: Task;
  @Output() details = new EventEmitter<Task>();
  @Output() edit = new EventEmitter<Task>();
  @Output() delete = new EventEmitter<number>();

  TaskStatus = TaskStatus;
  TaskPriority = TaskPriority;

  getStatusLabel(): string {
    switch (this.task.taskStatus) {
      case TaskStatus.Todo:
        return "Da fare";

      case TaskStatus.InProgress:
        return "In corso";

      case TaskStatus.Done:
        return "Completato";

      default:
        return "";
    }
  }

  getPriorityLabel(): string {
    switch (this.task.taskPriority) {
      case TaskPriority.Low:
        return "Bassa";

      case TaskPriority.Normal:
        return "Normale";

      case TaskPriority.High:
        return "Alta";

      default:
        return "";
    }
  }
}
