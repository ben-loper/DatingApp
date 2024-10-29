import { inject, Injectable, signal } from '@angular/core';
import { map, Observable } from 'rxjs';
import { UserDto } from '../models/user-dto';
import { AuthRequestDto } from '../models/auth-request';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);

  private currentUser = signal<UserDto | null>(null);
  
  readonlyCurrentUser = this.currentUser.asReadonly();

  constructor() {
    const user = this.getCurrentUserFromLocalStorage();
    this.currentUser.set(user);
  }

  login(username: string, password: string) : Observable<UserDto> {
    const userAuthDto = new AuthRequestDto(username, password);

    return this.http.post<UserDto>(`https://localhost:7211/api/Account/login`, userAuthDto).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      })
    );
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }

  getCurrentUserFromLocalStorage() : UserDto | null {
    const user = localStorage.getItem('user');
    if (!user) return null;    
    return JSON.parse(user);
  }
}
