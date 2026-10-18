import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { OrderMock } from '../../models/models';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container py-3">
      <h4 class="fw-bold mb-4 text-dark"><i class="bi bi-clock-history text-primary me-2"></i> تاریخچه و پیگیری سفارشات</h4>

      <div class="row g-3">
        <div class="col-12" *ngFor="let order of mockOrders">
          <div class="card border-0 shadow-sm p-4">
            <div class="d-flex justify-content-between align-items-center border-bottom pb-3 mb-3">
              <div>
                <strong class="text-dark fs-5 me-3">سفارش {{ order.orderNumber }}#</strong>
                <small class="text-muted"><i class="bi bi-calendar3 me-1"></i> {{ order.date }}</small>
              </div>
              <span [class]="'badge px-3 py-2 ' + order.statusClass">{{ order.status }}</span>
            </div>

            <!-- Order Items List -->
            <div class="mb-3">
              <h6 class="fw-bold text-secondary mb-2 small">اقلام سفارش:</h6>
              <ul class="list-group list-group-flush">
                <li class="list-group-item bg-transparent d-flex justify-content-between align-items-center px-0 py-1" *ngFor="let item of order.items">
                  <span><i class="bi bi-book me-2 text-primary"></i> {{ item.bookTitle }} ({{ item.quantity }} جلد)</span>
                  <span class="fw-medium text-dark">{{ (item.price * item.quantity) | number }} تومان</span>
                </li>
              </ul>
            </div>

            <div class="d-flex justify-content-between align-items-center pt-2 border-top">
              <span class="text-muted small">جمع کل فاکتور:</span>
              <strong class="fs-5 text-success">{{ order.totalPrice | number }} تومان</strong>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class OrderListComponent {
  mockOrders: OrderMock[] = [
    {
      id: 101,
      orderNumber: 'SH-9801',
      date: '۱۴۰۳/۰۴/۱۰ - ۱۴:۳۰',
      status: 'تحویل داده شده',
      statusClass: 'bg-success',
      totalPrice: 490000,
      items: [
        { bookTitle: 'دیوان کامل شهریار', price: 280000, quantity: 1 },
        { bookTitle: 'معماری تمیز (Clean Architecture)', price: 210000, quantity: 1 }
      ]
    },
    {
      id: 102,
      orderNumber: 'SH-9802',
      date: '۱۴۰۳/۰۴/۱۸ - ۰۹:۱۵',
      status: 'در حال پردازش و ارسال',
      statusClass: 'bg-warning text-dark',
      totalPrice: 175000,
      items: [
        { bookTitle: 'سمفونی مردگان', price: 175000, quantity: 1 }
      ]
    }
  ];
}
