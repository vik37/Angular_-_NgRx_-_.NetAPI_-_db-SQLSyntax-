import { Component, Input, OnInit } from '@angular/core';
import { BackendErrors } from 'src/app/shared/types/backendErrors.interface';

@Component({
  selector: 'app-backend-error-messages',
  templateUrl: './backend-error-messages.component.html'
})
export class BackendErrorMessagesComponent implements OnInit {
  @Input('backendErrors')backendErrorsProps: BackendErrors

  errorMessages:string[]
  constructor() { }

  ngOnInit(): void {
    this.errorMessages = Object.keys(this.backendErrorsProps).map((name:string)=>{
      const messages = this.backendErrorsProps[name].join(' ')
      return `${name} ${messages}`
    })
  }

}
