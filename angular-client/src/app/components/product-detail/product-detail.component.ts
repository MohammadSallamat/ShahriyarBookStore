import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { BookMock, CommentMock } from '../../models/models';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container py-3">
      <!-- Breadcrumb -->
      <nav class="mb-4">
        <ol class="breadcrumb bg-white p-3 rounded-3 shadow-sm">
          <li class="breadcrumb-item"><a routerLink="/" class="text-decoration-none">صفحه اصلی</a></li>
          <li class="breadcrumb-item"><a routerLink="/products" class="text-decoration-none">فهرست کتاب‌ها</a></li>
          <li class="breadcrumb-item active text-muted" aria-current="page">{{ book?.title }}</li>
        </ol>
      </nav>

      <div class="row g-4" *ngIf="book">
        <!-- Book Cover -->
        <div class="col-md-4">
          <div class="card border-0 shadow-sm p-4 text-center">
            <img [src]="book.imageUrl" [alt]="book.title" class="img-fluid rounded shadow object-fit-contain" style="max-height: 380px;">
          </div>
        </div>

        <!-- Book Details -->
        <div class="col-md-8">
          <div class="card border-0 shadow-sm p-4 h-100">
            <span class="badge bg-primary bg-opacity-10 text-primary border me-auto px-3 py-2 fs-6 mb-2">{{ book.category }}</span>
            <h2 class="fw-bold text-dark mb-2">{{ book.title }}</h2>
            <p class="text-muted fs-6 mb-3"><i class="bi bi-pen me-1"></i> نویسنده: <strong>{{ book.author }}</strong></p>

            <div class="d-flex align-items-center mb-4">
              <i class="bi bi-star-fill text-warning me-1" *ngFor="let s of [1,2,3,4,5]"></i>
              <span class="fw-bold ms-2">{{ book.rating }}</span>
              <span class="text-muted small ms-2">(از دیدگاه کاربران)</span>
            </div>

            <p class="text-secondary leading-relaxed mb-4 fs-6">
              {{ book.description }}
            </p>

            <!-- Specifications Table -->
            <div class="mb-4" *ngIf="book.specifications">
              <h6 class="fw-bold text-dark mb-3"><i class="bi bi-info-circle text-info me-1"></i> مشخصات شناسنامه‌ای کتاب</h6>
              <table class="table table-bordered table-striped table-sm align-middle">
                <tbody>
                  <tr *ngFor="let spec of book.specifications">
                    <th class="bg-light text-secondary w-25">{{ spec.key }}</th>
                    <td>{{ spec.value }}</td>
                  </tr>
                </tbody>
              </table>
            </div>

            <div class="mt-auto pt-3 border-top d-flex align-items-center justify-content-between">
              <div>
                <span class="d-block text-muted small">قیمت مصرف‌کننده:</span>
                <span class="fs-3 fw-bold text-success">{{ book.price | number }} تومان</span>
              </div>
              <button class="btn btn-warning btn-lg px-4 shadow-sm fw-bold" (click)="addToCart()">
                <i class="bi bi-cart-plus me-2"></i> افزودن به سبد خرید
              </button>
            </div>
          </div>
        </div>
      </div>

      <!-- Comments Section -->
      <div class="row mt-5">
        <div class="col-12">
          <div class="card border-0 shadow-sm p-4">
            <h5 class="fw-bold mb-4 text-dark"><i class="bi bi-chat-square-quote text-primary me-2"></i> نظرات و دیدگاه‌های خوانندگان</h5>

            <div *ngFor="let com of comments" class="border-bottom py-3">
              <div class="d-flex justify-content-between align-items-center mb-2">
                <div class="d-flex align-items-center gap-2">
                  <i class="bi bi-person-circle fs-4 text-primary"></i>
                  <strong class="text-dark">{{ com.userName }}</strong>
                </div>
                <small class="text-muted">{{ com.date }}</small>
              </div>
              <p class="text-secondary m-0 fs-6">{{ com.text }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class ProductDetailComponent implements OnInit {
  book: BookMock | null = null;

  comments: CommentMock[] = [
    {
      id: 1,
      userName: 'علی محمدی',
      userAvatar: '',
      text: 'کتاب بسیار ارزشمندی است. چاپ و کیفیت کاغذ عالی بود.',
      date: '۱۴۰۳/۰۲/۱۵',
      rating: 5
    },
    {
      id: 2,
      userName: 'زهرا احمدی',
      userAvatar: '',
      text: 'مطالعه این اثر را به همه علاقه‌مندان پیشنهاد می‌کنم.',
      date: '۱۴۰۳/۰۳/۰۱',
      rating: 5
    }
  ];

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.book = {
      id: 1,
      title: 'دیوان کامل شهریار',
      author: 'سید محمدحسین بهجت تبریزی',
      price: 280000,
      imageUrl: 'https://via.placeholder.com/300x400/1e3a8a/ffffff?text=دیوان+شهریار',
      description: 'مجموعه غزل‌ها، اشعار و منظومه ماندگار حیدربابایه سلام که اثر جاودان استاد شهریار است.',
      category: 'ادبیات و شعر فارسی',
      categoryId: 1,
      rating: 4.9,
      specifications: [
        { key: 'تعداد صفحات', value: '۸۵۰ صفحه' },
        { key: 'نوع جلد', value: 'گالینگور (سخت)' },
        { key: 'قطع کتاب', value: 'وزیری' },
        { key: 'ناشر', value: 'انتشارات نگاه' }
      ]
    };
  }

  addToCart(): void {
    alert(`کتاب "${this.book?.title}" به سبد خرید اضافه شد.`);
  }
}
