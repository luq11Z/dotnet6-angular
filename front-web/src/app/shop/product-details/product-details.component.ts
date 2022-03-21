import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShoppingCartService } from 'src/app/shopping-cart/shopping-cart.service';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product: IProduct;
  quantity = 1;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute, private bcService: BreadcrumbService, 
              private shoppingCartService: ShoppingCartService) {
    this.bcService.set('@product-details', ' ');
   }

  ngOnInit(): void {
    this.loadProdcut();
  }

  addItemToShoppingCart(){
    this.shoppingCartService.addItem(this.product, this.quantity);
  }

  incrementQuantity(){
    this.quantity++;
  }

  decrementQuantity(){
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  loadProdcut() {
    this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe({
      next: (response) => {
        this.product = response;
        this.bcService.set('@product-details', this.product.name)
      },
      error: (error) => console.log(error)
    });
  }

}
