import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from './product-item/product-item.component';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [MatCard, ProductItemComponent],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  private shopServices = inject(ShopService);
  title = 'client';

  products: Product[] = [];

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    this.shopServices.getBrands();
    this.shopServices.getTypes();
    this.shopServices.getProducts().subscribe({
      next: (repsonse) => (this.products = repsonse.data),
      error: (error) => console.log(error),
      complete: () => console.log('Request Completed!!'),
    });
  }
}
