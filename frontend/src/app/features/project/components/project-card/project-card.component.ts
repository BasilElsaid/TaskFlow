import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Input, Output } from "@angular/core";
import { RouterModule } from "@angular/router";
import { Project } from "../../models/project";

@Component({
  selector: "app-project-card",
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: "./project-card.component.html",
})
export class ProjectCardComponent {
  @Input() project!: Project;

  @Output() edit = new EventEmitter<Project>();
  @Output() delete = new EventEmitter<number>();
}
