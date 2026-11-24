import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthService } from '../../services/auth.service';

type AuthMode = 'password' | 'otp' | 'register';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <div class="auth-wrap py-3 py-md-5" dir="rtl">
      <div class="card border-0 shadow-sm auth-card mx-auto">
        <div class="card-body p-4 p-md-5">
          <div class="text-center mb-4">
            <i class="bi bi-person-circle text-primary display-5" aria-hidden="true"></i>
            <h1 class="h4 fw-bold mt-3">ورود به حساب کاربری</h1>
            <p class="text-muted small mb-0">به فروشگاه کتاب شهریار خوش آمدید</p>
          </div>
          <div class="d-flex gap-2 mb-4 auth-tabs" aria-label="روش ورود">
            <button type="button" class="btn flex-fill" [class.btn-primary]="mode === 'password'" [class.btn-light]="mode !== 'password'" [disabled]="loading" (click)="setMode('password')">رمز عبور</button>
            <button type="button" class="btn flex-fill" [class.btn-primary]="mode === 'otp'" [class.btn-light]="mode !== 'otp'" [disabled]="loading" (click)="setMode('otp')">کد یک‌بارمصرف</button>
            <button type="button" class="btn flex-fill" [class.btn-primary]="mode === 'register'" [class.btn-light]="mode !== 'register'" [disabled]="loading" (click)="setMode('register')">ثبت‌نام</button>
          </div>
          <div *ngIf="error" class="alert alert-danger" role="alert">{{ error }}</div>
          <div *ngIf="message" class="alert alert-success" role="status">{{ message }}</div>
          <form (ngSubmit)="submit()" #authForm="ngForm" novalidate>
            <div class="mb-3">
              <label for="phone" class="form-label fw-medium">شماره موبایل</label>
              <input id="phone" name="phone" class="form-control text-start" dir="ltr" type="tel" inputmode="tel" autocomplete="tel" placeholder="09123456789" [(ngModel)]="phone" [disabled]="loading || (mode === 'otp' && otpRequested)" required>
            </div>
            <div *ngIf="mode !== 'otp'" class="mb-3">
              <label for="password" class="form-label fw-medium">رمز عبور</label>
              <input id="password" name="password" class="form-control text-start" dir="ltr" type="password" [attr.autocomplete]="mode === 'register' ? 'new-password' : 'current-password'" [(ngModel)]="password" [disabled]="loading" required>
            </div>
            <div *ngIf="mode === 'register'" class="mb-3">
              <label for="confirmPassword" class="form-label fw-medium">تکرار رمز عبور</label>
              <input id="confirmPassword" name="confirmPassword" class="form-control text-start" dir="ltr" type="password" autocomplete="new-password" [(ngModel)]="confirmPassword" [disabled]="loading" required>
            </div>
            <div *ngIf="mode === 'otp' && otpRequested" class="mb-3">
              <label for="code" class="form-label fw-medium">کد یک‌بارمصرف</label>
              <input id="code" name="code" class="form-control text-start" dir="ltr" inputmode="numeric" autocomplete="one-time-code" [(ngModel)]="code" [disabled]="loading" required>
              <button type="button" class="btn btn-link px-0 small" [disabled]="loading" (click)="changePhone()">تغییر شماره یا درخواست دوباره کد</button>
            </div>
            <p *ngIf="mode === 'otp'" class="text-muted small">با تأیید کد، در صورت نداشتن حساب، حساب جدید ساخته می‌شود. در محیط توسعه کد در گزارش سرور ثبت می‌شود.</p>
            <button type="submit" class="btn btn-primary w-100 py-2" [disabled]="loading">
              <span *ngIf="loading" class="spinner-border spinner-border-sm me-2" aria-hidden="true"></span>
              {{ loading ? 'لطفاً صبر کنید...' : mode === 'register' ? 'ثبت‌نام' : mode === 'password' ? 'ورود' : otpRequested ? 'تأیید و ورود' : 'درخواست کد' }}
            </button>
          </form>
          <a routerLink="/" class="d-block text-center small mt-4 text-decoration-none">بازگشت به فروشگاه</a>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .auth-card { max-width: 540px; }
    .auth-card:hover { transform: none; }
    .auth-tabs .btn { font-size: .875rem; white-space: nowrap; }
    @media (max-width: 400px) { .auth-tabs { flex-wrap: wrap; } .auth-tabs .btn { min-width: 40%; } }
  `]
})
export class AuthComponent {
  mode: AuthMode = 'password';
  phone = '';
  password = '';
  confirmPassword = '';
  code = '';
  otpRequested = false;
  loading = false;
  error = '';
  message = '';

  constructor(private readonly auth: AuthService, private readonly router: Router, route: ActivatedRoute) {
    if (route.snapshot.queryParamMap.has('logoutError')) {
      this.error = 'خروج از این دستگاه انجام شد، اما ارتباط با سرور برای لغو نشست برقرار نشد.';
    }
  }

  setMode(mode: AuthMode): void {
    this.mode = mode;
    this.password = '';
    this.confirmPassword = '';
    this.code = '';
    this.otpRequested = false;
    this.error = '';
    this.message = '';
  }

  changePhone(): void { this.otpRequested = false; this.code = ''; this.error = ''; this.message = ''; }

  submit(): void {
    if (this.loading) return;
    this.error = '';
    this.message = '';
    const phone = this.normalizeDigits(this.phone).trim();
    if (!/^09\d{9}$/.test(phone)) { this.error = 'شماره موبایل معتبر وارد کنید (مثلاً 09123456789).'; return; }
    if (this.mode !== 'otp' && !this.password) { this.error = 'رمز عبور را وارد کنید.'; return; }
    if (this.mode === 'register' && this.password.length < 4) { this.error = 'رمز عبور باید حداقل ۴ نویسه باشد.'; return; }
    if (this.mode === 'register' && this.password !== this.confirmPassword) { this.error = 'رمزهای عبور یکسان نیستند.'; return; }
    const code = this.normalizeDigits(this.code).trim();
    if (this.mode === 'otp' && this.otpRequested && !/^\d{4,8}$/.test(code)) { this.error = 'کد یک‌بارمصرف معتبر وارد کنید.'; return; }

    this.loading = true;
    const operation = this.mode === 'password' ? this.auth.login(phone, this.password)
      : this.mode === 'register' ? this.auth.register(phone, this.password)
      : this.otpRequested ? this.auth.verifyOtp(phone, code) : this.auth.requestOtp(phone);
    operation.pipe(finalize(() => this.loading = false)).subscribe({
      next: () => {
        if (this.mode === 'register') {
          this.setMode('password');
          this.message = 'ثبت‌نام انجام شد. اکنون با رمز عبور وارد شوید.';
        } else if (this.mode === 'otp' && !this.otpRequested) {
          this.phone = phone;
          this.otpRequested = true;
          this.message = 'درخواست کد ثبت شد. کد را وارد کنید.';
        } else {
          void this.router.navigateByUrl('/');
        }
      },
      error: (err: unknown) => {
        const body = err instanceof HttpErrorResponse ? err.error : null;
        this.error = err instanceof HttpErrorResponse && err.status === 503 && this.mode === 'otp'
          ? 'دریافت کد در حال حاضر در دسترس نیست. لطفاً بعداً تلاش کنید.'
          : typeof body?.message === 'string' ? body.message : 'عملیات انجام نشد. لطفاً دوباره تلاش کنید.';
      }
    });
  }

  private normalizeDigits(value: string): string {
    return value.replace(/[۰-۹٠-٩]/g, digit => String('۰۱۲۳۴۵۶۷۸۹'.includes(digit) ? digit.charCodeAt(0) - 1776 : digit.charCodeAt(0) - 1632));
  }
}
