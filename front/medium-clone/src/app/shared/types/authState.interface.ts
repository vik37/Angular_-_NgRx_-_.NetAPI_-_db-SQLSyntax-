import { BackendErrors } from "./backendErrors.interface";
import { CurrentUser } from "./currentUser.interface";

export interface AuthStateInterface{
  isSubmiting:boolean
  currentUser: CurrentUser | null
  isLoggedin: boolean | null
  validationErrors: BackendErrors | null
}
