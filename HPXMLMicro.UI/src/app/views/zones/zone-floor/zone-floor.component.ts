import { CommonService } from './../../../shared/services/common.service';
import { BooleanOptions, FoundationTypeOptions } from './../../../shared/lookups/about-lookups';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { nameValidator } from '../../../shared/modules/Validators/validators';

@Component({
  selector: 'app-zone-floor',
  templateUrl: './zone-floor.component.html',
  styleUrl: './zone-floor.component.scss'
})
export class ZoneFloorComponent implements OnInit {

  //variable initialization
  foundationForm!: FormGroup;
  enableNext: boolean = false;
  buildingId!: string;
  hpxmlString!: string;
  validationMsg!: any;
  foundationTypeOptions = FoundationTypeOptions;

  get foundationsObj(): FormArray {
    return this.foundationForm.get('foundations') as FormArray;
  }

  // act like a getter
  foundationWallsObj(index: number): FormArray {
    return this.foundationsObj.at(index).get('foundationWalls') as FormArray;
  }

  slabsObj(index: number): FormArray {
    return this.foundationsObj.at(index).get('slabs') as FormArray;
  }

  frameFloorsObj(index: number): FormArray {
    return this.foundationsObj.at(index).get('frameFloors') as FormArray;
  }

  foundationTypeDynamicOptionsObj(index: number): FormArray {
    return this.foundationsObj?.at(index).get('foundationTypeDynamicOptions') as FormArray;
  }

  slabPerimeterInsulationsObj(pindex: number, index: number): FormArray {
    return this.slabsObj(pindex)?.at(index).get('perimeterInsulations') as FormArray;
  }

  foundationWallInsulationsObj(pindex: number, index: number): FormArray {
    return this.foundationWallsObj(pindex)?.at(index).get('insulations') as FormArray;
  }

