import { Component, EventEmitter, inject, output, Output } from '@angular/core';
import {  FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})
export class RegisterFormComponent {
  cancelRegistrationEvent = output();
  registrationForm: FormGroup;
  authService = inject(AuthService);

  constructor(private formBuilder: FormBuilder) {
    this.registrationForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }
  
  cancel() {
    this.cancelRegistrationEvent.emit();
  }

  register() {
    if (this.registrationForm.valid) {
      this.authService.register(this.registrationForm.value.username, this.registrationForm.value.password).subscribe({
        next: () => {
          this.registrationForm.reset();
        },
        error: err => console.log(err)
      })
    }
  }
}
