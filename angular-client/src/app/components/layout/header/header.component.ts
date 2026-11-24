import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <header class="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm py-2 px-3">
      <div class="container-fluid">
        <!-- Brand / Logo -->
        <a class="navbar-brand d-flex align-items-center gap-2 fw-bold text-white" routerLink="/">
          <i class="bi bi-book-half fs-3 text-warning"></i>
          <span>فروشگاه کتاب شهریار</span>
        </a>

        <!-- Header Actions (Cart, User Avatar, Search) -->
        <div class="d-flex align-items-center gap-3 ms-auto">
          <!-- Search input -->
          <div class="input-group input-group-sm d-none d-md-flex" style="width: 250px;">
            <input type="text" class="form-control" placeholder="جستجوی عنوان کتاب یا نویسنده...">
            <button class="btn btn-warning" type="button">
              <i class="bi bi-search"></i>
            </button>
          </div>

          <!-- Cart Icon -->
          <a routerLink="/cart" class="btn btn-outline-light position-relative btn-sm px-3">
            <i class="bi bi-cart3 fs-5"></i>
            <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
              ۲
            </span>
          </a>

          <a *ngIf="!auth.isAuthenticated()" routerLink="/login" class="btn btn-outline-light btn-sm text-nowrap">
            <i class="bi bi-person-circle me-1" aria-hidden="true"></i> ورود / ثبت‌نام
          </a>
          <div *ngIf="auth.isAuthenticated()" class="d-flex align-items-center gap-2 text-white">
            <span class="d-none d-md-inline small">حساب کاربری</span>
            <button type="button" class="btn btn-outline-light btn-sm text-nowrap" [disabled]="loggingOut" (click)="logout()">
              {{ loggingOut ? 'در حال خروج...' : 'خروج' }}
            </button>
          </div>
        </div>
      </div>
    </header>
  `,
  styles: [`
    header {
      background: linear-gradient(135deg, #0f172a 0%, #1e3a8a 100%) !important;
      border-bottom: 2px solid #f59e0b;
    }
    @media (max-width: 575px) {
      header .container-fluid { flex-wrap: wrap; gap: .75rem; }
      header .navbar-brand { font-size: 1rem; margin: 0; }
      header .container-fluid > .d-flex { gap: .5rem !important; }
    }
  `]
})
export class HeaderComponent {
  loggingOut = false;

  constructor(public readonly auth: AuthService, private readonly router: Router) {}

  logout(): void {
    if (this.loggingOut) return;
    this.loggingOut = true;
    this.auth.logout().pipe(finalize(() => this.loggingOut = false)).subscribe({
      next: () => void this.router.navigateByUrl('/'),
      error: () => {
        void this.router.navigate(['/login'], { queryParams: { logoutError: '1' } });
      }
    });
  }
}
