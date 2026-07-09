import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Input, Output } from "@angular/core";
import { RouterModule } from "@angular/router";
import { Task } from "../../models/task";

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
}
