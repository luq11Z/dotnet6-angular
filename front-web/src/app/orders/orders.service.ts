import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_CONFIG } from 'src/config/api.config';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private http: HttpClient) { }

  getOrdersForUser() {
    return this.http.get(`${API_CONFIG.baseUrl}/orders`);
  }

  getOrderDetails(id: number) {
    return this.http.get(`${API_CONFIG.baseUrl}/orders/${id}`);
  }

}
