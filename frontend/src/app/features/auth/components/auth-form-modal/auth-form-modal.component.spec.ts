import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthFormModalComponent } from './auth-form-modal.component';

describe('AuthFormModalComponent', () => {
  let component: AuthFormModalComponent;
  let fixture: ComponentFixture<AuthFormModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthFormModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuthFormModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
