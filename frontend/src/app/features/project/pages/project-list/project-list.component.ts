import { CommonModule } from "@angular/common";
import { Component, inject } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ProjectService } from "../../services/project.service";
import { Project } from "../../models/project";
import { ProjectCardComponent } from "../../components/project-card/project-card.component";

@Component({
  selector: "app-project-list",
  imports: [CommonModule, RouterModule, ProjectCardComponent],
  templateUrl: "./project-list.component.html",
  styleUrl: "./project-list.component.css",
})
export class ProjectListComponent {
  private projectService = inject(ProjectService);

  projects: any[] = [];

  ngOnInit() {
    this.loadProjects();
  }

  loadProjects() {
    this.projectService.getProjects().subscribe((res) => {
      this.projects = res;
    });
  }

  openEditModal(project: Project) {
    console.log("modifica", project);
  }

  deleteProject(id: number) {
    if (!confirm("Sei sicuro?")) return;

    this.projectService.deleteProject(id).subscribe(() => {
      this.projects = this.projects.filter((p) => p.id !== id);
    });
  }
}
