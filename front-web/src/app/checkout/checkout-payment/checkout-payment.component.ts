import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IOrder } from 'src/app/shared/models/order';
import { IShoppingCart } from 'src/app/shared/models/shopping-cart';
import { ShoppingCartService } from 'src/app/shopping-cart/shopping-cart.service';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {

  @Input() checkoutForm: FormGroup;

  constructor(private shoppingCartService: ShoppingCartService, private checkoutService: CheckoutService, 
              private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
  }

  submitOrder() {
    const cart = this.shoppingCartService.getCurrentShoppingCartValue();
    const orderToCreate = this.getOrderToCreate(cart);

    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: (order: IOrder) => {
        this.toastr.success('Order created successfully');
        this.shoppingCartService.deleteLocalShoppingCart(cart.id);
        const navigationExtras: NavigationExtras = {state: order};
        this.router.navigate(['checkout/success'], navigationExtras)
      }, 
      error: (error) => {
        this.toastr.error('Something went wrong');
        console.log(error.message);
      }
    })
  }

  private getOrderToCreate(cart: IShoppingCart) {
    return {
      shoppingCartId: cart.id,
      deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shipToAddress: this.checkoutForm.get('addressForm').value 
    }; 
  }

}
