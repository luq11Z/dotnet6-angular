import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IShoppingCart } from 'src/app/shared/models/shopping-cart';
import { IUser } from 'src/app/shared/models/user';
import { ShoppingCartService } from 'src/app/shopping-cart/shopping-cart.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  shoppingCart$: Observable<IShoppingCart>;
  currentUser$: Observable<IUser>;

  constructor(private shoppingCartService: ShoppingCartService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.shoppingCart$ = this.shoppingCartService.shoppingCart$;
    this.currentUser$ = this.accountService.currentUser$;
  }

  logout() {
    this.accountService.logout();
  }

}
