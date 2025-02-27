import { HeatingSystemFuelOptions, HeatPumpTypeOptions, AnnualHeatingEfficiencyUnitsOptions, AnnualCoolingEfficiencyUnitsOptions, HeatingSystemTypeOptions, CoolingSystemTypeOptions } from './../../../shared/lookups/about-lookups';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from '../../../shared/services/common.service';
import { nameValidator } from '../../../shared/modules/Validators/validators';

@Component({
  selector: 'app-hvac-heat-cool',
  templateUrl: './hvac-heat-cool.component.html',
  styleUrl: './hvac-heat-cool.component.scss'
})
export class HvacHeatCoolComponent {
  //variable initialization
  hvacPlantForm!: FormGroup;
  enableNext: boolean = false;
  buildingId!: string;
  hpxmlString!: string;
  validationMsg!: any;
  distributionSystemOptions:any[] = [];
  heatingSystemFuelOptions = HeatingSystemFuelOptions
  heatingSystemTypeOptions = HeatingSystemTypeOptions
  heatPumpTypeOptions = HeatPumpTypeOptions
  annualHeatingEfficiencyUnitsOptions = AnnualHeatingEfficiencyUnitsOptions
  annualCoolingEfficiencyUnitsOptions = AnnualCoolingEfficiencyUnitsOptions
  coolingSystemTypeOptions = CoolingSystemTypeOptions

  get heatingSystemsObj(): FormArray {
    return this.hvacPlantForm.get('heatingSystems') as FormArray;
  }

  get coolingSystemsObj(): FormArray {
    return this.hvacPlantForm.get('coolingSystems') as FormArray;
  }

  get heatPumpsObj(): FormArray {
    return this.hvacPlantForm.get('heatPumps') as FormArray;
  }

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getBuildingId();
    this.variableDeclaration();
  }

  //variable declaration
  variableDeclaration() {
    this.hvacPlantForm = this.fb.group({
      heatPumps: this.fb.array([]),
      heatingSystems: this.fb.array([]),
      coolingSystems: this.fb.array([]),
    })

    this.commonService.getDistributionSystemNamesByBuildingId(this.buildingId).subscribe({
      next: (res:any) => {
        res?.namesAndIds.forEach((ele:any) => {
          this.distributionSystemOptions.push({name:ele.name,value:ele.id})
        });
      }
    })
  }

  getBuildingId() {
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? ""
    })
  }

  //Input Field methods
  heatingSystemsInputs(): FormGroup {
    let hs = this.fb.group({
      buildingId: [this.buildingId],
      heatingSystemName: [null, [Validators.required], [nameValidator('heatingSystemName')]],
      distributionSystemId: [null, [Validators.required]], //bending
      fractionHeatLoadServed: [null, [Validators.required, Validators.min(0), Validators.max(1)]], //double
      heatingCapacity: [null, Validators.required], //double
      heatingSystemFuel: [null, Validators.required], //options
      heatingSystemType: [null, Validators.required],//options
      annualHeatingEfficiencyUnits: [null, Validators.required],//options
      annualHeatingEfficiencyValue: [null, [Validators.required, Validators.min(0)]],
      modelYear: [null, [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
      yearInstalled: [null, [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
      floorAreaServed: [null, [Validators.required, Validators.min(0)]], //double
    })
    return hs;
  }

  heatPumpInputs(): FormGroup {
    let hp = this.fb.group({
      buildingId: [this.buildingId],
      heatPumpName: [null, [Validators.required], [nameValidator('heatPumpName')]],
      fractionHeatLoadServed: [null, [Validators.required, Validators.min(0), Validators.max(1)]],
      heatingCapacity17F: [null, [Validators.required]],
      heatingCapacity: [null, [Validators.required]],
      fractionCoolLoadServed: [null, [Validators.required, Validators.min(0), Validators.max(1)]],
      coolingCapacity: [null, [Validators.required]],
      heatPumpType: [null, [Validators.required]],
      annualHeatingEfficiencyUnits: [null, [Validators.required]],
      annualHeatingEfficiencyValue: [null, [Validators.required, Validators.min(0)]],
      annualCoolingEfficiencyUnits: [null, [Validators.required]],
      annualCoolingEfficiencyValue: [null, [Validators.required, Validators.min(0)]],
      modelYear: [null, [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
      yearInstalled: [null, [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
      floorAreaServed: [null, [Validators.required, Validators.min(0)]], //double
    })
    return hp;
  }
  coolingSystemInputs(): FormGroup {
    let cs = this.fb.group({
      buildingId: [this.buildingId],
      coolingSystemName: [null, [Validators.required], [nameValidator('coolingSystemName')]],
      fractionCoolLoadServed: [null, [Validators.required, Validators.min(0), Validators.max(1)]],
      distributionSystemId: [null, [Validators.required]],
      coolingCapacity: [null, [Validators.required]],
      coolingSystemType: [null, [Validators.required]],
      annualCoolingEfficiencyUnits: [null, [Validators.required]],
      annualCoolingEfficiencyValue: [null, [Validators.required]],
      modelYear: [null, [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
      yearInstalled: [null, [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
      floorAreaServed: [null, [Validators.required, Validators.min(0)]], //double
    })
    return cs;
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
    if (this.hvacPlantForm.invalid) {
      this.hvacPlantForm.markAllAsTouched();
      return;
    }

    this.commonService.sendHAVCPlant(this.hvacPlantForm.value, this.buildingId).subscribe({
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
