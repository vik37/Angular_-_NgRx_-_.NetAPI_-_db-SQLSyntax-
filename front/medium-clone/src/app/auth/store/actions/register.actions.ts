import { createAction, props } from '@ngrx/store';
import { ActionTypes } from 'src/app/auth/store/export-types';
import {RegisterRequestInterface} from 'src/app/shared/types/register.interface';
import {CurrentUser} from 'src/app/shared/types/currentUser.interface';
import { BackendErrors } from 'src/app/shared/types/backendErrors.interface';

export const registerAction = createAction(
  ActionTypes.REGISTER,
  props<{request:RegisterRequestInterface}>()
);

export const registerSuccessAction = createAction(
  ActionTypes.REGISTER_SUCCESS,
  props<{currentUser:CurrentUser}>()
);

export const registerFailedAction = createAction(
    ActionTypes.REGISTER_FAILURE,
    props<{errors: BackendErrors}>()
);
