import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryImageSize, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { IProduct } from 'src/app/shared/models/product';
import { ShoppingCartService } from 'src/app/shopping-cart/shopping-cart.service';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product: IProduct;
  quantity = 1;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute, private bcService: BreadcrumbService, 
              private shoppingCartService: ShoppingCartService) {
    this.bcService.set('@product-details', ' ');
   }

  ngOnInit(): void {
    this.loadProdcut();
  }
  
  initializeGallery() {
    this.galleryOptions = [
      {
        width: '500px',
        height: '600px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Fade,
        imageSize: NgxGalleryImageSize.Contain,
        thumbnailSize: NgxGalleryImageSize.Contain,
        preview: false
      }
    ];

    this.galleryImages = this.getImages();
  }

  addItemToShoppingCart(){
    this.shoppingCartService.addItem(this.product, this.quantity);
  }

  loadProdcut() {
    this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe({
      next: (response: IProduct) => {
        this.product = response;
        this.bcService.set('@product-details', this.product.name);
        this.initializeGallery();
      },
      error: (error) => console.log(error)
    });
  }

  getImages() {
    const imageUrls = [];

    for (const picture of this.product.pictures) {
      imageUrls.push({
        small: picture.pictureUrl,
        medium: picture.pictureUrl,
        big: picture.pictureUrl,
      });
    }

    return imageUrls;
  }

  incrementQuantity(){
    this.quantity++;
  }

  decrementQuantity(){
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

}
