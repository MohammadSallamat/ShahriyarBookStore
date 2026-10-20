import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from './api.config';
import { OrderDto } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = `${API_BASE_URL}/Order`;

  constructor(private http: HttpClient) {}

  getOrderById(id: number): Observable<OrderDto> {
    return this.http.get<OrderDto>(`${this.apiUrl}/${id}`);
  }

  getOrdersByUserId(userId: number): Observable<OrderDto[]> {
    return this.http.get<OrderDto[]>(`${this.apiUrl}/user/${userId}`);
  }
}
