import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_CONFIG } from 'src/config/api.config';
import { IPagination } from 'src/app/shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http: HttpClient) { }

  getProducts(){
    return this.http.get<IPagination>(`${API_CONFIG.baseUrl}/products`);
  }
  
}
