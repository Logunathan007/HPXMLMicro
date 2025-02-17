import { RoofColorOptions, BooleanOptions } from './../../../shared/lookups/about-lookups';
import { Component } from '@angular/core';
import { Form, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from '../../../shared/services/common.service';
import { nameValidator } from '../../../shared/modules/Validators/validators';
import { atticWallTypeOptions, RoofTypeOptions } from '../../../shared/lookups/about-lookups';

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
  atticWallTypeOptions = atticWallTypeOptions;
  RoofTypeOptions = RoofTypeOptions;
  RoofColorOptions = RoofColorOptions;
  BooleanOptions = BooleanOptions;

  get atticsObj(): FormArray {
    return this.atticForm.get('attics') as FormArray;
  }

  // act like a getter
  roofsObj(index: number): FormArray {
    return this.atticsObj.at(index).get('roofs') as FormArray;
  }

  wallsObj(index: number): FormArray {
    return this.atticsObj.at(index).get('walls') as FormArray;
  }

  frameFloorsObj(index: number): FormArray {
    return this.atticsObj.at(index).get('frameFloors') as FormArray;
  }

  roofInsulationsObj(pIndex: number, cIndex: number): FormArray {
    return this.roofsObj(pIndex).at(cIndex).get('insulations') as FormArray;
  }

  frameFloorInsulationsObj(pindex: number, index: number): FormArray {
    return this.frameFloorsObj(pindex)?.at(index).get('insulations') as FormArray;
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
      atticName: [null, [Validators.required],[nameValidator('atticName')]],
      atticType: [null, [Validators.required]],
      roofs: this.fb.array([]),
      walls: this.fb.array([]),
      frameFloors: this.fb.array([]),
    })
  }

  roofInputs() {
    return this.fb.group({
      roofName: [null, [Validators.required], [nameValidator('roofName')]],
      area: [null, [Validators.required]],
      solarAbsorptance: [null, [Validators.required, Validators.min(0.1), Validators.max(1)]],
      roofColor: [null, [Validators.required]],
      roofType: [null, [Validators.required]],
      radiantBarrier: [null, [Validators.required]],
      insulations: this.fb.array([this.insulationInputs()])
    })
  }

  wallInputs() {
    return this.fb.group({
      wallName: [null, [Validators.required], [nameValidator('wallName')]],
      atticWallType: [null, [Validators.required]],
      area: [null, [Validators.required]],
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

  // for click event functions
  onSubmit() {
    if (this.atticForm.invalid) {
      this.atticForm.markAllAsTouched();
      return;
    }
  }
  goNext(){

  }
}
