import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Contact } from '../models/contact.model'
import { ContactService } from './contact.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-fetch-contact',
  templateUrl: './fetch-contact.component.html'
})

@Injectable({
  providedIn: 'root'
})

export class FetchContactComponent {
  public contacts: Contact[];
  public pagination: number;

  searchByFirstName: string = "";
  searchByLastName: string = "";
  searchByAddress: string = "";
  searchByTelephoneNumber: string = "";

  _http: HttpClient;
  _baseUrl: string = "";

  constructor(private contactservice: ContactService, http: HttpClient, @Inject('BASE_URL') baseUrl: string, private _router: Router) {
    this._http = http;
    this._baseUrl = baseUrl;

    if (this.searchByFirstName.length > 0 || this.searchByLastName.length > 0 || this.searchByAddress.length > 0 || this.searchByTelephoneNumber.length > 0) {
      console.log("Will be executed serach by fields");
    }
    else {
      http.get<Contact[]>(baseUrl + 'contact').subscribe(result => {
        this.contacts = result;
        this.contactservice._globalContactList = result;
      }, error => console.error(error));
    }


  }




  loadContactToEdit(contactId: string) {
    this.contactservice._globalNameOfVerb = "Update";
    this.contactservice._globalContactIdForExecuteVerb = contactId;
    console.log(contactId);
    this._router.navigate(['/create-new-contact', contactId]);
  }

  cleadModelForAddNewContact(contactId: string) {
    this.contactservice._globalNameOfVerb = "Add";
    this.contactservice._globalContactIdForExecuteVerb = "0";
    this._router.navigate(['/create-new-contact']);
  }


  deleteThisContact(contactId: string) {
    this.contactservice._globalContactIdForExecuteVerb = contactId;


    this.contactservice.deleteContact(Number(contactId)).subscribe(
      (data: number) => {
        console.log("It was executed an update by ID:" + `${Number(contactId)}`);
        this._router.navigate(['fetch-data-contact']);
      },
      (error: any) => { console.log(error) }
    );

    // remove from contacts table spcific item, that we removed/delete
    this.contacts.forEach((item, index) => {
      if (item.id == Number(contactId)) this.contacts.splice(index, 1);
    });
    console.log(contactId);
  }


  paginationBack() {
    this.contactservice._globalPagination--;

    this._http.get<Contact[]>(this._baseUrl + 'contact?pageNumber=' + this.contactservice._globalPagination).subscribe(result => {
      this.contacts = result;
      this.contactservice._globalContactList = result;
    }, error => console.error(error));
  }

  paginationNext() {
    this.contactservice._globalPagination++;

    this._http.get<Contact[]>(this._baseUrl + 'contact?pageNumber=' + this.contactservice._globalPagination).subscribe(result => {
      this.contacts = result;
      this.contactservice._globalContactList = result;
    }, error => console.error(error));
  }


  searchContact() {
    if (this.searchByFirstName.length > 0 || this.searchByLastName.length > 0 || this.searchByAddress.length > 0 || this.searchByTelephoneNumber.length > 0) {
      console.log(this.searchByFirstName);
      this._http.get<Contact[]>(this._baseUrl + 'api/ContactsEF?firstName=' + this.searchByFirstName + '&lastName=' + this.searchByLastName + '&address=' + this.searchByAddress + '&telephoneNumber=' + this.searchByTelephoneNumber).subscribe(result => {
        this.contacts = result;
        this.contactservice._globalContactList = result;
      }, error => console.error(error));
    }
    else {
      this._http.get<Contact[]>(this._baseUrl + 'contact').subscribe(result => {
        this.contacts = result;
        this.contactservice._globalContactList = result;
      }, error => console.error(error));
    }

  }




}
