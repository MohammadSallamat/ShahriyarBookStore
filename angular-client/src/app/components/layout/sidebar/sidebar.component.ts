import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CategoryMock } from '../../../models/models';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <aside class="bg-white border rounded-3 h-100 shadow-sm p-3">
      <div class="mb-4 text-center pb-3 border-bottom">
        <h6 class="fw-bold text-muted text-uppercase small tracking-wide">منوی دسترسی سریع</h6>
      </div>

      <ul class="nav nav-pills flex-column mb-auto gap-2">
        <li class="nav-item">
          <a class="nav-link d-flex align-items-center gap-2 text-dark" routerLink="/" routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}">
            <i class="bi bi-house-door-fill fs-5"></i>
            <span>صفحه اصلی</span>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link text-dark d-flex align-items-center gap-2" routerLink="/products" routerLinkActive="active">
            <i class="bi bi-journal-bookmark fs-5 text-success"></i>
            <span>فهرست کتاب‌ها</span>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link text-dark d-flex align-items-center gap-2" routerLink="/cart" routerLinkActive="active">
            <i class="bi bi-cart-check fs-5 text-warning"></i>
            <span>سبد خرید</span>
            <span class="badge bg-danger rounded-pill ms-auto">۲</span>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link text-dark d-flex align-items-center gap-2" routerLink="/orders" routerLinkActive="active">
            <i class="bi bi-clock-history fs-5 text-info"></i>
            <span>پیگیری سفارشات</span>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link text-dark d-flex align-items-center gap-2" routerLink="/sellers" routerLinkActive="active">
            <i class="bi bi-shop fs-5 text-primary"></i>
            <span>فروشندگان همکار</span>
          </a>
        </li>
      </ul>

      <hr class="my-4">

      <!-- Mock Categories List in Sidebar -->
      <div class="mb-2">
        <h6 class="fw-bold text-dark small mb-3"><i class="bi bi-tags-fill text-danger me-1"></i> دسته‌بندی موضوعی</h6>

        <div class="d-flex flex-column gap-2">
          <a *ngFor="let category of mockCategories" routerLink="/products" class="sidebar-category-link d-flex justify-content-between align-items-center text-decoration-none text-secondary p-2 rounded">
            <span>{{ category.title }}</span>
            <span class="badge bg-light text-dark border rounded-pill">{{ category.count }}</span>
          </a>
        </div>
      </div>
    </aside>
  `,
  styles: [`
    .nav-link {
      border-radius: 8px;
      font-weight: 500;
      transition: all 0.2s ease;
    }
    .nav-link:hover {
      background-color: #f1f5f9;
    }
    .nav-link.active {
      background-color: #2563eb !important;
      color: #fff !important;
    }
    .nav-link.active i {
      color: #fff !important;
    }
    .sidebar-category-link:hover {
      background-color: #f8fafc;
      color: #2563eb !important;
    }
  `]
})
export class SidebarComponent {
  mockCategories: CategoryMock[] = [
    { id: 1, title: 'ادبیات و شعر فارسی', count: 42, icon: 'bi-book' },
    { id: 2, title: 'رمان و داستان کوتاه', count: 85, icon: 'bi-journal-bookmark' },
    { id: 3, title: 'برنامه‌نویسی و وب', count: 34, icon: 'bi-code-slash' },
    { id: 4, title: 'تاریخ و فرهنگ', count: 29, icon: 'bi-bank' },
    { id: 5, title: 'روانشناسی و موفقیت', count: 50, icon: 'bi-person-badge' }
  ];
}
