import { Routes } from "@angular/router";
import { authGuard } from "./core/guards/auth.guard";
import { LoginComponent } from "./features/auth/pages/login/login.component";
import { RegisterComponent } from "./features/auth/pages/register/register.component";
import { DashboardComponent } from "./features/dashboard/pages/dashboard/dashboard.component";
import { HomeComponent } from "./features/home/pages/home/home.component";
import { ProjectListComponent } from "./features/project/pages/project-list/project-list.component";
import { ProjectDetailsComponent } from "./features/project/pages/project-details/project-details.component";

export const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
  },
  {
    path: "auth",
    children: [
      {
        path: "login",
        component: LoginComponent,
      },
      {
        path: "register",
        component: RegisterComponent,
      },
    ],
  },
  {
    path: "dashboard",
    canActivate: [authGuard],
    children: [
      {
        path: "",
        component: DashboardComponent,
      },
      {
        path: "projects",
        component: ProjectListComponent,
      },
      {
        path: "projects/:id",
        component: ProjectDetailsComponent,
      },
    ],
  },
  {
    path: "**",
    redirectTo: "",
  },
];
