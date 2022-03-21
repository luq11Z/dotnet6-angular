import { v4 as uuidv4 } from 'uuid';

export interface IShoppingCart {
    id: string;
    items: IShoppingCartItem[];
}

export interface IShoppingCartItem {
    id: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}

export interface IShoppingCartTotal {
    shipping: number;
    subtotal: number;
    total: number;
}

export class ShoppingCart implements IShoppingCart {
    id = uuidv4();
    items: IShoppingCartItem[] = [];
}