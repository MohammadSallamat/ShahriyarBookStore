import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CommentService } from '../../services/comment.service';
import { CartService } from '../../services/cart.service';
import { ProductDto, CommentDto } from '../../models/models';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container py-3">
      <div *ngIf="loading" class="text-center py-5">
        <div class="spinner-border text-primary"></div>
        <p class="mt-2 text-muted">در حال دریافت داده‌ها از وب ای‌پای‌آی...</p>
      </div>

      <div *ngIf="!loading && book" class="row g-4">
        <!-- Book Cover -->
        <div class="col-md-4">
          <div class="card border-0 shadow-sm p-4 text-center">
            <img [src]="book.imageName || 'https://via.placeholder.com/300x400/1e3a8a/ffffff?text=Book'" [alt]="book.title" class="img-fluid rounded shadow object-fit-contain" style="max-height: 380px;">
          </div>
        </div>

        <!-- Book Details -->
        <div class="col-md-8">
          <div class="card border-0 shadow-sm p-4 h-100">
            <h2 class="fw-bold text-dark mb-2">{{ book.title }}</h2>
            <p class="text-muted small mb-3">شناسه کتاب: #{{ book.id }} | اسلاگ: {{ book.slug }}</p>

            <p class="text-secondary leading-relaxed mb-4 fs-6">
              {{ book.description || 'توضیحات تکمیلی ثبت نشده است.' }}
            </p>

            <!-- Specifications Table -->
            <div class="mb-4" *ngIf="book.specifications && book.specifications.length > 0">
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
                <span class="fs-3 fw-bold text-success">۱۵۰,۰۰۰ تومان</span>
              </div>
              <button class="btn btn-warning btn-lg px-4 shadow-sm fw-bold" (click)="addToCart()">
                <i class="bi bi-cart-plus me-2"></i> افزودن به سبد خرید
              </button>
            </div>
          </div>
        </div>
      </div>

      <!-- Comments Section -->
      <div class="row mt-5" *ngIf="!loading">
        <div class="col-12">
          <div class="card border-0 shadow-sm p-4">
            <h5 class="fw-bold mb-4 text-dark"><i class="bi bi-chat-square-quote text-primary me-2"></i> نظرات و دیدگاه‌های خوانندگان</h5>

            <div *ngIf="comments.length === 0" class="alert alert-light text-center py-4 border">
              هنوز نظری ثبت نشده است.
            </div>

            <div *ngFor="let com of comments" class="border-bottom py-3">
              <div class="d-flex justify-content-between align-items-center mb-2">
                <span class="fw-bold text-primary"><i class="bi bi-person-circle me-1"></i> کاربر #{{ com.userId }}</span>
                <small class="text-muted">{{ com.creationDate | date:'yyyy/MM/dd' }}</small>
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
  book: ProductDto | null = null;
  comments: CommentDto[] = [];
  loading: boolean = true;

  fallbackBook: ProductDto = {
    id: 1,
    title: 'دیوان کامل شهریار',
    imageName: 'https://via.placeholder.com/300x400/1e3a8a/ffffff?text=دیوان+شهریار',
    description: 'مجموعه غزل‌ها، اشعار و منظومه ماندگار حیدربابایه سلام که اثر جاودان استاد شهریار است.',
    categoryId: 1,
    subCategoryId: 0,
    secondarySubCategoryId: 0,
    slug: 'shahriar-poet',
    creationDate: '',
    images: [],
    specifications: [
      { id: 1, creationDate: '', productId: 1, key: 'تعداد صفحات', value: '۸۵۰ صفحه' },
      { id: 2, creationDate: '', productId: 1, key: 'نوع جلد', value: 'گالینگور (سخت)' }
    ]
  };

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private commentService: CommentService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? Number(idParam) : 1;

    this.productService.getProductById(id).subscribe({
      next: (data) => {
        this.book = data || this.fallbackBook;
        this.loading = false;
        this.loadComments(id);
      },
      error: () => {
        this.book = this.fallbackBook;
        this.loading = false;
      }
    });
  }

  loadComments(productId: number): void {
    this.commentService.getComments(productId).subscribe({
      next: (res) => {
        this.comments = res ? res.comments : [];
      },
      error: () => {}
    });
  }

  addToCart(): void {
    if (this.book) {
      this.cartService.addToCart(this.book);
      alert(`کتاب "${this.book.title}" به سبد خرید اضافه شد.`);
    }
  }
}
