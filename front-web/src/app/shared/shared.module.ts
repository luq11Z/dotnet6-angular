import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { TabsModule} from 'ngx-bootstrap/tabs';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { StepperComponent } from './components/stepper/stepper.component';
import { ShoppingCartSummaryComponent } from './components/shopping-cart-summary/shopping-cart-summary.component';
import { RouterModule } from '@angular/router';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { PicutureWidgetComponent } from './components/picuture-widget/picuture-widget.component';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { ImageCropperModule } from 'ngx-image-cropper';

 
@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    TextInputComponent,
    StepperComponent,
    ShoppingCartSummaryComponent,
    PicutureWidgetComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ReactiveFormsModule,
    FormsModule,
    CdkStepperModule,
    RouterModule,
    CurrencyMaskModule,
    NgxGalleryModule,
    NgxDropzoneModule,
    ImageCropperModule
  ],
  exports: [
    PaginationModule,
    CarouselModule,
    BsDropdownModule,
    ReactiveFormsModule,
    FormsModule,
    CdkStepperModule,
    CurrencyMaskModule,
    NgxGalleryModule,
    TabsModule,
    NgxDropzoneModule,
    ImageCropperModule,
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    TextInputComponent,
    StepperComponent,
    ShoppingCartSummaryComponent,
    PicutureWidgetComponent
  ]
})
export class SharedModule { }
