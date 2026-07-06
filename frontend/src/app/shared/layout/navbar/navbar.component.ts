import { CommonModule } from "@angular/common";
import { Component, inject } from "@angular/core";
import { Router, RouterModule } from "@angular/router";
import { AuthService } from "../../../features/auth/services/auth.service";

@Component({
  selector: "app-navbar",
  imports: [RouterModule, CommonModule],
  templateUrl: "./navbar.component.html",
  styleUrl: "./navbar.component.css",
})
export class NavbarComponent {

  private authService = inject(AuthService);
  private router = inject(Router);

  isLoggedIn$ = this.authService.isLoggedIn$;

  logout() {
    this.authService.logout();
    this.router.navigate(["/auth/login"]);
  }
}
