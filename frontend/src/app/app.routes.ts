import { Routes } from "@angular/router";
import { authGuard } from "./core/guards/auth.guard";
import { LoginComponent } from "./features/auth/pages/login/login.component";
import { RegisterComponent } from "./features/auth/pages/register/register.component";
import { DashboardComponent } from "./features/dashboard/pages/dashboard/dashboard.component";
import { HomeComponent } from "./features/home/pages/home/home.component";
import { ProjectTasksPageComponent } from "./features/project/pages/project-tasks-page/project-tasks-page.component";
import { ProjectsPageComponent } from "./features/project/pages/projects-page/projects-page.component";


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
