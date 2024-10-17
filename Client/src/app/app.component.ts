import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AppService } from '../service/app.service';
import { UserDto } from '../model/userDto';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgIf, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  appService = inject(AppService);
  users!: UserDto[];

  title = 'Dating App';

  ngOnInit(): void {
    this.appService.getUsers().subscribe({
      next: res => this.users = res,
      error: err => console.log(err)
    }

    );
  }
}
