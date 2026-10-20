import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CartItem, ProductDto } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private STORAGE_KEY = 'shahriar_cart_items';
  private itemsSubject = new BehaviorSubject<CartItem[]>(this.loadCart());
  public items$: Observable<CartItem[]> = this.itemsSubject.asObservable();

  constructor() {}

  private loadCart(): CartItem[] {
    const saved = localStorage.getItem(this.STORAGE_KEY);
    return saved ? JSON.parse(saved) : [];
  }

  private saveCart(items: CartItem[]): void {
    localStorage.setItem(this.STORAGE_KEY, JSON.stringify(items));
    this.itemsSubject.next(items);
  }

  getItems(): CartItem[] {
    return this.itemsSubject.getValue();
  }

  addToCart(product: ProductDto, quantity: number = 1, price: number = 150000): void {
    const current = [...this.getItems()];
    const index = current.findIndex(i => i.product.id === product.id);

    if (index > -1) {
      current[index].quantity += quantity;
    } else {
      current.push({
        product,
        quantity,
        unitPrice: price
      });
    }

    this.saveCart(current);
  }

  updateQuantity(productId: number, quantity: number): void {
    let current = [...this.getItems()];
    if (quantity <= 0) {
      current = current.filter(i => i.product.id !== productId);
    } else {
      const item = current.find(i => i.product.id === productId);
      if (item) {
        item.quantity = quantity;
      }
    }
    this.saveCart(current);
  }

  removeFromCart(productId: number): void {
    const current = this.getItems().filter(i => i.product.id !== productId);
    this.saveCart(current);
  }

  clearCart(): void {
    this.saveCart([]);
  }

  getTotalCount(): number {
    return this.getItems().reduce((sum, item) => sum + item.quantity, 0);
  }

  getTotalPrice(): number {
    return this.getItems().reduce((sum, item) => sum + item.quantity * item.unitPrice, 0);
  }
}
