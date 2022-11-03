import { Component, OnInit } from '@angular/core';
import { Contact } from 'src/app/models/contact.model';
import { ContactsService } from 'src/app/service/contacts.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

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
