import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';


export class HttpService {
  _baseUrlWithPath: string;
  // _http: HttpClient;
  constructor(
    private http: HttpClient,
    baseUrlWithPath: string) {
    this._baseUrlWithPath = baseUrlWithPath;
    // this.http = http;
  }

  public post<Type>(requestUrlMethod: string, requestFormData: FormData): Observable<Type> {
    let requestUrl = this._baseUrlWithPath + requestUrlMethod;
    return this.http.post<Type>(requestUrl, requestFormData)
      .pipe(
        catchError(this.handleError)
      );
  }

  public get<Type>(requestUrlMethod: string): Observable<Type> {
    let requestUrl = this._baseUrlWithPath + requestUrlMethod;
    return this.http.get<Type>(requestUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse): Observable<any> {
    if (error.status === 401) {
      window.location.href = '/authentication/login';

    } else {
      console.error('An error occurred:', error.error.message);
    }
    // return an observable with a user-facing error message
    const badResponse: any = { isSuccessful: false };
    return of(badResponse);
    // return throwError(
    //   'Something bad happened; please try again later.');
  }
}
