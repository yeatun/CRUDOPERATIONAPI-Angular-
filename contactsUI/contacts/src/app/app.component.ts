import { Component, OnInit } from '@angular/core';
import { Contact } from './models/contact.model';
import { ContactsService } from './service/contacts.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'contacts';
  contacts: Contact[] = [];
  contact: Contact = {
    id: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    email: '',
    company: ''
  }

  constructor(private contactService: ContactsService){

  }

  ngOnInit(): void {
    this.getAllContacts();
  }

  getAllContacts(){
    this.contactService.getAllContacts()
    .subscribe(
      response => {
        this.contacts = response;
      }
    );
  }
  onSubmit () {

    if(this.contact.id === ''){
      this.contactService.addContact(this.contact)
      .subscribe(
        response => {
          this.getAllContacts();
          this.contact ={
            id: '',
            firstName: '',
            lastName: '',
            phoneNumber: '',
            email: '',
            company: ''
          }
        }
      );
    }
    else {
      this.updateContact(this.contact);
    }


  }

  onDelete(id: string){
    this.contactService.deleteContact(id)
    .subscribe(
      response => {
        this.getAllContacts();
      }
    );
  }

  populateForm(contact: Contact) {
    this.contact = contact;
  }

  updateContact(contact: Contact){
    this.contactService.updateContact(contact)
    .subscribe(
      response => {
        this.getAllContacts();
        this.contact ={
          id: '',
          firstName: '',
          lastName: '',
          phoneNumber: '',
          email: '',
          company: ''
        }
      }
    );
  }
}
