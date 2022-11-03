import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ValiadateForm from 'src/app/helpers/validateForm';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  type: string = "password"
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash"
  signupForm!: FormGroup;
  constructor(private fb: FormBuilder, private userservice: UserService, private router: Router) { }

  ngOnInit(): void {
    this.signupForm = this.fb.group({
      email: ['', Validators.required],
      username: ['', Validators.required],
      password:  ['', Validators.required]
    })
  }
  hideShowPass(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  onSignUp(){
    if(this.signupForm.valid){
      //action
      console.log(this.signupForm.value)
      this.userservice.signUp(this.signupForm.value)
      .subscribe({
        next:(res =>{
          alert(res.message)
          this.signupForm.reset();
          this.router.navigate(['login']);
        })
        ,error:(err =>{
          alert(err?.error.message)
        })
      })
    }
    else{
      ValiadateForm.validateAllFormFields(this.signupForm);
      alert("Your form is invalid")
    }
  }


}
