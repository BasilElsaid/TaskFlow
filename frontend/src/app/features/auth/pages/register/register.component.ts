import { Component, inject } from "@angular/core";
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router, RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";
import { TokenService } from "../../../../core/services/token.service";
import { AuthService } from "../../services/auth.service";
import { AuthFormModalComponent } from "../../components/auth-form-modal/auth-form-modal.component";

@Component({
  selector: "app-register",
  standalone: true,
  imports: [RouterModule, ReactiveFormsModule, CommonModule, AuthFormModalComponent],
  templateUrl: "./register.component.html",
  styleUrl: "./register.component.css",
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private tokenService = inject(TokenService);

  errorMessage: string | null = null;

  registerForm = this.fb.nonNullable.group({
    firstName: ["", Validators.required],
    lastName: ["", Validators.required],
    email: ["", [Validators.required, Validators.email]],
    password: ["", [Validators.required, Validators.minLength(6)]],
  });

  register() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const request = this.registerForm.getRawValue();

    this.authService.register(request).subscribe({
      next: () => {
        // 👇 login automatico subito dopo register
        this.authService
          .login({
            email: request.email,
            password: request.password,
          })
          .subscribe({
            next: (res) => {
              this.tokenService.setToken(res.token);
              this.router.navigate(["/dashboard"]);
            },
            error: () => {
              this.router.navigate(["/auth/login"]);
            },
          });
      },
      error: () => {
        this.errorMessage = "Registrazione fallita";
      },
    });
  }
}
