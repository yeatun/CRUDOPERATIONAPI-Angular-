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
    emailId: '',
    companyName: ''
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
    console.log(this.contact);
  }
}
