import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_CONFIG } from 'src/config/api.config';
import { IPagination, Pagination } from 'src/app/shared/models/pagination';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/product-type';
import { map, of } from 'rxjs';
import { ShopParams } from '../shared/models/shop-params';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  products: IProduct[] = [];
  brands: IBrand[] = [];
  types: IType[] = [];
  pagination = new Pagination();
  shopParams = new ShopParams();
  productCache = new Map();

  constructor(private http: HttpClient) { }

  getProducts(useCache: boolean) {
    if (useCache === false) {
      this.productCache = new Map();
    }

    if (this.productCache.size > 0 && useCache === true) {
      if (this.productCache.has(Object.values(this.shopParams).join('-'))) {
        this.pagination.data = this.productCache.get(Object.values(this.shopParams).join('-'));
        return of(this.pagination);
      }
    }

    let params = new HttpParams();

    if(this.shopParams.brandId !== 0){
      params = params.append('brandId', this.shopParams.brandId.toString());
    }

    if(this.shopParams.typeId !== 0){
      params = params.append('typeId', this.shopParams.typeId.toString());
    }

    if (this.shopParams.search) {
      params = params.append('search', this.shopParams.search);
    }

    params = params.append('sort', this.shopParams.sort);
    params = params.append('pageIndex', this.shopParams.pageIndex.toString());
    params = params.append('pageSize', this.shopParams.pageSize.toString());

    return this.http.get<IPagination>(`${API_CONFIG.baseUrl}/products`, {observe: 'response', params})
      .pipe(
        map(response => {
          this.productCache.set(Object.values(this.shopParams).join('-'), response.body.data);
          this.pagination = response.body;
          return this.pagination;
        })
      );
  }

  setShopParams(shopParams: ShopParams) {
    this.shopParams = shopParams;
  }

  getShopParams() {
    return this.shopParams;
  }

  getProduct(id: number) {
    let product: IProduct;
    this.productCache.forEach((products: IProduct[]) => {
      product = products.find(p => p.id === id);
    })

    if (product != null) {
      return of(product);
    }

    return this.http.get<IProduct>(`${API_CONFIG.baseUrl}/products/${id}`); 
  }

  getBrands() {
    if(this.brands.length > 0) {
      return of(this.brands);
    }

    return this.http.get<IBrand[]>(`${API_CONFIG.baseUrl}/products/brands`)
      .pipe(
        map(response => {
          this.brands = response;
          return response;
        })
      )
  }

  getTypes() {
    if(this.types.length > 0) {
      return of(this.types);
    }

    return this.http.get<IType[]>(`${API_CONFIG.baseUrl}/products/types`)
      .pipe(
        map(response => {
          this.types = response;
          return response;
        })
      );
  }
  
}
