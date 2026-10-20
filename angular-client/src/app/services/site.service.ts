import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from './api.config';
import { SliderDto, BannerDto } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class SiteService {
  private apiUrl = `${API_BASE_URL}/SiteEntities`;

  constructor(private http: HttpClient) {}

  getSliders(): Observable<SliderDto[]> {
    return this.http.get<SliderDto[]>(`${this.apiUrl}/sliders`);
  }

  getBanners(): Observable<BannerDto[]> {
    return this.http.get<BannerDto[]>(`${this.apiUrl}/banners`);
  }
}
