import { HttpClient } from '@angular/common/http';
import { Component, Inject, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import {
  MatListOption,
  MatSelectionList,
  MatSelectionListChange,
} from '@angular/material/list';
import { ShopParams } from '../../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    MatCard,
    MatButton,
    ProductItemComponent,
    MatIcon,
    MatMenuTrigger,
    MatMenu,
    MatSelectionList,
    MatListOption,
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  private dialogService = inject(MatDialog);
  private shopService = inject(ShopService);
  title = 'client';
  // selectedBrands: string[] = [];
  // selectedTypes: string[] = [];
  // selectedSort: string = 'name';
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low-High', value: 'priceAsc' },
    { name: 'Price: High-Low', value: 'priceDesc' },
  ];

  shopParams = new ShopParams();

  products: Product[] = [];

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProducts();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (repsonse) => (this.products = repsonse.data),
      error: (error) => console.log(error),
    });
  }

  onSortChange(event: MatSelectionListChange) {
    const selectionOption = event.options[0];

    if (selectionOption) {
      this.shopParams.sort = selectionOption.value;
      //console.log(this.selectedSort);
      this.getProducts();
    }
  }

  openFilterDialog() {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types,
      },
    });

    // console.log(this.selectedBrands);

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        console.log(result);
        this.shopParams.brands = result.selectedBrands;
        this.shopParams.types = result.selectedTypes;
        this.getProducts();
      }
    });
  }
}
