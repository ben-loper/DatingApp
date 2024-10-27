import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [ReactiveFormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  loginForm: FormGroup;
  loggedIn = false;
  userId!: string;

  constructor(private apiService: ApiService, private formBuilder: FormBuilder){
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login(): void {
    this.apiService.login(this.loginForm.value.username, this.loginForm.value.password).subscribe({
      next: res => {
            this.loggedIn = true;
            this.loginForm.reset();
            this.userId = res.nameId;
          },
      error: err => console.log(err)
    })
  }

  logout(): void {
    this.loggedIn = false;
  }
}
