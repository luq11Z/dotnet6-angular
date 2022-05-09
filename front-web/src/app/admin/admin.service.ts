import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_CONFIG } from 'src/config/api.config';
import { ProductFormValues } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  createProduct(product: ProductFormValues) {
    return this.http.post(`${API_CONFIG.baseUrl}/products`, product);
  }

  updateProduct(product: ProductFormValues, id: number) {
    return this.http.put(`${API_CONFIG.baseUrl}/products/${id}`, product);
  }

  deleteProduct(id: number) {
    return this.http.delete(`${API_CONFIG.baseUrl}/products/${id}`);
  }
}
