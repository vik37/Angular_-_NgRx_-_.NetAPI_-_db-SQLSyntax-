import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from  '@angular/forms';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { registerAction } from 'src/app/auth/store/actions';
import { AuthService } from 'src/app/auth/services/auth.service';
import { isSubmittingSelector } from '../../store/selectors';
import { CurrentUser } from 'src/app/shared/types/currentUser.interface';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {

  isSubmitting$: Observable<boolean>;
  form: FormGroup;
  users:CurrentUser[]=[];
  constructor(private fb: FormBuilder, private store: Store,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.initializeForm();
    this.initializeValues();
  }
  initializeValues():void{
    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
    console.log('isSubmitting$',this.isSubmitting$)
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
      console.log('on submit',this.form.value);
      this.store.dispatch(registerAction(this.form.value));
      this.authService.register(this.form.value);
  }

}
