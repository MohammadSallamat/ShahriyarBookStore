import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CategoryService } from '../../services/category.service';
import { CartService } from '../../services/cart.service';
import { ProductDto, CategoryDto } from '../../models/models';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <!-- Hero Banner -->
    <div class="hero-banner text-white p-4 p-md-5 rounded-4 shadow-sm mb-5 text-center position-relative overflow-hidden">
      <div class="position-relative z-1">
        <span class="badge bg-warning text-dark px-3 py-2 rounded-pill fs-6 mb-3 fw-bold">
          <i class="bi bi-star-fill me-1"></i> به یاد استاد شهریار
        </span>
        <h1 class="display-5 fw-bold mb-3">فروشگاه تخصصی کتاب شهریار</h1>
        <p class="lead mb-4 text-white-50 mx-auto" style="max-width: 700px;">
          متصل به ASP.NET Core Web API و معماری Clean Architecture & CQRS.
        </p>
        <div class="d-flex justify-content-center gap-3">
          <a routerLink="/products" class="btn btn-warning btn-lg px-4 shadow-sm fw-bold">
            <i class="bi bi-journal-text me-2"></i> مشاهده کتاب‌ها
          </a>
        </div>
      </div>
    </div>

    <!-- Featured Categories Grid -->
    <section class="mb-5" *ngIf="categories.length > 0">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h4 class="fw-bold text-dark m-0"><i class="bi bi-grid-fill text-primary me-2"></i> دسته‌بندی‌های برگزیده</h4>
          <small class="text-muted">موضوعات متنوع برای علاقه‌مندان به مطالعه</small>
        </div>
        <a routerLink="/products" class="btn btn-sm btn-outline-primary rounded-pill px-3">مشاهده همه</a>
      </div>

      <div class="row row-cols-2 row-cols-md-4 g-3">
        <div class="col" *ngFor="let cat of categories.slice(0, 4)">
          <a [routerLink]="['/products']" [queryParams]="{categoryId: cat.id}" class="card h-100 border-0 shadow-sm hover-card text-center p-3 text-decoration-none text-dark">
            <div class="rounded-circle bg-primary bg-opacity-10 text-primary mx-auto mb-3 d-flex align-items-center justify-content-center" style="width: 50px; height: 50px;">
              <i class="bi bi-bookmark-heart fs-4"></i>
            </div>
            <h6 class="fw-bold mb-1">{{ cat.title }}</h6>
          </a>
        </div>
      </div>
    </section>

    <!-- Featured Books Section -->
    <section class="mb-5">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h4 class="fw-bold text-dark m-0"><i class="bi bi-bookmarks-fill text-warning me-2"></i> تازه‌ترین کتاب‌ها</h4>
          <small class="text-muted">دریافت مستقیم داده‌ها از Web API</small>
        </div>
      </div>

      <div *ngIf="loading" class="text-center py-5">
        <div class="spinner-border text-primary"></div>
        <p class="mt-2 text-muted">در حال دریافت داده‌ها از وب ای‌پای‌آی...</p>
      </div>

      <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4" *ngIf="!loading && products.length > 0">
        <div class="col" *ngFor="let book of products">
          <div class="card h-100 border-0 shadow-sm product-card">
            <div class="position-relative overflow-hidden bg-light rounded-top text-center p-3" style="height: 240px;">
              <a [routerLink]="['/product', book.id]">
                <img [src]="book.imageName || 'https://via.placeholder.com/200x280/1e3a8a/ffffff?text=Shahriar+Book'" [alt]="book.title" class="img-fluid h-100 object-fit-contain rounded shadow-sm">
              </a>
            </div>
            <div class="card-body d-flex flex-column p-3">
              <h6 class="card-title fw-bold text-dark text-truncate" [title]="book.title">
                <a [routerLink]="['/product', book.id]" class="text-decoration-none text-dark">{{ book.title }}</a>
              </h6>
              <p class="text-muted small mb-2 line-clamp-2">{{ book.description || 'توضیحات موجود نیست' }}</p>

              <div class="mt-auto pt-2 border-top d-flex align-items-center justify-content-between">
                <span class="fw-bold text-success fs-6">۱۵۰,۰۰۰ تومان</span>
                <button class="btn btn-primary btn-sm px-3 rounded-pill" (click)="addToCart(book)">
                  <i class="bi bi-cart-plus me-1"></i> خرید
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  `,
  styles: [`
    .hero-banner {
      background: linear-gradient(135deg, #0f172a 0%, #1e3a8a 50%, #2563eb 100%);
      border-bottom: 4px solid #f59e0b;
    }
    .hover-card {
      transition: transform 0.2s ease, box-shadow 0.2s ease;
    }
    .hover-card:hover {
      transform: translateY(-4px);
      box-shadow: 0 10px 20px rgba(0,0,0,0.08) !important;
    }
    .line-clamp-2 {
      display: -webkit-box;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
      overflow: hidden;
    }
  `]
})
export class HomeComponent implements OnInit {
  products: ProductDto[] = [];
  categories: CategoryDto[] = [];
  loading: boolean = true;

  fallbackProducts: ProductDto[] = [
    {
      id: 1,
      title: 'دیوان کامل شهریار',
      imageName: 'https://via.placeholder.com/200x280/1e3a8a/ffffff?text=دیوان+شهریار',
      description: 'مجموعه غزل‌ها، اشعار و منظومه ماندگار حیدربابایه سلام.',
      categoryId: 1,
      subCategoryId: 0,
      secondarySubCategoryId: 0,
      slug: 'shahriar-poet',
      creationDate: '',
      images: [],
      specifications: []
    },
    {
      id: 2,
      title: 'معماری تمیز (Clean Architecture)',
      imageName: 'https://via.placeholder.com/200x280/2563eb/ffffff?text=Clean+Architecture',
      description: 'راهنمای عملی ساختاردهی نرم‌افزارهای مقیاس‌پذیر و قابل نگهداری.',
      categoryId: 3,
      subCategoryId: 0,
      secondarySubCategoryId: 0,
      slug: 'clean-architecture',
      creationDate: '',
      images: [],
      specifications: []
    }
  ];

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.productService.getProducts({ pageId: 1, take: 8 }).subscribe({
      next: (res) => {
        this.products = (res && res.products && res.products.length > 0) ? res.products : this.fallbackProducts;
        this.loading = false;
      },
      error: () => {
        this.products = this.fallbackProducts;
        this.loading = false;
      }
    });

    this.categoryService.getCategories().subscribe({
      next: (res) => this.categories = res || [],
      error: () => {}
    });
  }

  addToCart(product: ProductDto): void {
    this.cartService.addToCart(product);
    alert(`کتاب "${product.title}" به سبد خرید اضافه شد.`);
  }
}
