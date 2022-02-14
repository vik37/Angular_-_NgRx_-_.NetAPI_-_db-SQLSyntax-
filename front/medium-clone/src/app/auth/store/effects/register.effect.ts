import { Injectable } from "@angular/core";
import {createEffect, Actions, ofType} from "@ngrx/effects";
import { registerAction, registerFailedAction, registerSuccessAction } from "src/app/auth/store/actions/register.actions";
import {catchError, map, switchMap, tap} from 'rxjs/operators';
import { AuthService } from "src/app/auth/services/auth.service";
import { CurrentUser } from "src/app/shared/types/currentUser.interface";
import { of } from "rxjs";
import { HttpErrorResponse } from "@angular/common/http";
import { Router } from "@angular/router";
import { PersistanceService } from "src/app/shared/services/persistance.service";

@Injectable()
export class RegisterEffect{
  register$ = createEffect(()=>
    this.action$.pipe(
      ofType(registerAction),
      switchMap(({request})=>{
        console.log('switch map request',request)
        return this.authService.register(request).pipe(
          map((currentUser: CurrentUser)=>{
            this.persistanceService.set('accessToken',currentUser.token)
            console.log('effects', currentUser);
            return registerSuccessAction({currentUser})
          }),
          catchError((errorResponse: HttpErrorResponse)=>{
            return of(registerFailedAction({errors: errorResponse.error}))
          })
        )
      })
    )
  )

  redirectAfterSubmit = createEffect(
    ()=>
      this.action$.pipe(
        ofType(registerSuccessAction),
        tap(()=>{
          this.route.navigateByUrl('/')
        })
      ),
      {dispatch:false}
  )
  constructor(private action$: Actions, private authService: AuthService,
    private persistanceService:PersistanceService, private route: Router){}
}
