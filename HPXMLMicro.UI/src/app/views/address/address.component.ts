import { CommonService } from '../../shared/services/common.service';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrl: './address.component.scss'
})
export class AddressComponent {

  //variable initializations
  addressForm!: FormGroup;

  get addressControl() {
    return this.addressForm.controls;
  }

  constructor(public fb: FormBuilder, private commonService: CommonService, private router: Router) {
    this.variableDeclaration();
  }

  //variable declarations
  variableDeclaration() {
    this.addressForm = this.fb.group({
      address1: ["", Validators.required],
      address2: [""],
      state: ["", Validators.required],
      city: ["", Validators.required],
      zipcode: [null, [Validators.required, Validators.min(999), Validators.max(100000)]],
    })
  }

  onSubmit() {
    if (this.addressForm.invalid) {
      this.addressForm.markAllAsTouched();window.scrollTo({ top: 0, behavior: 'smooth' });
      return;
    }
    this.commonService.sendAddress(this.addressForm.getRawValue()).subscribe(
      (val: any) => {
        if (!val?.failed) {
          this.router.navigate(['about'], {
            queryParams: { id: val?.buildingId }
          });
        }
      });
  }
}
