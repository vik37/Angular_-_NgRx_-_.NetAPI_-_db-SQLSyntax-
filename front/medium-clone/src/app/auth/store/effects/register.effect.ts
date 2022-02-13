import { Injectable } from "@angular/core";
import {createEffect, Actions, ofType} from "@ngrx/effects";
import { registerAction, registerFailedAction, registerSuccessAction } from "src/app/auth/store/actions/register.actions";
import {catchError, map, switchMap} from 'rxjs/operators';
import { AuthService } from "src/app/auth/services/auth.service";
import { CurrentUser } from "src/app/shared/types/currentUser.interface";
import { of } from "rxjs";

@Injectable()
export class RegisterEffect{
  register$ = createEffect(()=>
    this.action$.pipe(
      ofType(registerAction),
      switchMap(({request})=>{
        return this.authService.register(request).pipe(
          map((currentUser: CurrentUser)=>{
            console.log('effects', currentUser);
            return registerSuccessAction({currentUser})
          }),
          catchError(()=>{
            return of(registerFailedAction())
          })
        )
      })
    )
  )
  /*register$ = createEffect(()=> this.action$.pipe(
    ofType(registerAction),
    switchMap(({request})=>{
        return this.authService.register(request).pipe(
          map((currentUser?: CurrentUser)=>{
            let cru = {cur:currentUser};
            return registerSuccessAction({currentUser})
          }),
          catchError(()=>{
            return of(registerFailedAction())
          })
        )
      })
    )
  )*/

  constructor(private action$: Actions, private authService: AuthService){}
}
