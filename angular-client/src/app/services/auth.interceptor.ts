import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { API_BASE_URL } from './api.config';
import { AuthService } from './auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const base = new URL(API_BASE_URL, document.baseURI);
  let url: URL;
  try { url = new URL(req.url, document.baseURI); } catch { return next(req); }
  const apiPath = base.pathname.replace(/\/$/, '');
  const path = url.pathname.toLowerCase();
  if (url.origin !== base.origin || !(path === apiPath.toLowerCase() || path.startsWith(apiPath.toLowerCase() + '/')) ||
      path.startsWith(apiPath.toLowerCase() + '/auth/') || path === apiPath.toLowerCase() + '/user/register' ||
      req.headers.has('Authorization')) return next(req);

  const token = auth.accessToken;
  if (!token) return next(req);
  const withToken = (accessToken: string) => req.clone({ setHeaders: { Authorization: `Bearer ${accessToken}` } });
  return next(withToken(token)).pipe(catchError(error => {
    if (!(error instanceof HttpErrorResponse) || error.status !== 401 || !auth.accessToken) {
      return throwError(() => error);
    }
    // Another request may already have rotated the token.
    if (auth.accessToken !== token) return next(withToken(auth.accessToken));
    return auth.refresh().pipe(
      catchError(refreshError => {
        if (auth.accessToken === token) auth.clearSession();
        return throwError(() => refreshError);
      }),
      switchMap(tokens => next(withToken(tokens.accessToken)))
    );
  }));
};
