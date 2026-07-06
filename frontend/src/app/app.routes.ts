import { Routes } from "@angular/router";

import { HomeComponent } from "./pages/home/home.component";
import { LoginComponent } from "./pages/auth/login/login.component";
import { RegisterComponent } from "./pages/auth/register/register.component";
import { DashboardComponent } from "./pages/dashboard/dashboard.component";
import { authGuard } from "./core/guards/auth.guard";
import { ProjectTasksPageComponent } from "./pages/projects/project-tasks-page/project-tasks-page.component";
import { ProjectsPageComponent } from "./pages/projects/projects-page/projects-page.component";

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
        component: ProjectsPageComponent,
      },
      {
        path: "projects/:id/tasks",
        component: ProjectTasksPageComponent,
      },
    ],
  },
  {
    path: "**",
    redirectTo: "",
  },
];
