import { CommonModule } from "@angular/common";
import { Component, EventEmitter, inject, Input, Output } from "@angular/core";
import { ReactiveFormsModule, FormBuilder, Validators } from "@angular/forms";
import { ProjectService } from "../../services/project.service";
import { Project } from "../../models/project";

@Component({
  selector: "app-project-form-modal",
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: "./project-form-modal.component.html",
  styleUrl: "./project-form-modal.component.css",
})
export class ProjectFormModalComponent {
  private fb = inject(FormBuilder);
  private projectService = inject(ProjectService);

  @Input() project?: Project;

  @Input() title = "";
  @Input() subtitle = "";

  @Output() close = new EventEmitter<void>();
  @Output() saved = new EventEmitter<void>();

  projectForm = this.fb.nonNullable.group({
    name: ["", Validators.required],
    description: [""],
  });

  loading = false;
  errorMessage = "";

  ngOnInit() {
    if (this.project) {
      this.projectForm.patchValue({
        name: this.project.name,
        description: this.project.description,
      });
    }
  }

  saveProject() {
    if (this.projectForm.invalid) {
      this.projectForm.markAllAsTouched();
      return;
    }

    this.loading = true;

    const request = this.projectForm.getRawValue();

    const operation = this.project
      ? this.projectService.updateProject(this.project.id, request)
      : this.projectService.createProject(request);

    operation.subscribe({
      next: () => {
        this.loading = false;
        this.saved.emit();
        this.close.emit();
      },

      error: () => {
        this.loading = false;
        this.errorMessage = "Errore durante il salvataggio del progetto";
      },
    });
  }

  closeModal() {
    this.close.emit();
  }
}
