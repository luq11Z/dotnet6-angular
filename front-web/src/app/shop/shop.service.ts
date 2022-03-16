import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_CONFIG } from 'src/config/api.config';
import { IPagination } from 'src/app/shared/models/pagination';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/product-type';
import { map } from 'rxjs';
import { ShopParams } from '../shared/models/shop-params';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams){
    let params = new HttpParams();

    if(shopParams.brandId !== 0){
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if(shopParams.typeId !== 0){
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageIndex.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http.get<IPagination>(`${API_CONFIG.baseUrl}/products`, {observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  getProduct(id: number) {
    return this.http.get<IProduct>(`${API_CONFIG.baseUrl}/products/${id}`);
  }

  getBrands(){
    return this.http.get<IBrand[]>(`${API_CONFIG.baseUrl}/products/brands`);
  }

  getTypes(){
    return this.http.get<IType[]>(`${API_CONFIG.baseUrl}/products/types`);
  }
  
}
