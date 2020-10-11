import { Injectable } from '@angular/core';

import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable, pipe } from 'rxjs';
import { map, catchError, debounceTime } from 'rxjs/operators';

@Injectable()
export class DataContextService {

  constructor(private http: HttpClient) {

  }

  httpGet(url: string, parameters?: Object[], secure: boolean = true): Observable<any> {
     
    let observable: any;
    const headers = new HttpHeaders();
    this.addContentTypeHeader(headers, 1);
    this.addAcceptHeader(headers);
    this.addAccessControlHeader(headers);
    
    observable = this.http.get(url, { headers: headers }).pipe(map((response) => response));
    return observable;
  }

  httpPost(url: string, dataObject: any, fullUrl: boolean = false): Observable<any> {
    if (fullUrl === false) {
      url = `./${url}`;
    }
    let body = dataObject;
    const _headers = new HttpHeaders();
    _headers.append("Content-Type", "application/json");
    _headers.append("Access-Control-Allow-Origin", location.origin);
    const observable = this.http.post(url, body, { headers: _headers }).pipe(map(response => response));
    return observable;
  }

  private getHeaders() {
   

    let headers = new Headers();
    headers.append('Accept', 'application/json'); 
    return headers;
  }

  private handleError(error: any) {
    // log error
    var errorText = null;
    try {
      if (error != null) {
        errorText = error.statusText;
      }

      if (errorText == null || errorText == "") {
        errorText = error._body;
      }

      if (errorText == null || errorText == "") {
        errorText = 'There was a problem when performing your request. Please contact system administrator.';
      }
    } catch (e) {

    }

    return Promise.reject(errorText);
  }

   

  addSecurityHeader(headers: any): any {
    var token = JSON.parse(localStorage.getItem('currentUser')).token;
    headers.append('Authorization', `Bearer ${token}`);
    return headers;
  }

  addContentTypeHeader(headers: any, type: number = 0) {
    headers.append('Content-Type', (type === 1 ? 'application/x-www-form-urlencoded' : 'application/json'));
    return headers;
  }

  addAcceptHeader(headers, type: number = 0) {
    headers.append('Accept', (type === 2 ? '*/*' : (type === 1 ? 'application/xml' : 'application/json')));
  }

  composeUrl(url: string): string {
    url = url.indexOf('http') === 0 ? url : `./${url}`;
    return url.replace(/(http?:\/\/)|(\/)+/g, "$1$2");
  }

  addAccessControlHeader(headers: any): any {
    headers.append('Access-Control-Allow-Origin', this.composeUrl(`${location.origin}/${location.pathname}`));
    return headers;
  }
}
