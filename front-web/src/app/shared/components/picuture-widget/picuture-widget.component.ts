import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { base64ToFile, ImageCroppedEvent } from 'ngx-image-cropper';

@Component({
  selector: 'app-picuture-widget',
  templateUrl: './picuture-widget.component.html',
  styleUrls: ['./picuture-widget.component.scss']
})
export class PicutureWidgetComponent implements OnInit {

  @Output() addFile = new EventEmitter();

  files: File[] = [];
  imageChangedEvent: any = '';
  croppedImage: any = '';

  constructor() { }

  ngOnInit(): void {
  }

  onSelect(event) {
    this.files = [];
    this.files.push(...event.addedFiles);
    this.fileChangeEvent(this.files[0]);
  }

  onUpload() {
    console.log(base64ToFile(this.croppedImage));
    this.addFile.emit(base64ToFile(this.croppedImage));
  }

  fileChangeEvent(event: any) : void {
    this.imageChangedEvent = event;
  }

  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
  }

}
