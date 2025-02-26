import { Component } from '@angular/core';
import { BattOptions, BooleanOptions, DuctLocationOptions, DuctTypeOptions, InsulationMaterialOptions, LeakinessObservedVisualInspectionOptions, LooseFillOptions, RigidOptions, sprayFoamOptions, TotalOrToOutsideOptions, UnitsOptions } from '../../../shared/lookups/about-lookups';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { nameValidator } from '../../../shared/modules/Validators/validators';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from '../../../shared/services/common.service';

@Component({
  selector: 'app-distributed-system',
  templateUrl: './distributed-system.component.html',
  styleUrl: './distributed-system.component.scss'
})
export class DistributedSystemComponent {

  //variable initialization
  distributedSystemForm!: FormGroup;
  enableNext: boolean = false;
  buildingId!: string;
  hpxmlString!: string;
  validationMsg!: any;
  leakinessObservedVisualInspectionOptions = LeakinessObservedVisualInspectionOptions
  booleanOptions = BooleanOptions
  ductTypeOptions = DuctTypeOptions
  unitsOptions = UnitsOptions
  totalOrToOutsideOptions = TotalOrToOutsideOptions
  ductInsulationMaterialOptions = InsulationMaterialOptions
  ductLocationOptions = DuctLocationOptions
  get distributionSystemsObj(): FormArray {
    return this.distributedSystemForm.get('distributionSystem') as FormArray;
  }

  //act like a getter
  ductsObj(index: number): FormArray {
    return this.distributionSystemsObj?.at(index).get('ducts') as FormArray;
  }
  ductInsulationMaterialDynamicOptionsObj(pindex: number, cindex: number): FormArray {
    return this.ductsObj(pindex).at(cindex).get('ductInsulationMaterialDynamicOptions') as FormArray;
  }

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getBuildingId();
    this.variableDeclaration();
  }

  //variable declaration
  variableDeclaration() {
    this.distributedSystemForm = this.fb.group({
      distributionSystem: this.fb.array([])
    })
    this.addToFormArray(this.distributionSystemsObj, this.distributionSystemInputs())
  }

  getBuildingId() {
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? ""
    })
  }

  //Input Field methods
  distributionSystemInputs(): FormGroup {
    return this.fb.group({
      buildingId: [this.buildingId],
      distributionSystemName: [null, Validators.required, nameValidator('distributionSystemName')],
      leakinessObservedVisualInspection: [null, Validators.required], //options
      ductSystemSealed: [null, Validators.required], //boolean
      ductType: [null], //options
      units: [null, Validators.required],  //options
      value: [null, [Validators.required, Validators.min(0)]],
      totalOrToOutside: [null, Validators.required], //options
      ducts: this.fb.array([])
    })
  }

  ductInputs(): FormGroup {
    let duct = this.fb.group({
      ductInsulationRValue: [null, [Validators.required, Validators.min(0)]],
      ductInsulationThickness: [null, [Validators.required, Validators.min(0)]],
      ductInsulationMaterial: [null, Validators.required], //options
      ductInsulationMaterialDynamicOptions: this.fb.array([]),
      ductLocation: [null, Validators.required], //options
      fractionDuctArea: [null, [Validators.required, Validators.min(0), Validators.max(1)]]
    })
    duct.get('ductInsulationMaterial')?.valueChanges.subscribe((val: any) => {
      let arr = duct.get('ductInsulationMaterialDynamicOptions') as FormArray;
      let group = this.AddInsulationMaterialOptions(val)
      arr.clear();
      if (group) arr.push(group);
    })
    return duct;
  }

  AddInsulationMaterialOptions(insulationMaterial: string): FormGroup | null {
    var fg = null;
    switch (insulationMaterial) {
      case 'Batt':
        let optForBatt = {
          label: 'Batt Type',
          placeHolder: 'Batt Type',
          options: BattOptions,
          name: "batt",
          errorMsg: 'Batt is required',
        }
        fg = this.fb.group({
          batt: [null, Validators.required],
          options: this.fb.control(optForBatt)
        })
        break;
      case 'LooseFill':
        let optForLooseFill = {
          label: 'Loose Fill Type',
          placeHolder: 'Loose Fill Type',
          options: LooseFillOptions,
          name: "looseFill",
          errorMsg: 'Loose Fill is required',
        }
        fg = this.fb.group({
          looseFill: [null, [Validators.required]],
          options: this.fb.control(optForLooseFill)
        })
        break;
      case 'Rigid':
        let optForRigid = {
          label: 'Rigid Type',
          placeHolder: 'Rigid Type',
          options: RigidOptions,
          name: "looseFill",
          errorMsg: 'Loose Fill is required',
        }
        fg = this.fb.group({
          looseFill: [null, Validators.required],
          options: this.fb.control(optForRigid)
        })
        break;
      case 'SprayFoam':
        let optForSprayFoam = {
          label: 'Spray Foam Type',
          placeHolder: 'Spray Foam',
          options: sprayFoamOptions,
          name: "sprayFoam",
          errorMsg: 'Spray Foam is required',
        }
        fg = this.fb.group({
          sprayFoam: [null, Validators.required],
          options: this.fb.control(optForSprayFoam)
        })
        break;
    }
    return fg;
  }

  // for add and remove a dynamic form
  addToFormArray(array: FormArray, formToBeAdded: FormGroup) {
    array?.push(formToBeAdded);
  }
  removeFromFormArray(array: FormArray, index: number) {
    array.removeAt(index);
  }

  arrayToObjectTransformer(ele: any, name: string) {
    if (!Array.isArray(ele?.[name])) return;
    let opt = [...ele?.[name]];
    if (ele && ele?.[name]) {
      delete ele?.[name];
    }
    ele[name] = {};
    for (let obj of opt) {
      for (let [key, value] of Object.entries(obj)) {
        if (key !== 'options') {
          ele[name][key] = value;
        }
      }
    }
  }

  // for click event functions
  onSubmit() {
    if (this.distributedSystemForm.invalid) {
      this.distributedSystemForm.markAllAsTouched();
      return;
    }
    let ds = this.distributedSystemForm.value;
    for (let ele of ds?.distributionSystem) {
      for (let duct of ele?.ducts) {
        this.arrayToObjectTransformer(duct, 'ductInsulationMaterialDynamicOptions');
      }
    }
    this.commonService.sendDistributionSystem(ds, this.buildingId).subscribe({
      next: (res: any) => {
        console.log(res);
      },
      error: (err: any) => {
        console.log("Error occured");
      }
    })
  }

  goNext() {
    this.router.navigate([''], {
      queryParams: { id: this.buildingId }
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
