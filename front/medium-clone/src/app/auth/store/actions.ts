import { createAction, props } from '@ngrx/store';
import { ActionTypes } from 'src/app/auth/store/export-types';
import {RegisterRequestInterface} from 'src/app/shared/types/register.interface';

export const registerAction = createAction(
  ActionTypes.REGISTER,
  props<{request:RegisterRequestInterface}>()
);
