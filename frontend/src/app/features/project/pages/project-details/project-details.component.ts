import { Component, inject } from "@angular/core";
import { ActivatedRoute, RouterModule } from "@angular/router";
import { Project } from "../../models/project";
import { ProjectService } from "../../services/project.service";
import { CommonModule } from "@angular/common";
import { TaskService } from "../../../task/services/task.service";
import { TaskFormModalComponent } from "../../../task/components/task-form-modal/task-form-modal.component";
import { TaskCardComponent } from "../../../task/components/task-card/task-card.component";
import { Task } from "../../../task/models/task";
import { TaskDetailsModalComponent } from "../../../task/components/task-details-modal/task-details-modal.component";

@Component({
  selector: "app-project-details",
  imports: [
    RouterModule,
    CommonModule,
    TaskFormModalComponent,
    TaskCardComponent,
    TaskDetailsModalComponent
  ],
  templateUrl: "./project-details.component.html",
  styleUrl: "./project-details.component.css",
})
export class ProjectDetailsComponent {
  private route = inject(ActivatedRoute);

  private projectService = inject(ProjectService);

  private taskService = inject(TaskService);

  project?: Project;

  tasks: Task[] = [];

  showTaskModal = false;

  selectedTask?: Task;

  showTaskDetails = false;

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get("id"));

    this.loadProject(id);

    this.loadTasks(id);
  }

  loadProject(id: number) {
    this.projectService.getProject(id).subscribe((project) => {
      this.project = project;
    });
  }

  loadTasks(id?: number) {
    const projectId = id ?? this.project!.id;

    this.taskService.getTasks(projectId).subscribe((tasks) => {
      this.tasks = tasks;
    });
  }

  openCreateModal() {
    this.selectedTask = undefined;

    this.showTaskModal = true;
  }

  openEditModal(task: Task) {
    this.selectedTask = task;

    this.showTaskModal = true;
  }

  deleteTask(id: number) {
    if (!confirm("Sei sicuro di eliminare il task?")) {
      return;
    }

    this.taskService.deleteTask(id).subscribe(() => {
      this.tasks = this.tasks.filter((t) => t.id !== id);
    });
  }

  closeTaskModal() {
    this.selectedTask = undefined;

    this.showTaskModal = false;
  }

  openTaskDetails(task: Task) {
    this.selectedTask = task;
    this.showTaskDetails = true;
  }

  closeTaskDetails() {
    this.selectedTask = undefined;
    this.showTaskDetails = false;
  }
}
