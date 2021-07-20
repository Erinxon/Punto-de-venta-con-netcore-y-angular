import { DOCUMENT } from '@angular/common';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiAuthService } from 'src/app/Services/apiAuth-service';
import { Usuario } from '../../Models/Usuario';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private emailPattern: any = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  public user = this.formBuilder.group({
     email: ['', [Validators.required, Validators.pattern(this.emailPattern)]],
     password: ['', Validators.required]
  })

  isLogin: boolean = true;
  isLoanding: boolean = false;

  result = {
    IsSend: false,
    success: false,
    message: ''
  }

  constructor(private formBuilder: FormBuilder, 
    @Inject(DOCUMENT) private _document: Document,
    private loginService: ApiAuthService,
    private router: Router) {
      if(this.isLogin){
        this.router.navigate(['/home'])     
      }else{
        this.loginService.token$.emit(false);
      }
   }

  ngOnInit(): void {
    this.loginService.token$.subscribe( token => {
      this.isLogin = token;
   });
  }

  refreshPage() {
    this._document.defaultView?.location.reload();
  }

  

  login(): void {
    this.isLoanding = true;
    this.loginService.login(this.user.value['email'], this.user.value['password']).subscribe( r => {
       if(r.success){
          this.loginService.token$.emit(true);
          this.result.IsSend = true;
          this.result.success = true;
          this.isLoanding = false;
          this.router.navigate(['/home'])
          this.refreshPage();
       }else{
         this.loginService.token$.emit(false);
          this.result.IsSend = true;
          this.result.message = r.message;
          this.result.success = false;
          this.isLoanding = false;
       }
    });
    
  }

  get CamposValidos() {
    return {
       email: this.user.get('email'),
       password: this.user.get('password'),
    }
 }

 CerrarMensaje(): void {
  this.result.IsSend = false;
  this.result.success = false;
  this.result.message = '';
}


}
