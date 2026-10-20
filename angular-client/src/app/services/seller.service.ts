import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from './api.config';
import { SellerDto } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class SellerService {
  private apiUrl = `${API_BASE_URL}/Seller`;

  constructor(private http: HttpClient) {}

  getSellers(): Observable<SellerDto[]> {
    return this.http.get<SellerDto[]>(this.apiUrl);
  }

  getSellerById(id: number): Observable<SellerDto> {
    return this.http.get<SellerDto>(`${this.apiUrl}/${id}`);
  }
}
