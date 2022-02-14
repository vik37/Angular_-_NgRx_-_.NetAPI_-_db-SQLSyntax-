import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from  '@angular/forms';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { registerAction } from 'src/app/auth/store/actions/register.actions';
import { AuthService } from 'src/app/auth/services/auth.service';
import { isSubmittingSelector, validationErrorsSelector } from 'src/app/auth/store/selectors';
import { RegisterRequestInterface } from 'src/app/shared/types/register.interface';
import { BackendErrors } from 'src/app/shared/types/backendErrors.interface';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {

  isSubmitting$: Observable<boolean>;
  backendErrors$: Observable<BackendErrors | null>
  form: FormGroup;
  constructor(private fb: FormBuilder, private store: Store,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.initializeForm();
    this.initializeValues();
  }
  initializeValues():void{
    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
    console.log('isSubmitting$',this.isSubmitting$)
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelector))
  }
  initializeForm():void{
    console.log('initialize form');
    this.form = this.fb.group({
      username:['',Validators.required],
      email:['',Validators.required],
      password:['',Validators.required]
    })
  }
  onSubmit(): void{
      //console.log('on submit',this.form.value);
      const request: RegisterRequestInterface = {
        user: this.form.value
      }
      console.log('on submit request',request);
      this.store.dispatch(registerAction({request}));
  }

}
