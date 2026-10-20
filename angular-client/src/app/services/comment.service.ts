import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from './api.config';
import { CommentFilterResult } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private apiUrl = `${API_BASE_URL}/Comment`;

  constructor(private http: HttpClient) {}

  getComments(productId?: number, pageId: number = 1, take: number = 10): Observable<CommentFilterResult> {
    let params = new HttpParams()
      .set('pageId', pageId.toString())
      .set('take', take.toString());

    if (productId) {
      params = params.set('productId', productId.toString());
    }

    return this.http.get<CommentFilterResult>(this.apiUrl, { params });
  }
}
