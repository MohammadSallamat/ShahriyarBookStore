import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { BookMock, CategoryMock } from '../../models/models';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  template: `
    <div class="container-fluid py-2">
      <div class="row g-4">
        <!-- Sidebar Filter -->
        <div class="col-lg-3">
          <div class="card border-0 shadow-sm p-3 mb-4">
            <h5 class="fw-bold mb-3 text-dark"><i class="bi bi-funnel-fill text-primary me-2"></i> فیلتر و جستجوی کتاب</h5>

            <div class="mb-3">
              <label class="form-label small fw-bold">جستجوی عنوان / نویسنده</label>
              <div class="input-group input-group-sm">
                <input type="text" class="form-control" [(ngModel)]="searchQuery" placeholder="نام کتاب یا نویسنده...">
                <button class="btn btn-primary" type="button">
                  <i class="bi bi-search"></i>
                </button>
              </div>
            </div>

            <div class="mb-3">
              <label class="form-label small fw-bold">دسته‌بندی موضوعی</label>
              <select class="form-select form-select-sm" [(ngModel)]="selectedCategoryId">
                <option [value]="0">همه دسته‌بندی‌ها</option>
                <option *ngFor="let cat of mockCategories" [value]="cat.id">{{ cat.title }}</option>
              </select>
            </div>

            <button class="btn btn-outline-secondary btn-sm w-100" (click)="resetFilters()">
              <i class="bi bi-arrow-counterclockwise me-1"></i> بازنشانی فیلترها
            </button>
          </div>
        </div>

        <!-- Product Grid -->
        <div class="col-lg-9">
          <div class="d-flex justify-content-between align-items-center mb-3">
            <h4 class="fw-bold m-0"><i class="bi bi-journals text-warning me-2"></i> فهرست کامل کتاب‌ها</h4>
            <span class="badge bg-primary px-3 py-2 rounded-pill">نمایش {{ filteredBooks.length }} کتاب</span>
          </div>

          <div *ngIf="filteredBooks.length === 0" class="alert alert-warning text-center py-5 shadow-sm">
            <i class="bi bi-exclamation-triangle fs-2 d-block mb-2 text-warning"></i>
            کتابی متناسب با جستجوی شما یافت نشد.
          </div>

          <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-4" *ngIf="filteredBooks.length > 0">
            <div class="col" *ngFor="let book of filteredBooks">
              <div class="card h-100 border-0 shadow-sm product-card">
                <div class="position-relative overflow-hidden bg-light rounded-top text-center p-3" style="height: 220px;">
                  <a [routerLink]="['/product', book.id]">
                    <img [src]="book.imageUrl" [alt]="book.title" class="img-fluid h-100 object-fit-contain rounded shadow-sm">
                  </a>
                </div>
                <div class="card-body d-flex flex-column p-3">
                  <span class="badge bg-light text-primary border me-auto mb-2 small">{{ book.category }}</span>
                  <h6 class="card-title fw-bold">
                    <a [routerLink]="['/product', book.id]" class="text-decoration-none text-dark">{{ book.title }}</a>
                  </h6>
                  <p class="text-muted small mb-2"><i class="bi bi-person me-1"></i> {{ book.author }}</p>

                  <div class="mt-auto pt-2 border-top d-flex align-items-center justify-content-between">
                    <span class="fw-bold text-success">{{ book.price | number }} تومان</span>
                    <button class="btn btn-primary btn-sm rounded-pill px-3" (click)="addToCart(book)">
                      <i class="bi bi-cart-plus me-1"></i> خرید
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class ProductListComponent {
  searchQuery: string = '';
  selectedCategoryId: number = 0;

  mockCategories: CategoryMock[] = [
    { id: 1, title: 'ادبیات و شعر فارسی', count: 42, icon: 'bi-book' },
    { id: 2, title: 'رمان و داستان کوتاه', count: 85, icon: 'bi-journal-bookmark' },
    { id: 3, title: 'برنامه‌نویسی و وب', count: 34, icon: 'bi-code-slash' },
    { id: 4, title: 'تاریخ و فرهنگ', count: 29, icon: 'bi-bank' }
  ];

  mockBooks: BookMock[] = [
    {
      id: 1,
      title: 'دیوان کامل شهریار',
      author: 'سید محمدحسین بهجت تبریزی',
      price: 280000,
      imageUrl: 'https://via.placeholder.com/200x280/1e3a8a/ffffff?text=دیوان+شهریار',
      description: 'مجموعه غزل‌ها، اشعار و منظومه ماندگار حیدربابایه سلام.',
      category: 'ادبیات و شعر فارسی',
      categoryId: 1,
      rating: 4.9
    },
    {
      id: 2,
      title: 'معماری تمیز (Clean Architecture)',
      author: 'رابرت سی. مارتین',
      price: 210000,
      imageUrl: 'https://via.placeholder.com/200x280/2563eb/ffffff?text=Clean+Architecture',
      description: 'راهنمای عملی ساختاردهی نرم‌افزارهای مقیاس‌پذیر و قابل نگهداری.',
      category: 'برنامه‌نویسی و وب',
      categoryId: 3,
      rating: 4.8
    },
    {
      id: 3,
      title: 'سمفونی مردگان',
      author: 'عباس معروفی',
      price: 175000,
      imageUrl: 'https://via.placeholder.com/200x280/dc2626/ffffff?text=سمفونی+مردگان',
      description: 'رمان برجسته ادبیات معاصر ایران درباره سرنوشت یک خانواده.',
      category: 'رمان و داستان کوتاه',
      categoryId: 2,
      rating: 4.7
    },
    {
      id: 4,
      title: 'تاریخ مستطاب آمریکا',
      author: 'محمدصادق کوشکی',
      price: 130000,
      imageUrl: 'https://via.placeholder.com/200x280/d97706/ffffff?text=تاریخ+مستطاب',
      description: 'روایتی جذاب و طنزگونه از تاریخ آمریکا.',
      category: 'تاریخ و فرهنگ',
      categoryId: 4,
      rating: 4.5
    }
  ];

  get filteredBooks(): BookMock[] {
    return this.mockBooks.filter(book => {
      const matchesSearch = !this.searchQuery ||
        book.title.includes(this.searchQuery) ||
        book.author.includes(this.searchQuery);
      const matchesCategory = this.selectedCategoryId === 0 || book.categoryId === Number(this.selectedCategoryId);
      return matchesSearch && matchesCategory;
    });
  }

  resetFilters(): void {
    this.searchQuery = '';
    this.selectedCategoryId = 0;
  }

  addToCart(book: BookMock): void {
    alert(`کتاب "${book.title}" به سبد خرید اضافه شد.`);
  }
}
