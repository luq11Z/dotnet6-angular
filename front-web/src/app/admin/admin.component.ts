import { Component, OnInit } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { ShopParams } from '../shared/models/shop-params';
import { ShopService } from '../shop/shop.service';
import { AdminService } from './admin.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  products: IProduct[];
  totalCount: number;
  shopParams: ShopParams;

  constructor(private shopService: ShopService, private adminService: AdminService) {
    this.shopParams = this.shopService.getShopParams();
   }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts(useCache = false) {
    this.shopService.getProducts(useCache).subscribe({
      next: (response: IPagination) => {
        this.products = response.data;
        this.totalCount = response.count;
      },
      error: (error) => console.log(error)
    })
  }

  deleteProduct(id: number) {
    this.adminService.deleteProduct(id).subscribe({
      next: () => {
        this.products.splice(this.products.findIndex(p => p.id === id), 1);
        this.totalCount--;
      }
    })
  }

  onPageChanged(event: any) {
    const params = this.shopService.getShopParams();

    if (this.shopParams.pageIndex !== event) {
      params.pageIndex = event;
      this.shopService.setShopParams(params);
      this.getProducts(true);
    }
  }

}
