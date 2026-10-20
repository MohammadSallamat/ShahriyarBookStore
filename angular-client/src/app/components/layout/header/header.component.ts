import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

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

          <!-- User Profile -->
          <div class="d-flex align-items-center gap-2 text-white border-start ps-3 me-2">
            <i class="bi bi-person-circle fs-4 text-warning"></i>
            <div class="d-none d-sm-block text-end">
              <span class="d-block fw-medium small">کاربر مهمان</span>
              <small class="text-white-50" style="font-size: 0.75rem;">توسعه‌دهنده بک‌اند</small>
            </div>
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
  `]
})
export class HeaderComponent {}
