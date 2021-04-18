import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RestService {

  constructor(
    @Inject('BASE_URL') private _baseUrl: string,
    private _httpClient: HttpClient
  ) {
  }

  public get<T>(endpoint, parameters: { [key: string]: any } = null): Observable<T> {

    console.log('GET ' + endpoint, parameters);

    if (parameters) {
      for (let paramName in parameters) {
        const parameterValue = parameters[paramName];

        endpoint = endpoint.replaceAll(`{${paramName}}`, encodeURIComponent(parameterValue));
      }
    }

    console.log('GET ' + this._baseUrl + endpoint);

    return Observable.create((observer: Observer<T>) => {
      this._httpClient.get<T>(this._baseUrl + endpoint)
        .subscribe((response: any) => {
          
          observer.next(response.result);
          observer.complete();
        }, error => {
            observer.error(error);
        });
    });

    console.log('-----');
  }
}
