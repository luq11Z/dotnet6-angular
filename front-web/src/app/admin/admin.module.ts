import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { EditProductComponent } from './edit-product/edit-product.component';
import { SharedModule } from '../shared/shared.module';
import { AdminRoutingModule } from './admin-routing.module';
import { EditProductFormComponent } from './edit-product-form/edit-product-form.component';
import { EditProductPicturesComponent } from './edit-product-pictures/edit-product-pictures.component';




@NgModule({
  declarations: [
    AdminComponent,
    EditProductComponent,
    EditProductFormComponent,
    EditProductPicturesComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
