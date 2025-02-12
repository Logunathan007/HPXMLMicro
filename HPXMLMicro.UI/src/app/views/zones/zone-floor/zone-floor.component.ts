import { BooleanOptions, FoundationTypeOptions } from './../../../shared/lookups/about-lookups';
import { AbstractControl, FormArray, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-zone-floor',
  templateUrl: './zone-floor.component.html',
  styleUrl: './zone-floor.component.scss'
})
export class ZoneFloorComponent {

  //variable initialization
  foundationForm!: FormGroup;

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

  constructor(private fb: FormBuilder) {
    this.variableDeclaration();
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
      foundationName: [null, [Validators.required],[this.nameValidator('foundationName')]],
      foundationType: [null, Validators.required],
      foundationTypeDynamicOptions: this.fb.array([
        //dynamic inputs will be added
      ]),
      foundationWalls: this.fb.array([]),
      slabs: this.fb.array([]),
      frameFloors: this.fb.array([])
    })
  }

  foundationWallInputs() {
    return this.fb.group({
      foundationWallName: [null, [Validators.required],[this.nameValidator('foundationWallName')]],
      area: [null, [Validators.required]],
      insulations: this.fb.array([this.insulationInputs()])
    })
  }

  frameFloorInput() {
    return this.fb.group({
      frameFloorName: [null, [Validators.required],[this.nameValidator('frameFloorName')]],
      area: [null, [Validators.required]],
      insulations: this.fb.array([this.insulationInputs()])
    })
  }

  insulationInputs() {
    return this.fb.group({
      nominalRValue: [null, [Validators.required, Validators.max(100), Validators.min(0)]],
      assemblyEffectiveRValue: [null, [Validators.required, Validators.max(100), Validators.min(0)]]
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
      slabName: [null, [Validators.required],[this.nameValidator('slabName')]],
      exposedPerimeter: [null, [Validators.required, Validators.max(100), Validators.min(0)]],
      perimeterInsulations: this.fb.array([this.perimeterInsulationInputs()])
    })
  }

  perimeterInsulationInputs() {
    return this.fb.group({
      nominalRValue: [null, [Validators.required, Validators.max(100), Validators.min(0)]],
      assemblyEffectiveRValue: [null,[Validators.max(100), Validators.min(0)]]
    })
  }

  // Async validator
  nameValidator(name: string) {
    return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return new Promise((resolve) => {
        setTimeout(() => {
          let arr = control.parent?.parent as any;
          if (arr && arr.controls) {
            for (let element of arr.controls) {
              if (control !== element.get(name) && element.get(name)?.value === control.value) {
                resolve({ nameSame: true });
                return;
              }
            }
          }
          resolve(null);
        }, 500);
      });
    };
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

  onSubmit() {
    if (this.foundationForm.invalid) {
      this.foundationForm.markAllAsTouched();
      return;
    }
    console.log(this.foundationForm)
  }
}
