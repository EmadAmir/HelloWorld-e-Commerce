import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { HttpClient } from '@angular/common/http';
import { NgFor } from '@angular/common';
import { ShopService } from './core/services/shop.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  private shopServices = inject(ShopService);
  title = 'client';

  products: any[] = [];

  ngOnInit(): void {
    this.shopServices.getProducts().subscribe({
      next: (repsonse) => {
        this.products = repsonse;
        console.log(this.products);
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {
        console.log('Request Completed!!');
      },
    });
  }
}
