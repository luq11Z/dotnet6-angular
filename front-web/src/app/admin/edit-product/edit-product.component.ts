import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { IBrand } from 'src/app/shared/models/brand';
import { IProduct, ProductFormValues } from 'src/app/shared/models/product';
import { IType } from 'src/app/shared/models/product-type';
import { ShopService } from 'src/app/shop/shop.service';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {

  product: ProductFormValues;
  brands: IBrand[];
  types: IType[];

  constructor(private adminService: AdminService, 
              private shopService: ShopService, 
              private route: ActivatedRoute, 
              private router: Router,
              private toastr: ToastrService) {
    this.product = new ProductFormValues();
  }

  ngOnInit(): void {
    const brands = this.getBrands();
    const types = this.getTypes();

    forkJoin([types, brands]).subscribe({
      next: (results) => {
        this.types = results[0];
        this.brands = results[1];
      }, 
      error: (error) => {
        console.log(error)
      }
    });

    if (this.route.snapshot.url[0].path === 'edit-product') {
      this.loadProduct();
    }
  }

  loadProduct() {
    this.shopService.getProduct(+this.route.snapshot.paramMap.get('id')).subscribe({
      next: (response: IProduct) => {
        const productBrandId = this.brands && this.brands.find(x => x.name === response.productBrand).id;
        const productTypeId = this.types && this.types.find(x => x.name === response.productType).id;
        this.product = {...response, productBrandId, productTypeId};
      }, 
      error: (error) => console.log(error)
    })
  }

  getBrands() {
    return this.shopService.getBrands();
  }

  getTypes() {
    return this.shopService.getTypes();
  }

  updatePrice(event: any) {
    this.product.price = event;
  }

  onSubmit(product: ProductFormValues) {
    if (this.route.snapshot.url[0].path === 'edit-product') {
      const updatedProduct = {...this.product, ...product, price: +product.price};
      this.adminService.updateProduct(updatedProduct, +this.route.snapshot.paramMap.get('id')).subscribe({
        next: () => {
          this.toastr.success("Product updated successfully");
          this.router.navigate(['admin']);
        },
        error: () => this.toastr.error("Something went wrong")
      })
    } else {
      const newProduct = {...product, price: +product.price};
      this.adminService.createProduct(newProduct).subscribe({
        next: () => {
          this.toastr.success("Product created successfully");
          this.router.navigate(['admin']);
        },
        error: () => this.toastr.error("Something went wrong")
      })
    }
  }
 
}
