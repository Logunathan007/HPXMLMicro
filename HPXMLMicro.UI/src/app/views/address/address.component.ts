import { CommonService } from '../../shared/services/common.service';
import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrl: './address.component.scss'
})
export class AddressComponent {

  //variable initializations
  addressForm!:FormGroup;

  get addressControl() {
    return this.addressForm.controls;
  }

  constructor(public fb:FormBuilder,private commonService:CommonService){
    this.variableDeclaration();
  }

  //variable declarations
  variableDeclaration(){
    this.addressForm = this.fb.group({
      address1:["",Validators.required],
      address2:[""],
      state:["",Validators.required],
      city:["",Validators.required],
      zipcode:[null,[Validators.required,Validators.min(999),Validators.max(100000)]],
    })
  }

  onSubmit(){
    debugger
    if(this.addressForm.invalid) {
      this.addressForm.markAllAsTouched();
      return;
    }
    this.commonService.sendAddress(this.addressForm.getRawValue()).subscribe((val)=>{
      console.log(val);
    });
  }
}
