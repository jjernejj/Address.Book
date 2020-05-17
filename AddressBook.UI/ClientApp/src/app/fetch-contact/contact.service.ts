import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs'
import { Contact } from "../models/contact.model";
import { catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})

export class ContactService {

  private contact: Contact[];
  url = '"https://localhost:44304/"';
  appUrl: string = "";

  _globalContactIdForExecuteVerb: string;
  _globalContactList: Contact[];
  _globalNameOfVerb: string;
  _globalPagination: number = 1;



  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.appUrl = baseUrl;
  }

  handleError(errorMsg: Response) {
    console.log(errorMsg);
    return throwError(errorMsg);
  }


  addNewContact(contactData: Contact): Observable<Contact> {
    return this.http.post<Contact>(this.appUrl + 'api/ContactsEF', contactData, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.handleError));
  }


  updateContact(contactData: Contact): Observable<void> {
    return this.http.put<void>(this.appUrl + 'api/ContactsEF' + `/${contactData.id}`, contactData, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.handleError));
  }



  getContactBySelectedID(id: number): Contact {

    return this._globalContactList.find(e => e.id == id);
  }



  getAllContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.url + 'contact');
  }


  getContactById(contactData: Contact): Observable<Contact> {
    return this.http.get<Contact>(this.url + 'api/ContactsEF' + `/${contactData.id}`);
  }


  deleteContact(contactdId: number): Observable<number> {
    return this.http.delete<number>(this.appUrl + 'api/ContactsEF/' + contactdId, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.handleError));
  }





  createEmployee(employee: Contact): Observable<Contact> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post<Contact>(this.url + '/CreateNewContact/',
      employee, httpOptions);
  }
  updateEmployee(employee: Contact): Observable<Contact> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put<Contact>(this.url + '/UpdateEmployeeDetails/',
      employee, httpOptions);
  }
  deleteEmployeeById(employeeid: string): Observable<number> {
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete<number>(this.url + '/DeleteEmployeeDetails?id=' + employeeid,
      httpOptions);
  }

}
