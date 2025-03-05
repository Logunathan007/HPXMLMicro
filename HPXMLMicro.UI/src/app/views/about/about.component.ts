import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { housePressureOptions, leakinessDescriptionOptions, ManufacturedHomeSectionsOptions, OrientationOptions, residentialFacilityTypeOptions, unitofMeasureOptions, BooleanOptions } from './../../shared/lookups/about-lookups';
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
  orientationOfFrontOfHomeOptions = OrientationOptions;
  housePressureOptions = housePressureOptions;
  unitofMeasureOptions = unitofMeasureOptions;
  leakinessDescriptionOptions = leakinessDescriptionOptions;
  manufacturedHomeSectionsOptions = ManufacturedHomeSectionsOptions;
  booleanOptions = BooleanOptions;
  hpxmlString!: string;
  validationMsg!: any;
  enableNext: boolean = false;
  enableManufacturedHome: boolean = false;
  errorList: string[] = []

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
    this.addSubscribe();
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
      numberofBedrooms: [null, [Validators.required, Validators.min(0)]],
      numberofConditionedFloorsAboveGrade: [null, [Validators.required, Validators.min(0)]],
      averageCeilingHeight: [null, [Validators.min(0)]],
      conditionedBuildingVolume: [null, [Validators.min(0)]],
      conditionedFloorArea: [null, [Validators.min(0)]],
      azimuthOfFrontOfHome: [null, [Validators.min(0), Validators.max(360)]],
      orientationOfFrontOfHome: [null, []],
      manufacturedHomeSections: [null],
      comments:[null],
      // AirInfiltrationMeasurement
      airInfiltrationMeasurements: this.fb.array([
        this.airInfiltrationInput()
      ])
    })
  }

  addSubscribe() {
    this.aboutForm.get('residentialFacilityType')?.valueChanges.subscribe((val) => {
      const mHome = this.aboutForm.get('manufacturedHomeSections')
      if (val == 'manufactured home') {
        this.enableManufacturedHome = true;
        mHome?.setValidators(Validators.required)
      } else {
        mHome?.clearValidators()
        mHome?.setValue(null)
        this.enableManufacturedHome = false;
      }
      this.aboutForm.get('manufacturedHomeSections')?.setValue(null);
    })
  }

  airInfiltrationInput() {
    return this.fb.group({
      housePressure: [null, [Validators.min(0)]],
      unitofMeasure: [null, []],
      airLeakage: [null, [Validators.min(1)]],
      airSealing: [null, [Validators.min(0)]],
      leakinessDescription: [null, []]
    });
  }

  addAirInfiltrationInput() {
    this.errorList.length = 0;
    this.validateLastAirInfiltration()
    if (this.errorList.length) {
      window.scrollTo({ top: 0, behavior: 'smooth' });
      return;
    }
    this.airInfiltrationMeasurementsObj?.push(this.airInfiltrationInput());
  }
  removeAirInfiltrationInput(index: number) {
    if (this.airInfiltrationMeasurementsObj.controls.length == 1) return;
    this.airInfiltrationMeasurementsObj.removeAt(index);
  }
  validateLastAirInfiltration() {
    var len = this.airInfiltrationMeasurementsObj.length;
    if (len != 0) {
      this.airInfiltrationValidation(len - 1);
    }
  }
  validateAboutField() {
    this.errorList.length = 0;
    const averageCeilingHeight = this.aboutFormControl['averageCeilingHeight'].value
    const conditionedFloorArea = this.aboutFormControl['conditionedFloorArea'].value
    const conditionedBuildingVolume = this.aboutFormControl['conditionedBuildingVolume'].value
    const azimuthOfFrontOfHome = this.aboutFormControl['azimuthOfFrontOfHome'].value
    const orientationOfFrontOfHome = this.aboutFormControl['orientationOfFrontOfHome'].value
    if (!averageCeilingHeight && (!conditionedFloorArea || !conditionedBuildingVolume)) {
      this.errorList.push('Either AverageCeilingHeight or both ConditionedBuildingVolume and ConditionedFloorArea are required.')
    }
    if (!orientationOfFrontOfHome && !azimuthOfFrontOfHome) {
      this.errorList.push("Either AzimuthOfFrontOfHome or OrientationOfFrontOfHome is invalid.")
    }
  }
  airInfiltrationValidation(index: number) {
    const lastOne = this.airInfiltrationMeasurementsObj.at(index);
    const airSealing = lastOne.get('airSealing')?.value
    const airLeakage = lastOne.get('airLeakage')?.value
    const housePressure = lastOne.get('housePressure')?.value
    const unitofMeasure = lastOne.get('unitofMeasure')?.value
    const leakinessDescription = lastOne.get('leakinessDescription')?.value
    if (!airLeakage && !airSealing && !leakinessDescription) {
      this.errorList.push(`Air Infiltration ${index + 1} must have "AirLeakage" or "LeakinessDescription" or "AirSealing"`)
    }
    if (airLeakage && (!housePressure || !unitofMeasure)) {
      this.errorList.push(`In Air Infiltration ${index + 1} UnitofMeasure must be either "CFM" or "ACH" and HousePressure must be 50`)
    }
  }

  onSubmitValidation() {
    this.validateAboutField()
    this.validateLastAirInfiltration()
  }

  onSubmit() {
    this.onSubmitValidation()
    if (this.aboutForm.invalid || this.errorList.length != 0) {
      this.aboutForm.markAllAsTouched();
      window.scrollTo({ top: 0, behavior: 'smooth' });
      return;
    }

    this.commonService.sendAbout(this.aboutForm.getRawValue(), this.buildingId).subscribe((val) => {
      console.log(val);
    });
    this.enableNext = true;
  }

  goNext() {
    this.router.navigate(['zones/floor'], {
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
