import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { OrderDetailsComponent } from './components/order-details/order-details.component'
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { StepperComponent } from './components/stepper/stepper.component';
import { ShoppingCartSummaryComponent } from './components/shopping-cart-summary/shopping-cart-summary.component';
import { RouterModule } from '@angular/router';

 
@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    OrderDetailsComponent,
    TextInputComponent,
    StepperComponent,
    ShoppingCartSummaryComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    ReactiveFormsModule,
    CdkStepperModule,
    RouterModule
  ],
  exports: [
    PaginationModule,
    CarouselModule,
    BsDropdownModule,
    ReactiveFormsModule,
    CdkStepperModule,
    PagingHeaderComponent,
    PagerComponent,
    OrderDetailsComponent,
    TextInputComponent,
    StepperComponent,
    ShoppingCartSummaryComponent
  ]
})
export class SharedModule { }
