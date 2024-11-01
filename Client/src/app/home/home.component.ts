import { Component, effect, inject, OnInit } from '@angular/core';
import { RegisterFormComponent } from "../register-form/register-form.component";
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterFormComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  registerMode = false;
  authService = inject(AuthService);
  
  constructor(){
    effect(() => {
      this.authService.readonlyCurrentUser(); 
      this.registerMode = false;
    })
  }

  toggleRegisterMode(value: boolean){
    this.registerMode = value;
  }
}
