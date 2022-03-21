import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ShoppingCartService } from 'src/app/shopping-cart/shopping-cart.service';
import { IShoppingCartTotal } from '../../models/shopping-cart';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit {

  shoppingCartTotal$: Observable<IShoppingCartTotal>;

  constructor(private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
    this.shoppingCartTotal$ = this.shoppingCartService.shoppingCartTotal$;
  }

}
