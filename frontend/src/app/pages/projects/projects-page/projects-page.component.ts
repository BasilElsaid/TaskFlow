import { Component, inject, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { ProjectService } from "../../../core/services/project.service";


@Component({
  selector: "app-projects-page",
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: "./projects-page.component.html",
})
export class ProjectsPageComponent implements OnInit {
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
}
