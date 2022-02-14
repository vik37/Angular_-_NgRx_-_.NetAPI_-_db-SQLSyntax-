import { createReducer,on,Action } from "@ngrx/store";

import { AuthStateInterface } from "src/app/shared/types/authState.interface";
import { registerAction, registerFailedAction, registerSuccessAction } from "src/app/auth/store/actions/register.actions";
//import { Action } from "rxjs/internal/scheduler/Action";

const initialState:AuthStateInterface = {
  isSubmiting:false,
  currentUser: null,
  validationErrors:null,
  isLoggedin: null
}

const authReducer = createReducer(
  initialState,
  on(registerAction, (state): AuthStateInterface=>({
      ...state,
      isSubmiting:true,
      validationErrors:null
    })
  ),
  on(registerSuccessAction, (state, action): AuthStateInterface =>({
      ...state,
      isSubmiting:false,
      isLoggedin:true,
      currentUser:action.currentUser
    })
  ),
  on(registerFailedAction,(state,action):AuthStateInterface =>({
        ...state,
        isSubmiting:false,
        validationErrors: action.errors
    })
  )
)
export function reducers(state: AuthStateInterface, action: Action){
  return authReducer(state,action);
}
