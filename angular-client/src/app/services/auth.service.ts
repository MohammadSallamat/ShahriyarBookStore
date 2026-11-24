import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, finalize, map, shareReplay, throwError } from 'rxjs';
import { API_BASE_URL } from './api.config';

export interface AuthTokens {
  accessToken: string;
  refreshToken: string;
}

const SESSION_KEY = 'shahriyar.auth';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly session = signal<AuthTokens | null>(this.readSession());
  readonly isAuthenticated = () => this.session() !== null;
  private refreshing: Observable<AuthTokens> | null = null;
  private generation = 0;

  constructor(private readonly http: HttpClient) {}

  get accessToken(): string | null { return this.session()?.accessToken ?? null; }

  login(phoneNumber: string, password: string): Observable<AuthTokens> {
    return this.http.post<AuthTokens>(`${API_BASE_URL}/Auth/login`, { phoneNumber, password })
      .pipe(map(tokens => this.acceptTokens(tokens)));
  }

  requestOtp(phoneNumber: string): Observable<unknown> {
    return this.http.post(`${API_BASE_URL}/Auth/request-otp`, { phoneNumber });
  }

  verifyOtp(phoneNumber: string, code: string): Observable<AuthTokens> {
    return this.http.post<AuthTokens>(`${API_BASE_URL}/Auth/verify-otp`, { phoneNumber, code })
      .pipe(map(tokens => this.acceptTokens(tokens)));
  }

  register(phoneNumber: string, password: string): Observable<unknown> {
    return this.http.post(`${API_BASE_URL}/User/register`, { phoneNumber, password });
  }

  refresh(): Observable<AuthTokens> {
    if (this.refreshing) return this.refreshing;
    const refreshToken = this.session()?.refreshToken;
    if (!refreshToken) return throwError(() => new Error('No session'));
    const generation = this.generation;
    const request = this.http.post<AuthTokens>(`${API_BASE_URL}/Auth/refresh-token`, { refreshToken }).pipe(
      map(tokens => {
        if (generation !== this.generation) throw new Error('Session changed');
        return this.acceptTokens(tokens, false);
      }),
      finalize(() => { if (this.refreshing === request) this.refreshing = null; }),
      shareReplay({ bufferSize: 1, refCount: false })
    );
    this.refreshing = request;
    return request;
  }

  logout(): Observable<unknown> {
    const refreshToken = this.session()?.refreshToken;
    this.clearSession();
    return refreshToken
      ? this.http.post(`${API_BASE_URL}/Auth/logout`, { refreshToken })
      : new Observable(subscriber => { subscriber.next(null); subscriber.complete(); });
  }

  clearSession(): void {
    this.generation++;
    this.refreshing = null;
    this.session.set(null);
    try { window.localStorage.removeItem(SESSION_KEY); } catch { /* storage may be unavailable */ }
  }

  private acceptTokens(tokens: AuthTokens, newSession = true): AuthTokens {
    if (!tokens || typeof tokens.accessToken !== 'string' || !tokens.accessToken ||
        typeof tokens.refreshToken !== 'string' || !tokens.refreshToken) {
      throw new Error('Invalid authentication response');
    }
    if (newSession) { this.generation++; this.refreshing = null; }
    this.session.set(tokens);
    try { window.localStorage.setItem(SESSION_KEY, JSON.stringify(tokens)); } catch { /* session remains in memory */ }
    return tokens;
  }

  private readSession(): AuthTokens | null {
    try {
      const value = JSON.parse(window.localStorage.getItem(SESSION_KEY) ?? 'null') as AuthTokens | null;
      return value && typeof value.accessToken === 'string' && value.accessToken &&
        typeof value.refreshToken === 'string' && value.refreshToken ? value : null;
    } catch { return null; }
  }
}
