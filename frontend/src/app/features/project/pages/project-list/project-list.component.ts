import { CommonModule } from "@angular/common";
import { Component, inject } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ProjectService } from "../../services/project.service";
import { Project } from "../../models/project";
import { ProjectCardComponent } from "../../components/project-card/project-card.component";
import { ProjectFormModalComponent } from "../../components/project-form-modal/project-form-modal.component";

@Component({
  selector: "app-project-list",
  imports: [
    CommonModule,
    RouterModule,
    ProjectCardComponent,
    ProjectFormModalComponent,
  ],
  templateUrl: "./project-list.component.html",
  styleUrl: "./project-list.component.css",
})
export class ProjectListComponent {
  private projectService = inject(ProjectService);

  projects: Project[] = [];

  showProjectModal = false;

  selectedProject?: Project;

  ngOnInit() {
    this.loadProjects();
  }

  loadProjects() {
    this.projectService.getProjects().subscribe((res) => {
      this.projects = res;
    });
  }

  openEditModal(project: Project) {
    this.selectedProject = project;

    this.showProjectModal = true;
  }

  openCreateModal() {
    this.selectedProject = undefined;

    this.showProjectModal = true;
  }

  closeProjectModal() {
    this.selectedProject = undefined;

    this.showProjectModal = false;
  }

  deleteProject(id: number) {
    if (!confirm("Sei sicuro?")) return;

    this.projectService.deleteProject(id).subscribe(() => {
      this.projects = this.projects.filter((p) => p.id !== id);
    });
  }
}
