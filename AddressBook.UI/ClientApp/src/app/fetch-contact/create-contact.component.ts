import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { NgForm, FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable, throwError } from 'rxjs';
//import { map } from 'rxjs-compat/operator/map';
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';
import { Contact } from '../models/contact.model'
import { identifierModuleUrl, ElementSchemaRegistry } from '@angular/compiler';
import { ContactService } from './contact.service'
import { error } from 'protractor';
import { log } from 'util';
import { Router } from '@angular/router';


@Component({
  selector: 'app-new-contact',
  templateUrl: './create-contact.component.html'
})
export class CreateContactComponent implements OnInit {
  myAppUrl: string = "";
  nameOfVerbAction: string;


  // the contatc object for edit or create
  _contacts: Contact = {
    id: 0,
    firstName: "",
    lastName: "",
    address: "",
    telephoneNumber: "",
  };

  // the form Model
  form: FormGroup;

  telephoneInUse: number;

  // will use mgModel
  _contact: Contact = {
    id: null,
    firstName: null,
    lastName: null,
    address: null,
    telephoneNumber: null,
  };



  constructor(private contactservice: ContactService, private _http: HttpClient, @Inject('BASE_URL') baseUrl: string, private _router: Router) {
    this.myAppUrl = baseUrl;

  }


  loadAllContacts() {
    this.contactservice.getAllContacts()
      .subscribe(countries => {
        this.contactservice._globalContactList = countries as Contact[]
      })
  }


  ngOnInit() {
    if (this.contactservice._globalContactIdForExecuteVerb && this.contactservice._globalContactIdForExecuteVerb != "0") {
      this._contact = this.contactservice.getContactBySelectedID(Number(this.contactservice._globalContactIdForExecuteVerb));
    }
    else {
      this._contact = this._contacts;
    }
    this.nameOfVerbAction = this.contactservice._globalNameOfVerb;


    this.form = new FormGroup(
      {
        name: new FormControl('', Validators.required),
        surname: new FormControl('', Validators.required),
        address: new FormControl('', Validators.required),
        phone: new FormControl('', Validators.required)
      },
    );


  }

  doSmtg() {

    console.log("BINGO");
  }


  addContact(contactForm: NgForm): void {


    this._http.get<Contact[]>(this.myAppUrl + 'api/ContactsEF?telephoneNumber=' + this._contact.telephoneNumber).subscribe(result => {
      this.telephoneInUse = result.length;

      if (this.telephoneInUse == 0 && this._contact.id == 0) {
        this.contactservice.addNewContact(this._contact).subscribe(
          (data: Contact) => {
            console.log("It was executed add/new contact:");
            console.log(data);
            this._router.navigate(['fetch-data-contact']);
          },
          (error: any) => { console.log(error) }
        );
      }
      else if (this.telephoneInUse == 1 && this._contact.id != 0) {
        this.contactservice.updateContact(this._contact).subscribe(
          () => {
            console.log("It was executed an update by ID:" + `${this._contact.id}`);
            this._router.navigate(['fetch-data-contact']);
          },
          (error: any) => { console.log(error) }
        );

      }
      else
        this._router.navigate(['/fetch-data-contact']);
     
    }, error => console.error(error));

    //if (this.telephoneInUse > 0) {
    //  console.log("telephone number is ready in use !!");
    //}
    //else {


    //}
  }


  //addNewContact(contactData: Contact): Observable<Contact> {   
  //    return this._http.post<Contact>(this.myAppUrl + 'api/ContactsEF', contactData, {
  //      headers: new HttpHeaders({
  //        'Content-Type': 'application/json'
  //      })
  //    }).pipe(catchError(this.handleError));
  //}


  //saveContact(contactForm: NgForm): Observable < Contact > {
  //  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  //  return this._http.post<Contact>(this.myAppUrl + '/api/ContactsEF/CreateNewContact/',
  //    contactForm.value, httpOptions);
  //}


  //RunSaveContact(contact: Contact) {
  //  if (this.employeeIdUpdate == null) {
  //    this.employeeService.createEmployee(employee).subscribe(
  //      () => {
  //        this.dataSaved = true;
  //        this.massage = 'Record saved Successfully';
  //        this.loadAllEmployees();
  //        this.employeeIdUpdate = null;
  //        this.employeeForm.reset();
  //      }
  //    );
  //  } else {
  //    employee.EmpId = this.employeeIdUpdate;
  //    this.employeeService.updateEmployee(employee).subscribe(() => {
  //      this.dataSaved = true;
  //      this.massage = 'Record Updated Successfully';
  //      this.loadAllEmployees();
  //      this.employeeIdUpdate = null;
  //      this.employeeForm.reset();
  //    });
  //  }
  //}





  ///** PUT: update the hero on the server. Returns the updated hero upon success. */
  //saveContact(contactForm: NgForm): Observable<Contact> {
  //  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  //  return this._http.put<Contact>(this.myAppUrl + '/api/ContactsEF', contactForm, httpOptions)
  //  //.pipe(
  //  //  catchError(this.errorHandler())
  //  //);
  //}


  //myAppUrl: string = "";
  //public contacts: Contact[];

  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //  http.get<Contact[]>(baseUrl + 'contact').subscribe(result => {
  //    this.contacts = result;
  //  }, error => console.error(error));
  //}

  //saveEmployee(employee) {
  //  return this._http.post(this.myAppUrl + 'api/Employee/Create', employee)
  //    .map((response: Response) => response.json())
  //    .catch(this.errorHandler)
  //}





  //ngOnInit() {    prej je bil tole aktiviran !!

  //}



  //saveEmployee(contactForm: NgForm) {
  //  return this._http.post(this.myAppUrl + 'api/ContactsEF/Create', contactForm.value)
  //    .map((response: Response) => response.json())

  //}


  //saveContact(contactForm: NgForm) {
  //  return this._http.post(this.myAppUrl + 'api/ContactsEF', contactForm.value).pipe(
  //    map((response: Response) => response.json()))

  //}




  //saveContact(contactForm: NgForm): Observable<Contact> {
  //  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  //  return this._http.post<Contact>(this.myAppUrl + '/api/ContactsEF/',
  //    contactForm.value, httpOptions);
  //}

  //saveContact(newContact: Contact): void {
  //  console.log(newContact);
  //}


  handleError(errorMsg: Response) {
    console.log(errorMsg);
    return throwError(errorMsg);
  }


}






