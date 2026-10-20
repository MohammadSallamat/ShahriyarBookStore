import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SellerMock } from '../../models/models';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SellerService } from '../../services/seller.service';
import { SellerDto } from '../../models/models';

@Component({
  selector: 'app-seller-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container py-3">
      <h4 class="fw-bold mb-4 text-dark"><i class="bi bi-shop text-success me-2"></i> فروشندگان و ناشران همکار</h4>

      <div class="row g-4">
        <div class="col-md-6" *ngFor="let seller of mockSellers">
      <div *ngIf="loading" class="text-center py-5">
        <div class="spinner-border text-primary"></div>
        <p class="mt-2 text-muted">در حال دریافت لیست فروشندگان از وب ای‌پای‌آی...</p>
      </div>

      <div *ngIf="!loading && sellers.length > 0" class="row g-4">
        <div class="col-md-6" *ngFor="let seller of sellers">
          <div class="card border-0 shadow-sm p-4 h-100">
            <div class="d-flex justify-content-between align-items-start mb-3">
              <div>
                <h5 class="fw-bold text-dark mb-1">{{ seller.shopName }}</h5>
                <p class="text-muted small m-0"><i class="bi bi-person me-1"></i> مسئول: {{ seller.sellerName }}</p>
              </div>
              <span class="badge bg-success px-3 py-2">فعال و معتبر</span>
            </div>

            <!-- Seller Inventories -->
            <div class="bg-light p-3 rounded-3 mb-3">
              <h6 class="fw-bold text-secondary small mb-2"><i class="bi bi-boxes me-1"></i> موجودی انبار فروشگاه:</h6>
              <ul class="list-unstyled mb-0 small">
                <li *ngFor="let inv of seller.inventories" class="d-flex justify-content-between border-bottom py-1">
                  <span>{{ inv.bookTitle }}</span>
                  <span class="fw-bold text-primary">{{ inv.stockCount }} عدد در انبار</span>
                <p class="text-muted small m-0"><i class="bi bi-card-heading me-1"></i> کد ملی/شناسه: {{ seller.nationalCode }}</p>
              </div>
              <span class="badge bg-success px-3 py-2">تایید شده</span>
            </div>

            <!-- Seller Inventories -->
            <div class="bg-light p-3 rounded-3 mb-3" *ngIf="seller.inventories && seller.inventories.length > 0">
              <h6 class="fw-bold text-secondary small mb-2"><i class="bi bi-boxes me-1"></i> موجودی انبار فروشگاه:</h6>
              <ul class="list-unstyled mb-0 small">
                <li *ngFor="let inv of seller.inventories" class="d-flex justify-content-between border-bottom py-1">
                  <span>محصول #{{ inv.productId }}</span>
                  <span class="fw-bold text-primary">{{ inv.count }} عدد در انبار</span>
                </li>
              </ul>
            </div>

            <button class="btn btn-outline-primary btn-sm mt-auto" (click)="contactSeller(seller)">
              <i class="bi bi-telephone me-1"></i> ارتباط با فروشنده
            </button>
          </div>
        </div>
      </div>
    </div>
  `
})
export class SellerListComponent {
  mockSellers: SellerMock[] = [
    {
      id: 1,
      shopName: 'فروشگاه و انتشارات نگاه',
      sellerName: 'محمد رضایی',
      nationalCode: '۰۰۱۲۳۴۵۶۷۸',
      status: 'تایید شده',
      rating: 4.8,
      inventories: [
        { bookTitle: 'دیوان کامل شهریار', stockCount: 25, price: 280000 },
        { bookTitle: 'سمفونی مردگان', stockCount: 14, price: 175000 }
export class SellerListComponent implements OnInit {
  sellers: SellerDto[] = [];
  loading: boolean = true;

  fallbackSellers: SellerDto[] = [
    {
      id: 1,
      creationDate: '',
      userId: 1,
      shopName: 'فروشگاه و انتشارات نگاه',
      nationalCode: '۰۰۱۲۳۴۵۶۷۸',
      status: 1,
      inventories: [
        { id: 1, creationDate: '', sellerId: 1, productId: 1, count: 25, price: 280000 }
      ]
    },
    {
      id: 2,
      shopName: 'کتابفروشی تخصصی دانش و کامپیوتر',
      sellerName: 'سارا کاظمی',
      nationalCode: '۰۰۸۷۶۵۴۳۲۱',
      status: 'تایید شده',
      rating: 4.9,
      inventories: [
        { bookTitle: 'معماری تمیز (Clean Architecture)', stockCount: 30, price: 210000 }
      ]
    }
  ];

  contactSeller(seller: SellerMock): void {
      creationDate: '',
      userId: 2,
      shopName: 'کتابفروشی تخصصی دانش و کامپیوتر',
      nationalCode: '۰۰۸۷۶۵۴۳۲۱',
      status: 1,
      inventories: [
        { id: 2, creationDate: '', sellerId: 2, productId: 2, count: 30, price: 210000 }
      ]
    }
  ];

  constructor(private sellerService: SellerService) {}

  ngOnInit(): void {
    this.sellerService.getSellers().subscribe({
      next: (data) => {
        this.sellers = (data && data.length > 0) ? data : this.fallbackSellers;
        this.loading = false;
      },
      error: () => {
        this.sellers = this.fallbackSellers;
        this.loading = false;
      }
    });
  }

  contactSeller(seller: SellerDto): void {
    alert(`درخواست ارتباط با ${seller.shopName} ثبت شد.`);
  }
}
