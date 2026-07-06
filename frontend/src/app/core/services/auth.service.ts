import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, map, Observable, tap } from "rxjs";
import { LoginRequest } from "../models/auth/login-request";
import { RegisterRequest } from "../models/auth/register-request";
import { TokenService } from "./token.service";
import { User } from "../models/auth/user";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly tokenService = inject(TokenService);

  private readonly api = "http://localhost:5271/api/auth";

  private authState = new BehaviorSubject<boolean>(
    this.tokenService.isLoggedIn(),
  );
  isLoggedIn$ = this.authState.asObservable();

  login(request: LoginRequest): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.api}/login`, request).pipe(
      tap((res) => {
        this.tokenService.setToken(res.token);
        this.authState.next(true);
      }),
    );
  }

  register(request: RegisterRequest): Observable<User> {
    return this.http.post<User>(`${this.api}/register`, request);
  }

  logout() {
    this.tokenService.removeToken();
    this.authState.next(false);
  }

  isLoggedIn(): boolean {
    return this.tokenService.isLoggedIn();
  }
}
