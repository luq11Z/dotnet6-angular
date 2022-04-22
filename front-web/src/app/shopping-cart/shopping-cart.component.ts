import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IShoppingCart, IShoppingCartItem, IShoppingCartTotal } from '../shared/models/shopping-cart';
import { ShoppingCartService } from './shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.scss']
})
export class ShoppingCartComponent implements OnInit {

  shoppingCart$: Observable<IShoppingCart>;
  shoppingCartTotals$: Observable<IShoppingCartTotal>;

  constructor(private shoppingCartServce: ShoppingCartService) { }

  ngOnInit(): void {
    this.shoppingCart$ = this.shoppingCartServce.shoppingCart$;
    this.shoppingCartTotals$ = this.shoppingCartServce.shoppingCartTotal$;
  }

  removeShoppingCartItem(item: IShoppingCartItem) {
    this.shoppingCartServce.removeItemFromShoppingCart(item);
  }

  incrementItemQuantity(item: IShoppingCartItem) {
    this.shoppingCartServce.incrementItemQuantity(item);
  }

  decrementItemQuantity(item: IShoppingCartItem) {
    this.shoppingCartServce.decrementItemQuantity(item);
  }

}
