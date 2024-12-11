import { inject, Injectable } from '@angular/core';
import {
  loadStripe,
  Stripe,
  StripeAddressElement,
  StripeAddressElementOptions,
  StripeElements,
} from '@stripe/stripe-js';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CartService } from './cart.service';
import { Cart } from '../../shared/models/cart';
import { firstValueFrom, map } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class StripeService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  private cartService = inject(CartService);
  private accountService = inject(AccountService);
  private stripePromise: Promise<Stripe | null>;
  private elements?: StripeElements;
  private addressElement?: StripeAddressElement;

  constructor() {
    this.stripePromise = loadStripe(environment.stripePublicKey);
  }

  getStripeInstent() {
    return this.stripePromise;
  }

  async initializeElements() {
    if (!this.elements) {
      const stripe = await this.getStripeInstent();
      if (stripe) {
        const cart = await firstValueFrom(this.createOrUpdatePaymentInstant());
        this.elements = stripe.elements({
          clientSecret: cart.clientSecret,
          appearance: { labels: 'floating' },
        });
      } else {
        throw new Error('Stripe has not been loaded');
      }
    }
    return this.elements;
  }

  async createAddressElement() {
    if (!this.addressElement) {
      const elements = await this.initializeElements();
      if (elements) {
        const user  = this.accountService.currentUser();
        let defaultValues:StripeAddressElementOptions['defaultValues']= {};
        if(user){
          defaultValues.name = user.firstName + ' ' + user.lastName;
        }
        
        if(user?.address){
          defaultValues.address = {
            line1: user.address.link1,
            line2: user.address.link2,
            city: user.address.city,
            state: user.address.state,
            country: user.address.country,
            postal_code: user.address.postalCode,
          }
        }

        const options: StripeAddressElementOptions = {
          mode: 'shipping',
        };
        this.addressElement = elements.create("address",options);
      }else{
        throw new Error('Elements instance has not loaded');
      }
    }
    return this.addressElement;
  }

  createOrUpdatePaymentInstant() {
    const cart = this.cartService.cart();
    if (!cart) throw new Error('Problem with cart');

    return this.http.post<Cart>(this.baseUrl + 'payments/' + cart.id, {}).pipe(
      map((cart) => {
        this.cartService.cart.set(cart);
        return cart;
      })
    );
  }


desposeElements(){
 this.elements = undefined;
 this.addressElement= undefined
}
}