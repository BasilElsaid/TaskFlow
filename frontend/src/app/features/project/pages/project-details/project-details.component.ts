import { Component, inject } from "@angular/core";
import { ActivatedRoute, RouterModule } from "@angular/router";
import { Project } from "../../models/project";
import { ProjectService } from "../../services/project.service";
import { CommonModule } from "@angular/common";
import { TaskService } from "../../../task/services/task.service";

@Component({
  selector: "app-project-details",
  imports: [RouterModule, CommonModule],
  templateUrl: "./project-details.component.html",
  styleUrl: "./project-details.component.css",
})
export class ProjectDetailsComponent {
  private route = inject(ActivatedRoute);

  private projectService = inject(ProjectService);

  private taskService = inject(TaskService);

  project?: Project;

  tasks: any[] = [];

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get("id"));

    this.projectService

      .getProject(id)

      .subscribe((project) => {
        this.project = project;
      });

    this.taskService

      .getTasks(id)

      .subscribe((tasks) => {
        this.tasks = tasks;
      });
  }
}
