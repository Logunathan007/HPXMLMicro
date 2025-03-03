import { WaterHeaterTypeOptions, FuelTypeOptions } from './../../../shared/lookups/about-lookups';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { nameValidator } from '../../../shared/modules/Validators/validators';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from '../../../shared/services/common.service';

@Component({
  selector: 'app-water-heating-system',
  templateUrl: './water-heating-system.component.html',
  styleUrl: './water-heating-system.component.scss'
})
export class WaterHeatingSystemComponent {
  //variable initialization
  waterHeaterForm!: FormGroup;
  enableNext: boolean = false;
  buildingId!: string;
  hpxmlString!: string;
  validationMsg!: any;
  waterHeaterTypeOptions = WaterHeaterTypeOptions
  fuelTypeOptions = FuelTypeOptions

  get waterHeatingSystemsObj(): FormArray {
    return this.waterHeaterForm.get('waterHeatingSystems') as FormArray;
  }

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getBuildingId();
    this.variableDeclaration();
  }

  //variable declaration
  variableDeclaration() {
    this.waterHeaterForm = this.fb.group({
      waterHeatingSystems: this.fb.array([]),

    })
  }

  getBuildingId() {
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? ""
    })
  }
  //Input Field methods
  WaterHeatingSystemInputs(): FormGroup {
    let hw = this.fb.group({
      buildingId: [this.buildingId],
      HeatingSystemName: [null, [Validators.required], [nameValidator('HeatingSystemName')]],
      fractionDHWLoadServed: [null, [Validators.required]], //double
      waterHeaterType: [null, [Validators.required]], //options
      fuelType: [null, [Validators.required]], //options
      energyFactor: [null, [Validators.required, Validators.min(0), Validators.max(5)]],
      uniformEnergyFactor: [null, [Validators.required, Validators.min(0), Validators.max(5)]],
      modelYear: [null, [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
      yearInstalled: [null, [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
    })
    return hw;
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
    if (this.waterHeaterForm.invalid) {
      this.waterHeaterForm.markAllAsTouched();window.scrollTo({ top: 0, behavior: 'smooth' });
      return;
    }

    this.commonService.sendWaterHeating(this.waterHeaterForm.value, this.buildingId).subscribe({
      next: (res: any) => {
        console.log(res);
      },
      error: (err: any) => {
        console.log("Error occured");
      }
    })
  }
  goNext() {
    this.router.navigate(['/systems/pv-system'], {
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
