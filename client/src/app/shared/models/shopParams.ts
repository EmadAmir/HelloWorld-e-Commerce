export class ShopParams {
  brands: string[] = [];
  types: string[] = [];
  sort: string = 'name';
  pageNumber = 1;
  pageSize: number = 10;
  search = '';
}
