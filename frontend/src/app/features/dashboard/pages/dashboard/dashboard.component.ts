import { CommonModule } from "@angular/common";
import { Component, inject } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ProjectService } from "../../../project/services/project.service";
import { TaskService } from "../../../task/services/task.service";
import { ProjectFormModalComponent } from "../../../project/components/project-form-modal/project-form-modal.component";
import { ProjectCardComponent } from "../../../project/components/project-card/project-card.component";
import { Project } from "../../../project/models/project";

@Component({
  selector: "app-dashboard",
  imports: [
    RouterModule,
    CommonModule,
    ProjectFormModalComponent,
    ProjectCardComponent,
  ],
  templateUrl: "./dashboard.component.html",
  styleUrl: "./dashboard.component.css",
})
export class DashboardComponent {
  private projectService = inject(ProjectService);
  private taskService = inject(TaskService);
  showProjectModal = false;
  selectedProject?: Project;

  stats = {
    projects: 0,
    tasks: 0,
    completed: 0,
    dueSoon: 0,
  };

  projects: any[] = [];
  activities: any[] = [];

  ngOnInit() {
    this.loadDashboard();
  }

  loadDashboard() {
    this.projectService.getProjects().subscribe((projects) => {
      this.projects = projects;

      this.stats.projects = projects.length;
    });

    let allTasks: any[] = [];

    this.projectService.getProjects().subscribe((projects) => {
      projects.forEach((p) => {
        this.taskService.getTasks(p.id).subscribe((tasks) => {
          allTasks = [...allTasks, ...tasks];

          this.calculateStats(allTasks);

          this.activities = tasks.slice(0, 5).map((t) => ({
            text: `Task: "${t.title}"`,
            time: t.createdAt,
          }));
        });
      });
    });
  }

  private calculateStats(tasks: any[]) {
    this.stats.tasks = tasks.length;

    this.stats.completed = tasks.filter(
      (t) => t.taskStatus === 2, // COMPLETED (dipende dal tuo enum)
    ).length;

    const now = new Date();

    this.stats.dueSoon = tasks.filter((t) => {
      if (!t.dueDate) return false;

      const due = new Date(t.dueDate);

      const diff = (due.getTime() - now.getTime()) / (1000 * 60 * 60 * 24);

      return diff <= 3 && diff >= 0;
    }).length;
  }

  openEditModal(project: Project) {
    this.selectedProject = project;
    this.showProjectModal = true;
  }

  deleteProject(id: number) {
    if (!confirm("Sei sicuro di eliminare il progetto?")) return;

    this.projectService.deleteProject(id).subscribe(() => {
      this.projects = this.projects.filter((p) => p.id !== id);

      this.stats.projects = this.projects.length;
    });
  }

  openCreateModal() {
    this.selectedProject = undefined;
    this.showProjectModal = true;
  }

  closeProjectModal() {
    this.selectedProject = undefined;
    this.showProjectModal = false;
  }
}
