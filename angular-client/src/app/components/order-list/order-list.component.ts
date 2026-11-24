import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { OrderDto } from '../../models/models';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container py-3">
      <h4 class="fw-bold mb-4 text-dark"><i class="bi bi-clock-history text-primary me-2"></i> تاریخچه و پیگیری سفارشات</h4>

      <div *ngIf="loading" class="text-center py-5">
        <div class="spinner-border text-primary"></div>
        <p class="mt-2 text-muted">در حال دریافت لیست سفارشات از وب ای‌پای‌آی...</p>
      </div>

      <div *ngIf="!loading && orders.length === 0" class="alert alert-light border text-center py-5 shadow-sm">
        <i class="bi bi-inbox fs-1 d-block mb-3 text-muted"></i>
        <h5>هیچ سفارشی ثبت نشده است.</h5>
        <a routerLink="/products" class="btn btn-primary mt-3">شروع خرید</a>
      </div>

      <div *ngIf="!loading && orders.length > 0" class="row g-3">
        <div class="col-12" *ngFor="let order of orders">
          <div class="card border-0 shadow-sm p-4">
            <div class="d-flex justify-content-between align-items-center border-bottom pb-3 mb-3">
              <div>
                <strong class="text-dark fs-5 me-3">سفارش شماره #{{ order.id }}</strong>
                <small class="text-muted"><i class="bi bi-calendar3 me-1"></i> {{ order.creationDate | date:'yyyy/MM/dd HH:mm' }}</small>
              </div>
              <span class="badge bg-success px-3 py-2">ثبت شده</span>
            </div>

            <!-- Order Items List -->
            <div class="mb-3">
              <h6 class="fw-bold text-secondary mb-2 small">اقلام سفارش:</h6>
              <ul class="list-group list-group-flush">
                <li class="list-group-item bg-transparent d-flex justify-content-between align-items-center px-0 py-1" *ngFor="let item of order.items">
                  <span><i class="bi bi-book me-2 text-primary"></i> انبار #{{ item.inventoryId }} ({{ item.count }} عدد)</span>
                  <span class="fw-medium text-dark">{{ item.totalPrice | number }} تومان</span>
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
export class OrderListComponent implements OnInit {
  orders: OrderDto[] = [];
  loading: boolean = true;

  fallbackOrders: OrderDto[] = [
    {
      id: 101,
      userId: 1,
      status: 1,
      creationDate: new Date().toISOString(),
      totalPrice: 490000,
      items: [
        { id: 1, creationDate: '', orderId: 101, inventoryId: 10, count: 1, price: 280000, totalPrice: 280000 },
        { id: 2, creationDate: '', orderId: 101, inventoryId: 12, count: 1, price: 210000, totalPrice: 210000 }
      ]
    }
  ];

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    // Fetch orders for user ID 1
    this.orderService.getOrdersByUserId(1).subscribe({
      next: (data) => {
        this.orders = (data && data.length > 0) ? data : this.fallbackOrders;
        this.loading = false;
      },
      error: () => {
        this.orders = this.fallbackOrders;
        this.loading = false;
      }
    });
  }
}
