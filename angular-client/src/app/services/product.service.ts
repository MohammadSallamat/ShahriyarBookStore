import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from './api.config';
import { ProductDto, ProductFilterParams, ProductFilterResult } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = `${API_BASE_URL}/Product`;

  constructor(private http: HttpClient) {}

  getProducts(filter: ProductFilterParams): Observable<ProductFilterResult> {
    let params = new HttpParams()
      .set('pageId', filter.pageId.toString())
      .set('take', filter.take.toString());

    if (filter.search) {
      params = params.set('search', filter.search);
    }
    if (filter.categoryId) {
      params = params.set('categoryId', filter.categoryId.toString());
    }

    return this.http.get<ProductFilterResult>(this.apiUrl, { params });
  }

  getProductById(id: number): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${this.apiUrl}/${id}`);
  }

  getProductBySlug(slug: string): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${this.apiUrl}/by-slug/${slug}`);
  }
}
