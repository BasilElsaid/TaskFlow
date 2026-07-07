import { CommonModule } from "@angular/common";
import { Component, inject } from "@angular/core";
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router, RouterModule } from "@angular/router";
import { TokenService } from "../../../../core/services/token.service";
import { LoginRequest } from "../../models/login-request";
import { AuthService } from "../../services/auth.service";
import { AuthFormModalComponent } from "../../components/auth-form-modal/auth-form-modal.component";


@Component({
  selector: "app-login",
  imports: [RouterModule, CommonModule, ReactiveFormsModule, AuthFormModalComponent],
  templateUrl: "./login.component.html",
  styleUrl: "./login.component.css",
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private tokenService = inject(TokenService);
  private router = inject(Router);

  loading = false;
  errorMessage: string | null = null;

  loginForm = this.fb.nonNullable.group({
    email: ["", [Validators.required, Validators.email]],
    password: ["", [Validators.required]],
  });

  ngOnInit() {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(["/dashboard"]);
    }
  }

  login() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.errorMessage = null;

    const request = this.loginForm.getRawValue() as LoginRequest;

    this.authService.login(request).subscribe({
      next: (res) => {
        this.tokenService.setToken(res.token);
        this.router.navigate(["/dashboard"]);
      },
      error: () => {
        this.loading = false;
        this.errorMessage = "Email o password non validi";
      },
    });
  }
}
