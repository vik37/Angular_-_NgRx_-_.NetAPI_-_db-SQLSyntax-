import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CurrentUser } from 'src/app/shared/types/currentUser.interface';
import {RegisterRequestInterface} from 'src/app/shared/types/register.interface';
import { AuthResponseInterface} from 'src/app/shared/types/authResponse.interface';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  url:string = environment.url + "user";
  constructor(private client: HttpClient) { }

  register(data: RegisterRequestInterface) : Observable<CurrentUser>{
      console.log("service data",data);
      console.log("service url", this.url);
      return this.client.post<AuthResponseInterface>(this.url,data).pipe(map(
        (response: AuthResponseInterface)=> response.user,
      ))
  }
}
