import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { CreateProjectRequest } from "../models/project/create-project-request";
import { Project } from "../models/project/project";
import { UpdateProjectRequest } from "../models/project/update-project-request";


@Injectable({
  providedIn: "root",
})
export class ProjectService {
  private readonly http = inject(HttpClient);

  private readonly projectsApi = "http://localhost:5271/api/projects";

  getProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.projectsApi);
  }

  getProject(id: number): Observable<Project> {
    return this.http.get<Project>(`${this.projectsApi}/${id}`);
  }

  createProject(request: CreateProjectRequest): Observable<Project> {
    return this.http.post<Project>(this.projectsApi, request);
  }

  updateProject(
    id: number,
    request: UpdateProjectRequest,
  ): Observable<Project> {
    return this.http.put<Project>(`${this.projectsApi}/${id}`, request);
  }

  deleteProject(id: number): Observable<void> {
    return this.http.delete<void>(`${this.projectsApi}/${id}`);
  }
}