  frameFloorInsulationsObj(pindex: number, index: number): FormArray {
    return this.frameFloorsObj(pindex)?.at(index).get('insulations') as FormArray;
  }

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.getBuildingId();
    this.variableDeclaration();
  }

  getBuildingId(){
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? ""
    })
  }

  //variable declaration
  variableDeclaration() {
    this.foundationForm = this.fb.group({
      foundations: this.fb.array([])
    })
    this.addNewFoundation()
  }

  //Input Field methods
  foundationInputs() {
    return this.fb.group({
      foundationName: [null, [Validators.required], [nameValidator('foundationName')]],
      foundationType: [null, Validators.required],
      foundationTypeDynamicOptions: this.fb.array([
        //dynamic inputs will be added for foundation Type
      ]),
      foundationWalls: this.fb.array([]),
      slabs: this.fb.array([]),
      frameFloors: this.fb.array([]),
      buildingId: [this.buildingId],
    })
  }

  foundationWallInputs() {
    return this.fb.group({
      buildingId: [this.buildingId],
      foundationWallName: [null, [Validators.required], [nameValidator('foundationWallName')]],
      area: [null, [Validators.required]],
      insulations: this.fb.array([this.insulationInputs()])
    })
  }

  frameFloorInput() {
    return this.fb.group({
      buildingId: [this.buildingId],
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

  basementInputs(): FormGroup[] {
    let optForFinished = {
      label: 'Basement Status for Finished',
      placeHolder: 'Finished or not',
      options: BooleanOptions,
      name: 'finished',
      errorMsg: 'Finished status is required',
    }
    let optForConditioned = {
      label: 'Basement Status for Conditioned',
      placeHolder: 'Conditioned or not',
      options: BooleanOptions,
      name: "conditioned",
      errorMsg: 'Conditioned status is required',
    }
    return [
      this.fb.group(
        {
          finished: [null, Validators.required],
          options: this.fb.control(optForFinished)
        }
      ),
      this.fb.group(
        {
          conditioned: [null, Validators.required],
          options: this.fb.control(optForConditioned)
        }
      ),
    ]
  }

  crawlspaceInputs(): FormGroup[] {
    let optForVented = {
      label: 'Crawlspace Status for Vented',
      placeHolder: 'Vented or not',
      options: BooleanOptions,
      name: 'vented',
      errorMsg: 'Finished status is required',
    }
    let optForConditioned = {
      label: 'Crawlspace Status for Conditioned',
      placeHolder: 'Conditioned or not',
      options: BooleanOptions,
      name: "conditioned",
      errorMsg: 'Crawlspace status is required',
    }
    return [
      this.fb.group(
        {
          vented: [null, Validators.required],
          options: this.fb.control(optForVented)
        }
      ),
      this.fb.group(
        {
          conditioned: [null, Validators.required],
          options: this.fb.control(optForConditioned)
        }
      ),
    ]
  }

  garageInputs() {
    let optForConditioned = {
      label: 'Garage Status for Conditioned',
      placeHolder: 'Conditioned or not',
      options: BooleanOptions,
      name: "conditioned",
      errorMsg: 'Crawlspace status is required',
    }
    return this.fb.group(
      {
        conditioned: [null, Validators.required],
        options: this.fb.control(optForConditioned)
      }
    )
  }

  slabInputs() {
    return this.fb.group({
      buildingId: [this.buildingId],
      slabName: [null, [Validators.required], [nameValidator('slabName')]],
      exposedPerimeter: [null, [Validators.required, Validators.max(100), Validators.min(0)]],
      perimeterInsulations: this.fb.array([this.perimeterInsulationInputs()])
    })
  }

  perimeterInsulationInputs() {
    return this.fb.group({
      nominalRValue: [null, [Validators.required, Validators.max(1), Validators.min(0.1)]],
      assemblyEffectiveRValue: [null, [Validators.max(1), Validators.min(0.1)]]
    })
  }

  // for adding multiple foundation
  addNewFoundation() {
    let found = this.foundationInputs()
    found.get('foundationType')?.valueChanges.subscribe(
      (val: any) => {
        let arr = found.get('foundationTypeDynamicOptions') as FormArray;
        let fndWall = (found.get('foundationWalls') as FormArray);
        let slabs = (found.get('slabs') as FormArray);
        arr.clear();
        if (val == 'Basement') {
          this.basementInputs().forEach(group => arr.push(group));
        } else if (val == 'Crawlspace') {
          this.crawlspaceInputs().forEach(group => arr.push(group));
        } else if (val == 'Garage') {
          arr.push(this.garageInputs())
        }
        slabs.clear();
        fndWall.clear();
        if (val == 'SlabOnGrade') {
          slabs.push(this.slabInputs())
        } else {
          fndWall.push(this.foundationWallInputs())
        }
      }
    )
    this.addToFormArray(this.foundationsObj, found)
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
    if (this.foundationForm.invalid) {
      this.foundationForm.markAllAsTouched();
      return;
    }
    let found = this.foundationForm.value;

    for (let ele of found.foundations) {
      if (!Array.isArray(ele?.foundationTypeDynamicOptions)) continue;
      let opt = [...ele.foundationTypeDynamicOptions];
      if (ele && ele.foundationTypeDynamicOptions) {
        delete ele.foundationTypeDynamicOptions;
      }
      ele.foundationTypeDynamicOptions = {};
      for (let obj of opt) {
        for (let [key, value] of Object.entries(obj)) {
          if (key !== 'options') {
            ele.foundationTypeDynamicOptions[key] = value;
          }
        }
      }
    }
    this.commonService.sendZoneFloor(found, this.buildingId).subscribe(
      (obj: any) => {
        console.log(obj);
        this.enableNext = true;
      }
    )
    console.log(this.foundationForm)
  }

  goNext() {
    this.router.navigate(['zones/roof'], {
      queryParams: { id:' this.buildingId' }
    })
  }

  generateHPXML() {
    this.commonService.getHPXMLString(this.buildingId).subscribe(
      (val: any) => {
        if (!val?.failed) {
          this.hpxmlString = val?.hpxmlString
        }
      }
    )
  }

  validateHPXML() {
    this.commonService.getHPXMLBase64(this.buildingId).subscribe(
      (val: any) => {
        if (!val?.failed) {
          this.commonService.validateHpxml(val).subscribe(
            (res: any) => {
              this.validationMsg = res
            }
          )
        }
      }
    )
  }
}


