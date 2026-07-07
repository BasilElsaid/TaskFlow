import { Component, Input } from "@angular/core";

@Component({
  selector: "app-auth-card",
  standalone: true,
  templateUrl: "./auth-card.component.html",
})
export class AuthCardComponent {
  @Input() title = "";
  @Input() subtitle = "";
}
