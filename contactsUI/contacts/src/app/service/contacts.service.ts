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
}
