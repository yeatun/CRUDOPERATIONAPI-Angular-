import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ValiadateForm from 'src/app/helpers/validateForm';
import { UserService } from 'src/app/service/user.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  type: string = "password"
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash"

  loginForm!:FormGroup;
  constructor(private fb: FormBuilder, private userservice: UserService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password:  ['', Validators.required]
    })
  }

  hideShowPass(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  onLogin(){
    if(this.loginForm.valid){
      
      console.log(this.loginForm.value)

      this.userservice.login(this.loginForm.value)
      .subscribe({
        next:(res)=>{
          alert(res.message)
          this.loginForm.reset();
          this.router.navigate(['dashboard'])
        },
        error:(err)=>{
          alert(err.error.message)
        }
        
      })
    }
    else{
      ValiadateForm.validateAllFormFields(this.loginForm)
      alert("Your form is invalid")
      // this.validateAllFormFields(this.loginForm);
      // alert("Your form is invalid")
    }
  }


}
