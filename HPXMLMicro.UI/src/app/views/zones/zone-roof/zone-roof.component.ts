import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from '../../../shared/services/common.service';
import { nameValidator } from '../../../shared/modules/Validators/validators';

@Component({
  selector: 'app-zone-roof',
  templateUrl: './zone-roof.component.html',
  styleUrl: './zone-roof.component.scss'
})
export class ZoneRoofComponent {

  //variable initialization
  atticForm!: FormGroup;
  enableNext: boolean = false;
  buildingId: string = ""
  hpxmlString!: string;
  validationMsg!: any;

  get atticsObj(): FormArray {
    return this.atticForm.get('attics') as FormArray;
  }

  // act like a getter
  roofsObj(index: number): FormArray {
    return this.atticsObj.at(index).get('roofs') as FormArray;
  }

  wallsObj(index: number): FormArray {
    return this.atticsObj.at(index).get('slabs') as FormArray;
  }

  frameFloorsObj(index: number): FormArray {
    return this.atticsObj.at(index).get('frameFloors') as FormArray;
  }

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) {
    this.variableDeclaration();
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? ""
    })
  }

  //variable declaration
  variableDeclaration() {
    this.atticForm = this.fb.group({
      attics: this.fb.array([])
    })
    this.addNewAttics()
  }

  //Input Field methods
  atticInputs() {
    return this.fb.group({
    })
  }

  frameFloorInput() {
    return this.fb.group({
      frameFloorName: [null, [Validators.required], [nameValidator('frameFloorName')]],
      area: [null, [Validators.required]],
      insulations: this.fb.array([this.insulationInputs()])
    })
  }

  insulationInputs() {
    return this.fb.group({
      nominalRValue: [null, [Validators.required, Validators.max(1), Validators.min(0.1)]],
      assemblyEffectiveRValue: [null, [Validators.required, Validators.max(1), Validators.min(0.1)]]
    })
  }

  // for adding multiple foundation
  addNewAttics() {
    let found = this.atticInputs()
    this.addToFormArray(this.atticsObj, found)
  }

  // for add and remove a dynamic form
  addToFormArray(array: FormArray, formToBeAdded: FormGroup) {
    array?.push(formToBeAdded);
  }
  removeFromFormArray(array: FormArray, index: number) {
    array.removeAt(index);
  }

}
