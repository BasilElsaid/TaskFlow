import { Component, inject, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ActivatedRoute, RouterModule } from "@angular/router";
import { TaskService } from "../../../core/services/task.service";


@Component({
  selector: "app-project-tasks",
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: "./project-tasks-page.component.html",
})
export class ProjectTasksPageComponent implements OnInit {
  private taskService = inject(TaskService);
  private route = inject(ActivatedRoute);

  projectId!: number;
  tasks: any[] = [];

  ngOnInit() {
    this.projectId = Number(this.route.snapshot.paramMap.get("id"));
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getTasks(this.projectId).subscribe((res) => {
      this.tasks = res;
    });
  }
}