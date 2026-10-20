import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CartItemMock } from '../../models/models';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CartService } from '../../services/cart.service';
import { CartItem } from '../../models/models';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container py-3">
      <h4 class="fw-bold mb-4 text-dark"><i class="bi bi-cart3 text-warning me-2"></i> سبد خرید شما</h4>

      <div *ngIf="cartItems.length === 0" class="alert alert-light border text-center py-5 shadow-sm">
        <i class="bi bi-cart-x fs-1 d-block mb-3 text-muted"></i>
        <h5>سبد خرید شما در حال حاضر خالی است.</h5>
        <a routerLink="/products" class="btn btn-primary mt-3">مشاهده کتاب‌ها</a>
      </div>

      <div *ngIf="cartItems.length > 0" class="row g-4">
        <!-- Cart Table -->
        <div class="col-lg-8">
          <div class="card border-0 shadow-sm p-3">
            <div class="table-responsive">
              <table class="table align-middle m-0">
                <thead>
                  <tr class="table-light">
                    <th>عنوان کتاب</th>
                    <th class="text-center">قیمت واحد</th>
                    <th class="text-center">تعداد</th>
                    <th class="text-center">جمع کل</th>
                    <th></th>
                  </tr>
                </thead>
                <tbody>
                  <tr *ngFor="let item of cartItems">
                    <td>
                      <div class="d-flex align-items-center gap-3">
                        <img [src]="item.book.imageUrl" class="rounded shadow-sm" style="width: 45px; height: 60px; object-fit: cover;">
                        <div>
                          <strong class="d-block text-dark">{{ item.book.title }}</strong>
                          <small class="text-muted">{{ item.book.author }}</small>
                        </div>
                      </div>
                    </td>
                    <td class="text-center fw-medium">{{ item.book.price | number }} تومان</td>
                    <td class="text-center">
                      <div class="input-group input-group-sm justify-content-center" style="width: 100px; margin: 0 auto;">
                        <button class="btn btn-outline-secondary" (click)="decreaseQty(item)">-</button>
                        <span class="input-group-text bg-white fw-bold px-3">{{ item.quantity }}</span>
                        <button class="btn btn-outline-secondary" (click)="increaseQty(item)">+</button>
                      </div>
                    </td>
                    <td class="text-center fw-bold text-success">{{ (item.book.price * item.quantity) | number }} تومان</td>
                    <td class="text-end">
                      <button class="btn btn-outline-danger btn-sm" (click)="removeItem(item)">
                        <img [src]="item.product.imageName || 'https://via.placeholder.com/45x60?text=Book'" class="rounded shadow-sm" style="width: 45px; height: 60px; object-fit: cover;">
                        <div>
                          <strong class="d-block text-dark">{{ item.product.title }}</strong>
                          <small class="text-muted">کد: #{{ item.product.id }}</small>
                        </div>
                      </div>
                    </td>
                    <td class="text-center fw-medium">{{ item.unitPrice | number }} تومان</td>
                    <td class="text-center">
                      <div class="input-group input-group-sm justify-content-center" style="width: 100px; margin: 0 auto;">
                        <button class="btn btn-outline-secondary" (click)="updateQuantity(item.product.id, item.quantity - 1)">-</button>
                        <span class="input-group-text bg-white fw-bold px-3">{{ item.quantity }}</span>
                        <button class="btn btn-outline-secondary" (click)="updateQuantity(item.product.id, item.quantity + 1)">+</button>
                      </div>
                    </td>
                    <td class="text-center fw-bold text-success">{{ (item.unitPrice * item.quantity) | number }} تومان</td>
                    <td class="text-end">
                      <button class="btn btn-outline-danger btn-sm" (click)="removeItem(item.product.id)">
                        <i class="bi bi-trash"></i>
                      </button>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>

        <!-- Order Summary -->
        <div class="col-lg-4">
          <div class="card border-0 shadow-sm p-4">
            <h5 class="fw-bold mb-3 border-bottom pb-2">خلاصه فاکتور خرید</h5>

            <div class="d-flex justify-content-between mb-2 text-muted">
              <span>تعداد کتاب‌ها:</span>
              <strong class="text-dark">{{ totalCount }} جلد</strong>
              <span>تعداد کل اقلام:</span>
              <strong class="text-dark">{{ totalCount }} عدد</strong>
            </div>

            <div class="d-flex justify-content-between mb-3 text-muted">
              <span>هزینه ارسال پستی:</span>
              <strong class="text-success">رایگان</strong>
            </div>

            <hr>

            <div class="d-flex justify-content-between mb-4">
              <span class="fw-bold fs-6">مبلغ قابل پرداخت:</span>
              <span class="fw-bold fs-4 text-success">{{ totalPrice | number }} تومان</span>
            </div>

            <button class="btn btn-success btn-lg w-100 shadow-sm fw-bold" (click)="checkout()">
              <i class="bi bi-check-circle me-2"></i> ثبت و نهایی‌سازی سفارش
            </button>
          </div>
        </div>
      </div>
    </div>
  `
})
export class CartComponent {
  cartItems: CartItemMock[] = [
    {
      book: {
        id: 1,
        title: 'دیوان کامل شهریار',
        author: 'سید محمدحسین بهجت تبریزی',
        price: 280000,
        imageUrl: 'https://via.placeholder.com/200x280/1e3a8a/ffffff?text=دیوان+شهریار',
        description: '',
        category: 'ادبیات',
        categoryId: 1,
        rating: 5
      },
      quantity: 1
    },
    {
      book: {
        id: 2,
        title: 'معماری تمیز (Clean Architecture)',
        author: 'رابرت سی. مارتین',
        price: 210000,
        imageUrl: 'https://via.placeholder.com/200x280/2563eb/ffffff?text=Clean+Architecture',
        description: '',
        category: 'برنامه‌نویسی',
        categoryId: 3,
        rating: 5
      },
      quantity: 1
    }
  ];

  get totalPrice(): number {
    return this.cartItems.reduce((sum, item) => sum + (item.book.price * item.quantity), 0);
  }

  get totalCount(): number {
    return this.cartItems.reduce((sum, item) => sum + item.quantity, 0);
  }

  increaseQty(item: CartItemMock): void {
    item.quantity++;
  }

  decreaseQty(item: CartItemMock): void {
    if (item.quantity > 1) {
      item.quantity--;
    }
  }

  removeItem(item: CartItemMock): void {
    this.cartItems = this.cartItems.filter(i => i.book.id !== item.book.id);
  }

  checkout(): void {
    alert('سفارش آزمایشی شما با موفقیت ثبت شد!');
    this.cartItems = [];
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  totalPrice: number = 0;
  totalCount: number = 0;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cartService.items$.subscribe(items => {
      this.cartItems = items;
      this.totalPrice = this.cartService.getTotalPrice();
      this.totalCount = this.cartService.getTotalCount();
    });
  }

  updateQuantity(productId: number, qty: number): void {
    this.cartService.updateQuantity(productId, qty);
  }

  removeItem(productId: number): void {
    this.cartService.removeFromCart(productId);
  }

  checkout(): void {
    alert('سفارش شما با موفقیت ثبت شد!');
    this.cartService.clearCart();
  }
}
