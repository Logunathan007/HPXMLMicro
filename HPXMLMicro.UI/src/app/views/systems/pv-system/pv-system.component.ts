import { OrientationOptions } from './../../../shared/lookups/about-lookups';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from '../../../shared/services/common.service';
import { nameValidator } from '../../../shared/modules/Validators/validators';

@Component({
  selector: 'app-pv-system',
  templateUrl: './pv-system.component.html',
  styleUrl: './pv-system.component.scss'
})
export class PvSystemComponent {
  //variable initialization
  photovoltaicForm!: FormGroup;
  enableNext: boolean = false;
  buildingId!: string;
  hpxmlString!: string;
  validationMsg!: any;
  orientationOptions = OrientationOptions

  get pVSystemsObj(): FormArray {
    return this.photovoltaicForm.get('pVSystems') as FormArray;
  }

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getBuildingId();
    this.variableDeclaration();
  }

  //variable declaration
  variableDeclaration() {
    this.photovoltaicForm = this.fb.group({
      pVSystems: this.fb.array([]),
    })
  }

  getBuildingId() {
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? ""
    })
  }

  //Input Field methods
  pVSystemInputs(): FormGroup {
    let pvs = this.fb.group({
      buildingId: [this.buildingId],
      pVSystemName: [null, [Validators.required], [nameValidator('pVSystemName')]],
      maxPowerOutput: [null, [Validators.required, Validators.min(0)]], //double
      collectorArea: [null, [Validators.required, Validators.min(0)]], //double
      numberOfPanels: [null, [Validators.required, Validators.min(0)]], //int
      yearInverterManufactured: [null, [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
      yearModulesManufactured: [null, [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
      arrayAzimuth: [null, [Validators.required, Validators.min(0), Validators.max(360)]],//double
      arrayOrientation: [null, [Validators.required]], //options
      arrayTilt: [null, [Validators.required, Validators.min(0), Validators.max(90)]],//double
    })
    return pvs;
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
    if (this.photovoltaicForm.invalid) {
      this.photovoltaicForm.markAllAsTouched();window.scrollTo({ top: 0, behavior: 'smooth' });
      return;
    }
    this.commonService.sendPhotovoltaic(this.photovoltaicForm.value, this.buildingId).subscribe({
      next: (res: any) => {
        console.log(res);
      },
      error: (err: any) => {
        console.log("Error occured");
      }
    })
  }
  // goNext() {
  //   this.router.navigate([''], {
  //     queryParams: { id: this.buildingId }
  //   })
  // }
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
