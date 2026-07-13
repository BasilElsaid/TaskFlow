import { Injectable, inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { CreateTaskRequest } from "../models/create-task-request";
import { Task } from "../models/task";
import { TaskFilterRequest } from "../models/task-filter-request";
import { UpdateTaskRequest } from "../models/update-task-request";
import { environment } from "../../../../environments/environment";


@Injectable({
  providedIn: "root",
})
export class TaskService {
  private readonly http = inject(HttpClient);

  private readonly projectApi = `${environment.apiUrl}/projects`;
  private readonly taskApi = `${environment.apiUrl}/tasks`;

  getTasks(projectId: number, filter?: TaskFilterRequest): Observable<Task[]> {
    let params = new HttpParams();

    if (filter?.taskStatus != null) {
      params = params.set("taskStatus", filter.taskStatus);
    }

    if (filter?.taskPriority != null) {
      params = params.set("taskPriority", filter.taskPriority);
    }

    if (filter?.search) {
      params = params.set("search", filter.search);
    }

    if (filter?.dueBefore) {
      params = params.set("dueBefore", filter.dueBefore);
    }

    if (filter?.assignedToMe != null) {
      params = params.set("assignedToMe", filter.assignedToMe);
    }

    return this.http.get<Task[]>(`${this.projectApi}/${projectId}/tasks`, {
      params,
    });
  }

  getTask(id: number): Observable<Task> {
    return this.http.get<Task>(`${this.taskApi}/${id}`);
  }

  createTask(projectId: number, request: CreateTaskRequest): Observable<Task> {
    return this.http.post<Task>(
      `${this.projectApi}/${projectId}/tasks`,
      request,
    );
  }

  updateTask(id: number, request: UpdateTaskRequest): Observable<Task> {
    return this.http.put<Task>(`${this.taskApi}/${id}`, request);
  }

  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.taskApi}/${id}`);
  }
}
