import { createReducer,on,Action } from "@ngrx/store";

import { AuthStateInterface } from "src/app/shared/types/authState.interface";
import { registerAction } from "src/app/auth/store/actions";

const initialState:AuthStateInterface = {
  isSubmiting:false
}

const authReducer = createReducer(
  initialState,
  on(registerAction, (state): AuthStateInterface=>({
   ...state,
   isSubmiting:true
})))

export function reducers(state: AuthStateInterface, action: Action){
  return authReducer(state,action);
}
