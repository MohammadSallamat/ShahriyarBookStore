import { TestBed } from '@angular/core/testing';
import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { authInterceptor } from './auth.interceptor';
import { API_BASE_URL } from './api.config';

describe('authentication', () => {
  let auth: AuthService;
  let http: HttpClient;
  let requests: HttpTestingController;
  const tokens = { accessToken: 'access-1', refreshToken: 'refresh-1' };

  beforeEach(() => {
    const storage = new Map<string, string>();
    Object.defineProperty(window, 'localStorage', { configurable: true, value: {
      getItem: (key: string) => storage.get(key) ?? null,
      setItem: (key: string, value: string) => storage.set(key, value),
      removeItem: (key: string) => storage.delete(key)
    } });
    TestBed.configureTestingModule({ providers: [provideHttpClient(withInterceptors([authInterceptor])), provideHttpClientTesting()] });
    auth = TestBed.inject(AuthService);
    http = TestBed.inject(HttpClient);
    requests = TestBed.inject(HttpTestingController);
  });
  afterEach(() => { requests.verify(); window.localStorage.removeItem('shahriyar.auth'); });

  function login(): void {
    auth.login('09123456789', 'secret').subscribe();
    const request = requests.expectOne(`${API_BASE_URL}/Auth/login`);
    expect(request.request.body).toEqual({ phoneNumber: '09123456789', password: 'secret' });
    expect(request.request.headers.has('Authorization')).toBe(false);
    request.flush(tokens);
  }

  it('persists login and clears the session on logout', () => {
    login();
    expect(auth.isAuthenticated()).toBe(true);
    expect(JSON.parse(window.localStorage.getItem('shahriyar.auth')!)).toEqual(tokens);
    auth.logout().subscribe();
    expect(auth.isAuthenticated()).toBe(false);
    expect(window.localStorage.getItem('shahriyar.auth')).toBeNull();
    const logout = requests.expectOne(`${API_BASE_URL}/Auth/logout`);
    expect(logout.request.body).toEqual({ refreshToken: 'refresh-1' });
    logout.flush({});
  });

  it('sends OTP and registration payloads without bearer tokens', () => {
    login();
    auth.requestOtp('09123456789').subscribe();
    const otp = requests.expectOne(`${API_BASE_URL}/Auth/request-otp`);
    expect(otp.request.body).toEqual({ phoneNumber: '09123456789' });
    expect(otp.request.headers.has('Authorization')).toBe(false);
    otp.flush({});
    auth.verifyOtp('09123456789', '123456').subscribe();
    const verify = requests.expectOne(`${API_BASE_URL}/Auth/verify-otp`);
    expect(verify.request.body).toEqual({ phoneNumber: '09123456789', code: '123456' });
    verify.flush(tokens);
    auth.register('09123456789', 'secret').subscribe();
    const register = requests.expectOne(`${API_BASE_URL}/User/register`);
    expect(register.request.body).toEqual({ phoneNumber: '09123456789', password: 'secret' });
    expect(register.request.headers.has('Authorization')).toBe(false);
    register.flush({});
  });

  it('shares a refresh for concurrent 401 responses and retries both requests', () => {
    login();
    http.get(`${API_BASE_URL}/Order/1`).subscribe();
    http.get(`${API_BASE_URL}/Order/2`).subscribe();
    const first = requests.expectOne(`${API_BASE_URL}/Order/1`);
    const second = requests.expectOne(`${API_BASE_URL}/Order/2`);
    expect(first.request.headers.get('Authorization')).toBe('Bearer access-1');
    first.flush({}, { status: 401, statusText: 'Unauthorized' });
    second.flush({}, { status: 401, statusText: 'Unauthorized' });
    const refresh = requests.expectOne(`${API_BASE_URL}/Auth/refresh-token`);
    expect(refresh.request.body).toEqual({ refreshToken: 'refresh-1' });
    refresh.flush({ accessToken: 'access-2', refreshToken: 'refresh-2' });
    const retries = requests.match(req => req.url.includes('/Order/'));
    expect(retries.length).toBe(2);
    retries.forEach(req => { expect(req.request.headers.get('Authorization')).toBe('Bearer access-2'); req.flush({}); });
  });

  it('never sends tokens to other origins or API lookalikes', () => {
    login();
    http.get('https://example.com/api/Order').subscribe();
    http.get('http://localhost:5000/api-evil/Order').subscribe();
    const external = requests.expectOne('https://example.com/api/Order');
    const lookalike = requests.expectOne('http://localhost:5000/api-evil/Order');
    expect(external.request.headers.has('Authorization')).toBe(false);
    expect(lookalike.request.headers.has('Authorization')).toBe(false);
    external.flush({}); lookalike.flush({});
  });

  it('clears a session if refresh fails', () => {
    login();
    http.get(`${API_BASE_URL}/Order/1`).subscribe({ error: () => {} });
    requests.expectOne(`${API_BASE_URL}/Order/1`).flush({}, { status: 401, statusText: 'Unauthorized' });
    requests.expectOne(`${API_BASE_URL}/Auth/refresh-token`).flush({}, { status: 400, statusText: 'Bad Request' });
    expect(auth.isAuthenticated()).toBe(false);
  });
});
