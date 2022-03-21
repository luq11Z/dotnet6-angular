import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IShoppingCart } from 'src/app/shared/models/shopping-cart';
import { ShoppingCartService } from 'src/app/shopping-cart/shopping-cart.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  shoppingCart$: Observable<IShoppingCart>;

  constructor(private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
    this.shoppingCart$ = this.shoppingCartService.shoppingCart$;
  }

}
