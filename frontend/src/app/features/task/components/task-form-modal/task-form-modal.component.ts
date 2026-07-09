import { CommonModule } from "@angular/common";
import { Component, EventEmitter, inject, Input, Output } from "@angular/core";
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms";
import { TaskService } from "../../services/task.service";
import { Task } from "../../models/task";
import { TaskPriority } from "../../enums/task-priority-enum";
import { UpdateTaskRequest } from "../../models/update-task-request";
import { CreateTaskRequest } from "../../models/create-task-request";

@Component({
  selector: "app-task-form-modal",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: "./task-form-modal.component.html",
  styleUrl: "./task-form-modal.component.css",
})
export class TaskFormModalComponent {
  private fb = inject(FormBuilder);
  private taskService = inject(TaskService);

  @Input() task?: Task;
  @Input() projectId!: number;

  @Input() title = "";
  @Input() subtitle = "";

  @Output() close = new EventEmitter<void>();
  @Output() saved = new EventEmitter<void>();

  // espongo l'enum al template
  TaskPriority = TaskPriority;

  taskForm = this.fb.nonNullable.group({
    title: ["", Validators.required],
    description: [""],
    taskPriority: [TaskPriority.Normal],
    dueDate: [""],
    assignedUserId: [null as string | null],
  });

  loading = false;
  errorMessage = "";

  ngOnInit() {
    if (this.task) {
      this.taskForm.patchValue({
        title: this.task.title,
        description: this.task.description,
        taskPriority: this.task.taskPriority,
        dueDate: this.task.dueDate ?? "",
        assignedUserId: this.task.assignedUserId ?? "",
      });
    }
  }

  saveTask() {
    if (this.taskForm.invalid) {
      this.taskForm.markAllAsTouched();
      return;
    }

    this.loading = true;

    const formValue = this.taskForm.getRawValue();

    const request: CreateTaskRequest = {
      title: formValue.title,
      description: formValue.description,
      taskPriority: formValue.taskPriority,
      dueDate: formValue.dueDate || null,
      assignedUserId: formValue.assignedUserId || null,
    };

    const operation = this.task
      ? this.taskService.updateTask(this.task.id, request as UpdateTaskRequest)
      : this.taskService.createTask(this.projectId, request);

    operation.subscribe({
      next: () => {
        this.loading = false;
        this.saved.emit();
        this.close.emit();
      },

      error: () => {
        this.loading = false;
        this.errorMessage = "Errore durante il salvataggio del task";
      },
    });
  }

  closeModal() {
    this.close.emit();
  }
}
