import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RestService {

  constructor(
    @Inject('BASE_URL') private _baseUrl: string,
    private _httpClient: HttpClient
  ) {
  }

  /**
   * Send a HTTP-GET Request
   * @param endpoint
   * @param parameters
   */
  get<T>(endpoint: string, parameters: { [key: string]: any } = null): Observable<T> {
    const url = this.getCompleteEndpointUrl(endpoint, parameters);

    return Observable.create((observer: Observer<T>) => {
      this._httpClient.get<T>(url)
        .subscribe((response: any) => {
          observer.next(response.result);
          observer.complete();
        }, error => {
          observer.error(error);
        });
    });
  }

  /**
   * Send a HTTP-POST Request
   * @param endpoint
   * @param payload
   */
  post<T>(endpoint: string, payload: any, parameters: { [key: string]: any } = null): Observable<T> {
    const url = this.getCompleteEndpointUrl(endpoint, parameters);

    return Observable.create((observer: Observer<T>) => {
      this._httpClient.post<T>(url, payload)
        .subscribe((response: any) => {
          observer.next(response.result);
          observer.complete();
        }, error => {
          observer.error(error);
        });
    });
  }

  private getCompleteEndpointUrl(endpoint, parameters?: { [key: string]: any }) {

    if (parameters) {
      for (let paramName in parameters) {
        const parameterValue = parameters[paramName];

        endpoint = endpoint.replaceAll(`{${paramName}}`, encodeURIComponent(parameterValue));
      }
    }

    endpoint = this._baseUrl + endpoint;

    return endpoint;
  }
}
