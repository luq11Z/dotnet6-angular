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

  uploadImage(file: File, id: number) {
    const formData = new FormData();
    formData.append('picture', file, 'image.png');

    return this.http.put(`${API_CONFIG.baseUrl}/products/${id}/picture`, formData, {
      reportProgress: true,
      observe: 'events'
    });
  }

  setMainPicture(pictureId: number, productId: number) {
    return this.http.put(`${API_CONFIG.baseUrl}/products/${productId}/picture/${pictureId}`, {});
  }

  deleteProductPicture(pictureId: number, productId: number) {
    return this.http.delete(`${API_CONFIG.baseUrl}/products/${productId}/picture/${pictureId}`);
  }

}
