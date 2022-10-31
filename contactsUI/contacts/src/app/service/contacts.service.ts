import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Contact } from '../models/contact.model';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {

  baseUrl = 'https://localhost:7135/api/contacts';

  constructor(private http: HttpClient) { }

  //Get all contacts
  getAllContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.baseUrl);
  }

  //Add a new contact
    addContact(contact: Contact) : Observable<Contact> {
      contact.id = '00000000-0000-0000-0000-000000000000';
      return this.http.post<Contact>(this.baseUrl, contact)
    }

    //Delete a contact
    deleteContact(id: string) : Observable<Contact> {
      return this.http.delete<Contact>(this.baseUrl + '/' + id)

    }
    updateContact(contact: Contact) : Observable<Contact>{
      return this.http.put<Contact>(this.baseUrl + '/' + contact.id, contact)
    }
}
