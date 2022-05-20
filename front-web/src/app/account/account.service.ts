import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, of, ReplaySubject } from 'rxjs';
import { API_CONFIG } from 'src/config/api.config';
import { IAddress } from '../shared/models/address';
import { IUser } from '../shared/models/user';
import { ShoppingCartService } from '../shopping-cart/shopping-cart.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private currentUserSource = new ReplaySubject<IUser>(1);
  currentUser$ = this.currentUserSource.asObservable();
  private isAdminSource = new ReplaySubject<boolean>(1);
  isAdmin$ = this.isAdminSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  loadCurrentUser(token: string) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get(`${API_CONFIG.baseUrl}/account`, {headers}).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          this.isAdminSource.next(this.isAdmin(user.token));
        }
      })
    );
  }

  login(values: any) {
    return this.http.post(`${API_CONFIG.baseUrl}/account/login`, values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          this.isAdminSource.next(this.isAdmin(user.token));
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  register(values: any) {
    return this.http.post(`${API_CONFIG.baseUrl}/account/register`, values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    )
  }

  checkIfEmailExists(email: string) {
    return this.http.get(`${API_CONFIG.baseUrl}/account/emailexists?email=${email}`);
  }

  getUserAddress() {
    return this.http.get<IAddress>(`${API_CONFIG.baseUrl}/account/address`);
  }

  updateUserAddress(address: IAddress) {
    return this.http.put<IAddress>(`${API_CONFIG.baseUrl}/account/address`, address);
  }

  isAdmin(token: string) : boolean {
    if (token) {
      const decodedToken = JSON.parse(atob(token.split('.')[1]));
      if (decodedToken.role.indexOf('Admin') > -1) {
        return true
      }
    }

    return false;
  }

}
