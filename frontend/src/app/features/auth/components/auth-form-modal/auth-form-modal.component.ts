import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-auth-form-modal',
  imports: [],
  templateUrl: './auth-form-modal.component.html',
  styleUrl: './auth-form-modal.component.css'
})
export class AuthFormModalComponent {
  @Input() title = "";
  @Input() subtitle = "";
}
