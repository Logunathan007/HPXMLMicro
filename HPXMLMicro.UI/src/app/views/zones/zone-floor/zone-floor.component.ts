import { foundationTypeOptions } from './../../../shared/lookups/about-lookups';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component } from '@angular/core';

@Component({
  selector: 'app-zone-floor',
  templateUrl: './zone-floor.component.html',
  styleUrl: './zone-floor.component.scss'
})
export class ZoneFloorComponent {

  //for version 3 only not support for 4
  //variable initialization
  zoneFloorForm!: FormGroup;
  foundationTypeOptions = foundationTypeOptions;

  get foundationObj(): FormArray {
    return this.zoneFloorForm.get('foundations') as FormArray;
  }

  constructor(private fb: FormBuilder) {
    this.variableDeclaration();
  }

  //variable declaration
  variableDeclaration() {
    this.zoneFloorForm = this.fb.group(
      {
        foundations: this.fb.array(
          [
            this.foundationInput()
          ]
        )
      }
    )
  }

  foundationWallObj(index:number): FormArray {
    return this.foundationObj?.at(index).get('foundationWalls') as FormArray;
  }

  foundationInput() {
    //If there is more than one foundation, each needs an area specified on either "Slab" or "FrameFloor" attached. FrameFloor means floor
    return this.fb.group({
      //need to change dynamic its only for version 3.1 not support for 4
      foundationType: [null, Validators.required],
      //needs to change the validation
      finished  :[null],
      conditioned :[null],
      walkout :[null],
      foundationWalls: this.fb.array(
        [
          this.foundationWallsInput()
        ]
      )
    })
  }

  foundationWallsInput() {
    return this.fb.group({
      attachedToFoundationWall:[false],
      area: [null,  [Validators.required, Validators.max(100), Validators.min(0)]],
      nominalRValue: [null, [Validators.required, Validators.max(100), Validators.min(0)]],
      assemblyEffectiveRValue: [null, [Validators.required, Validators.max(100), Validators.min(0)]]
    })
  }

  addFoundationWall(index:number) {
    this.foundationWallObj(index)?.push(this.foundationWallsInput());
  }

  removeFoundationWall(findex: number,fwindex:number) {
    if (this.foundationWallObj(findex).controls.length == 1) return;
    this.foundationWallObj(findex).removeAt(fwindex);
  }

  addFoundation() {
    this.foundationObj?.push(this.foundationInput());
  }

  removeFoundation(index: number) {
    if (this.foundationObj.controls.length == 1) return;
    this.foundationObj.removeAt(index);
  }

  // isAttactToFoundationWall(){
  //   this.foundationWallObj
  // }

  onSubmit(){
    if (this.zoneFloorForm.invalid) {
      this.zoneFloorForm.markAllAsTouched();
      return;
    }
    console.log(this.zoneFloorForm)
  }
}
