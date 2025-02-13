import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { housePressureOptions, leakinessDescriptionOptions, orientationOfFrontOfHomeOptions, residentialFacilityTypeOptions, unitofMeasureOptions } from './../../shared/lookups/about-lookups';
import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../shared/services/common.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrl: './about.component.scss'
})
export class AboutComponent implements OnInit {

  //variable initializations
  buildingId!: string;
  aboutForm!: FormGroup;
  residentialFacilityTypeOptions = residentialFacilityTypeOptions;
  orientationOfFrontOfHomeOptions = orientationOfFrontOfHomeOptions;
  housePressureOptions = housePressureOptions
  unitofMeasureOptions = unitofMeasureOptions
  leakinessDescriptionOptions = leakinessDescriptionOptions
  hpxmlString!: string;
  validationMsg!: any;
  enableNext: boolean = false;

  get aboutFormControl() {
    return this.aboutForm.controls;
  }

  get airInfiltrationMeasurementsObj(): FormArray {
    return this.aboutForm.get('airInfiltrationMeasurements') as FormArray;
  }

  constructor(
    public fb: FormBuilder,
    private commonService: CommonService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.variableDeclaration();
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? "";
    })
  }

  //variable declarations
  variableDeclaration() {
    this.aboutForm = this.fb.group({
      residentialFacilityType: [null, Validators.required],
      yearBuilt: ["", [Validators.required, Validators.min(999), Validators.max(new Date().getFullYear())]],
      numberofBedrooms: [null, [Validators.required]],
      numberofConditionedFloorsAboveGrade: [null, [Validators.required]],
      averageCeilingHeight: [null, [Validators.required]],
      conditionedBuildingVolume: [null, [Validators.required]],
      conditionedFloorArea: [null, [Validators.required]],
      azimuthOfFrontOfHome: [null, [Validators.required, Validators.min(0), Validators.max(360)]],
      orientationOfFrontOfHome: [null, [Validators.required]],
      // AirInfiltrationMeasurement
      airInfiltrationMeasurements: this.fb.array([
        this.airInfiltrationInput()
      ])
    })
  }

  airInfiltrationInput() {
    return this.fb.group({
      housePressure: [null, Validators.required],
      unitofMeasure: [null, [Validators.required]],
      airLeakage: [null, [Validators.required, Validators.max(1), Validators.min(0)]],
      leakinessDescription: [null, [Validators.required]]
    });
  }

  addAirInfiltrationInput() {
    this.airInfiltrationMeasurementsObj?.push(this.airInfiltrationInput());
  }

  removeAirInfiltrationInput(index: number) {
    if (this.airInfiltrationMeasurementsObj.controls.length == 1) return;
    this.airInfiltrationMeasurementsObj.removeAt(index);
  }

  onSubmit() {
    if (this.aboutForm.invalid) {
      this.aboutForm.markAllAsTouched();
      return;
    }
    this.commonService.sendAbout(this.aboutForm.getRawValue(), this.buildingId).subscribe((val) => {
      console.log(val);
      this.enableNext = true;
    });
  }

  goNext() {
    this.router.navigate(['zones','floor'], {
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
