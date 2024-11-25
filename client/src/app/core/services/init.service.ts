import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private cartService = inject(CartService);

  init() {
    const cart = localStorage.getItem('cart_id');
    const cart$ = cart ? this.cartService.getCart(cart) : of(null);
    return cart$;
  }
}