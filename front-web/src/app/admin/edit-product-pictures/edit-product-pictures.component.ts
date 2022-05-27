import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { IProduct } from 'src/app/shared/models/product';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-edit-product-pictures',
  templateUrl: './edit-product-pictures.component.html',
  styleUrls: ['./edit-product-pictures.component.scss']
})
export class EditProductPicturesComponent implements OnInit {

  @Input() product: IProduct;

  progress: number;
  addPictureMode: boolean = false;

  constructor(private adminService: AdminService, private toast: ToastrService) { }

  ngOnInit(): void {
  }

  addPictureModeToggle() {
    if (!this.addPictureMode) {
      this.addPictureMode = true;
    } else {
      this.addPictureMode = false;
    }
  }

  uploadImage(file: File) {
    this.adminService.uploadImage(file, this.product.id).subscribe({
      next: (response: HttpEvent<any>) => {
        switch (response.type) {
          case HttpEventType.UploadProgress:
            this.progress = Math.round(response.loaded / response.total * 100);
            break;
          case HttpEventType.Response:
            this.product = response.body;
            setTimeout(() => {
              this.progress = 0;
              this.addPictureMode = false;
            }, 1500);
        }
      },
      error: (error) => {
        if (error.errors) {
          this.toast.error(error.errors[0]);
        } else {
          this.toast.error('Problem uploading image');
        }

        this.progress = 0;
      }
    })
  }

  deletePicture(pictureId: number) {
    this.adminService.deleteProductPicture(pictureId, this.product.id).subscribe({
      next: (response) => {
        const pictureIndex = this.product.pictures.findIndex(x => x.id === pictureId);
        this.product.pictures.splice(pictureIndex, 1);
      },
      error: (error) => {
        this.toast.error('Problem deleting photo');
        console.log(error);
      }
    })
  }

  setMainPicture(pictureId: number) {
    this.adminService.setMainPicture(pictureId, this.product.id).subscribe({
      next: (response: IProduct) => this.product = response
    })
  }

}
