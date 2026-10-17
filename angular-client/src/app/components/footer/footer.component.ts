import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule],
  template: `
    <footer class="bg-dark text-white pt-5 pb-4 mt-5 border-top border-warning border-3">
      <div class="container">
        <div class="row g-4">
          <div class="col-md-4">
            <h5 class="text-warning mb-3">فروشگاه کتاب شهریار</h5>
            <p class="text-white-50 leading-relaxed fs-6">
              ارائه‌دهنده ارزشمندترین آثار ادبی، داستانی، علمی و آموزشی به یاد شاعر بزرگ ایران‌زمین، استاد شهریار.
            </p>
          </div>
          <div class="col-md-4">
            <h5 class="text-warning mb-3">دسترسی سریع</h5>
            <ul class="list-unstyled text-white-50">
              <li class="mb-2"><a href="/products" class="text-decoration-none text-white-50">فهرست کامل کتاب‌ها</a></li>
              <li class="mb-2"><a href="/cart" class="text-decoration-none text-white-50">سبد خرید</a></li>
              <li class="mb-2"><a href="/orders" class="text-decoration-none text-white-50">سفارشات من</a></li>
            </ul>
          </div>
          <div class="col-md-4">
            <h5 class="text-warning mb-3">معماری و تکنولوژی</h5>
            <p class="text-white-50 mb-1"><i class="bi bi-code-slash text-warning me-2"></i> ASP.NET Core Web API (Clean Architecture & CQRS)</p>
            <p class="text-white-50 mb-1"><i class="bi bi-layers text-warning me-2"></i> Angular + TypeScript + Bootstrap</p>
          </div>
        </div>
        <hr class="my-4 border-secondary">
        <div class="text-center text-white-50 fs-6">
          کلیه حقوق این سیستم محفوظ است &copy; {{ currentYear }} - پروژه شهریار
        </div>
      </div>
    </footer>
  `
})
export class FooterComponent {
  currentYear: number = new Date().getFullYear();
}
