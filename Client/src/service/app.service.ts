import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { UserDto } from "../model/userDto";

@Injectable()
export class AppService {
    http = inject(HttpClient);

    getUsers() {
        return this.http.get<UserDto[]>('https://localhost:7211/api/user');
    }
}