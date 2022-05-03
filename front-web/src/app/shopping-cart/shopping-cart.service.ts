import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { API_CONFIG } from 'src/config/api.config';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IProduct } from '../shared/models/product';
import { IShoppingCart, IShoppingCartItem, IShoppingCartTotal, ShoppingCart } from '../shared/models/shopping-cart';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {

  private shoppingCartSource = new BehaviorSubject<IShoppingCart>(null);
  shoppingCart$ = this.shoppingCartSource.asObservable();

  private shoppingCartTotalSource = new BehaviorSubject<IShoppingCartTotal>(null);
  shoppingCartTotal$ = this.shoppingCartTotalSource.asObservable();

  shipping = 0;

  constructor(private http: HttpClient) { }

  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    this.shipping = deliveryMethod.price;
    const shoppingCart = this.getCurrentShoppingCartValue();
    shoppingCart.deliveryMethodId = deliveryMethod.id;
    shoppingCart.shippingPrice = deliveryMethod.price;
    this.calcualteTotal();
    this.setShoppingCart(shoppingCart);
  }

  getShoppingCart(id: string) {
    return this.http.get(`${API_CONFIG.baseUrl}/shoppingcart?id=${id}`)
      .pipe(
        map((shoppingCart: IShoppingCart) => {
          this.shoppingCartSource.next(shoppingCart);
          this.shipping = shoppingCart.shippingPrice;
          this.calcualteTotal();
        })
      )
  }

  setShoppingCart(shoppingCart: IShoppingCart) {
    return this.http.post(`${API_CONFIG.baseUrl}/shoppingcart`, shoppingCart).subscribe({
      next: (response: IShoppingCart) => {
        this.shoppingCartSource.next(response);
        this.calcualteTotal();
      },
      error: (erorr) => console.log(erorr)
    });
  }

  getCurrentShoppingCartValue() {
    return this.shoppingCartSource.value; 
  }

  addItem(item: IProduct, quantity = 1) {
    const itemToAdd: IShoppingCartItem = this.mapProductItemToShoppingCartItem(item, quantity);
    const shoppingCart = this.getCurrentShoppingCartValue() ?? this.createShoppingCart();
    shoppingCart.items = this.addOrUpdateItem(shoppingCart.items, itemToAdd, quantity);
    this.setShoppingCart(shoppingCart);
  }

  incrementItemQuantity(item: IShoppingCartItem) {
    const shopingCart = this.getCurrentShoppingCartValue();
    const foundItemIndex = shopingCart.items.findIndex(x => x.id === item.id);
    shopingCart.items[foundItemIndex].quantity++;
    this.setShoppingCart(shopingCart);
  }

  decrementItemQuantity(item: IShoppingCartItem) {
    const shopingCart = this.getCurrentShoppingCartValue();
    const foundItemIndex = shopingCart.items.findIndex(x => x.id === item.id);

    if (shopingCart.items[foundItemIndex].quantity > 1) {
      shopingCart.items[foundItemIndex].quantity--;
    } else {
      this.removeItemFromShoppingCart(item);
    }

    this.setShoppingCart(shopingCart);
  }

  removeItemFromShoppingCart(item: IShoppingCartItem) {
    const shoppingCart = this.getCurrentShoppingCartValue();

    if(shoppingCart.items.some(x => x.id === item.id)) {
      shoppingCart.items = shoppingCart.items.filter(x => x.id !== item.id);

      if (shoppingCart.items.length > 0) {
        this.setShoppingCart(shoppingCart);
      } else {
        this.deleteShoppingCart(shoppingCart);
      }
    }

  }

  deleteLocalShoppingCart(id: string) {
    this.shoppingCartSource.next(null);
    this.shoppingCartTotalSource.next(null);
    localStorage.removeItem('shoppingCart_id');
  }

  deleteShoppingCart(shoppingCart: IShoppingCart) {
    return this.http.delete(`${API_CONFIG.baseUrl}/shoppingcart?id=${shoppingCart.id}`).subscribe({
      next: () => {
        this.shoppingCartSource.next(null);
        this.shoppingCartTotalSource.next(null);
        localStorage.removeItem('shoppingCart_id');
      },
      error: (error) => console.log(error)
    });
  }

  private addOrUpdateItem(items: IShoppingCartItem[], itemToAdd: IShoppingCartItem, quantity: number): IShoppingCartItem[] {
    const index = items.findIndex(i => i.id === itemToAdd.id);

    if (index === -1) {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    } else {
      items[index].quantity += quantity;
    }

    return items;
  }

  private createShoppingCart(): IShoppingCart {
    const shoppingCart = new ShoppingCart();
    localStorage.setItem('shoppingCart_id', shoppingCart.id);

    return shoppingCart;
  }

  private calcualteTotal(){
    const shopingCart = this.getCurrentShoppingCartValue();
    const shipping = this.shipping;
    const subtotal = shopingCart.items.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const total = subtotal + shipping;
    this.shoppingCartTotalSource.next({shipping, total, subtotal});
  }

  createPaymentIntent() {
    return this.http.post(`${API_CONFIG.baseUrl}/payments/${this.getCurrentShoppingCartValue().id}`, {})
      .pipe(
        map((shoppingCart: IShoppingCart) => {
          this.shoppingCartSource.next(shoppingCart);
        })
      )
  }
  
  private mapProductItemToShoppingCartItem(item: IProduct, quantity: number): IShoppingCartItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type: item.productType
    }
  }

}
