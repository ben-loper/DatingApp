import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserDto } from '../models/user-dto';
import { AuthRequestDto } from '../models/auth-request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  public login(username: string, password: string) : Observable<UserDto> {
    const userAuthDto = new AuthRequestDto(username, password);

    return this.http.post<UserDto>(`https://localhost:7211/api/Account/login`, userAuthDto);
  }
}
