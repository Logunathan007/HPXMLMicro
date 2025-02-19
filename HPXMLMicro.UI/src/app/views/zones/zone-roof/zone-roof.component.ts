import { RoofColorOptions, BooleanOptions, AtticTypeOptions } from './../../../shared/lookups/about-lookups';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  AtticTypeOptions = AtticTypeOptions;
  tempAttic = []

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

  atticTypeDynamicOptionsObj(index: number): FormArray {
    return this.atticsObj?.at(index).get('atticTypeDynamicOptions') as FormArray;
  }

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.getBuildingId();
    this.variableDeclaration();
  }

  //variable declaration
  variableDeclaration() {
    this.atticForm = this.fb.group({
      attics: this.fb.array([])
    })
    this.addNewAttics()
  }

  getBuildingId(){
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? ""
    })
  }

  //Input Field methods
  atticInputs() {
    return this.fb.group({
      atticName: [null, [Validators.required], [nameValidator('atticName')]],
      atticType: [null, [Validators.required]],
      atticTypeDynamicOptions: this.fb.array([]),
      roofs: this.fb.array([]),
      walls: this.fb.array([]),
      frameFloors: this.fb.array([]),
      buildingId:[this.buildingId]
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
      buildingId:[this.buildingId] ,
      insulations: this.fb.array([this.insulationInputs()])
    })
  }

  wallInputs() {
    return this.fb.group({
      wallName: [null, [Validators.required], [nameValidator('wallName')]],
      atticWallType: [null, [Validators.required]],
      buildingId:[this.buildingId] ,
      area: [null, [Validators.required]],
    })
  }

  frameFloorInput() {
    return this.fb.group({
      frameFloorName: [null, [Validators.required], [nameValidator('frameFloorName')]],
      buildingId:[this.buildingId] ,
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

  atticTypeInputs(): FormGroup[] {
    let optForVented = {
      label: 'Attic Status for Vented',
      placeHolder: 'Vented or not',
      options: BooleanOptions,
      name: 'vented',
      errorMsg: 'Finished status is required',
    }
    let optForConditioned = {
      label: 'Attic Status for Conditioned',
      placeHolder: 'Conditioned or not',
      options: BooleanOptions,
      name: "conditioned",
      errorMsg: 'Conditioned status is required',
    }
    let optForCapeCod = {
      label: 'Attic Status for CapeCod',
      placeHolder: 'CapeCod or not',
      options: BooleanOptions,
      name: "capeCod",
      errorMsg: 'Cape Cod status is required',
    }
    return [
      this.fb.group({
        vented: [null, Validators.required],
        options: this.fb.control(optForVented)
      }),
      this.fb.group({
        conditioned: [null, Validators.required],
        options: this.fb.control(optForConditioned)
      }),
      this.fb.group({
        capeCod: [null, Validators.required],
        options: this.fb.control(optForCapeCod)
      }),
    ]
  }

  // for adding multiple foundation
  addNewAttics() {
    let attic = this.atticInputs()
    attic.get('atticType')?.valueChanges.subscribe(
      (val) => {
        let arr = attic.get('atticTypeDynamicOptions') as FormArray;
        arr.clear();
        if (val == 'Attic') {
          this.atticTypeInputs().forEach(group => arr.push(group));
        }
      }
    )
    this.addToFormArray(this.atticsObj, attic)
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
    let attic = this.atticForm.value;
    for (let ele of attic.attics) {
      if(!Array.isArray(ele?.atticTypeDynamicOptions)) continue;
      let opt = [...ele.atticTypeDynamicOptions];
      if (ele && ele.atticTypeDynamicOptions) {
        delete ele.atticTypeDynamicOptions;
      }
      ele.atticTypeDynamicOptions = {};
      for (let obj of opt) {
        for (let [key, value] of Object.entries(obj)) {
          if (key !== 'options') {
            ele.atticTypeDynamicOptions[key] = value;
          }
        }
      }
    }
    this.commonService.sendZoneRoof(attic,this.buildingId).subscribe({
      next:(res)=>{
        console.log(res);
      },
      error:(err)=>{
        console.log("Error occured");
      }
    })
  }
  goNext() {

  }
}
