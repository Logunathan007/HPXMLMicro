import { RootTypeOptions, RoofColorOptions, BooleanOptions, InsulationMaterialOptions, BattOptions, LooseFillOptions, RigidOptions } from './../../../shared/lookups/about-lookups';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-zone-roof',
  templateUrl: './zone-roof.component.html',
  styleUrl: './zone-roof.component.scss'
})
export class ZoneRoofComponent {

  //variable initialization
  zoneRoofForm!: FormGroup;
  RootTypeOptions = RootTypeOptions;
  RoofColorOptions = RoofColorOptions;
  BooleanOptions = BooleanOptions;
  InsulationMaterialOptions = InsulationMaterialOptions;

  get roofsObj(): FormArray {
    return this.zoneRoofForm?.get('roofs') as FormArray;
  }

  // act like a getter
  insulationMaterialDynamicOptionsObj(index: number): FormArray {
    return this.roofsObj?.at(index).get('insulationMaterialDynamicOptions') as FormArray;
  }

  constructor(private fb: FormBuilder) {
    this.variableDeclaration();
  }

  //variable declaration
  variableDeclaration() {
    this.zoneRoofForm = this.fb.group(
      {
        roofs: this.fb.array([])
      }
    )
    this.addNewRoof();
  }

  // Input fields for dynamic form
  roofInputs() {
    return this.fb.group({
      area: [null, [Validators.required]],
      roofType: [null, [Validators.required]],
      roofColor: [null, [Validators.required]],
      solarAbsorptance: [null, [Validators.required, Validators.max(100), Validators.min(0)]],
      radiantBarrier: [null, [Validators.required]], //boolean
      insulationMaterial: [null], //condition based not required one
      nominalRValue: [null, [Validators.required, Validators.max(100), Validators.min(0)]],
      assemblyEffectiveRValue: [null, [Validators.required, Validators.max(100), Validators.min(0)]],
      //dynamic inputs
      insulationMaterialDynamicOptions: this.fb.array([
        //for dynamic groups
      ])
    })
  }
  BattsInputs(){
    let opt = {
      label:'Insulation Material Batt is',
      name: 'batt',
      options:BattOptions,
      errorMsg: "Batt is required",
      placeHolder:"Batt is"
    }
    return this.fb.group({
      batt : [null,[Validators.required]],
      options: this.fb.control(opt),
    })
  }
  LooseFillInputs(){
    let opt = {
      label:'Insulation Material Loose Fill is',
      name: 'looseFill',
      options:LooseFillOptions,
      errorMsg: "Loose Fill is required",
      placeHolder:"Loose Fill is"
    }
    return this.fb.group({
      looseFill : [null,[Validators.required]],
      options: this.fb.control(opt),
    })
  }
  RigidInputs(){
    let opt = {
      label:'Insulation Material Rigid is',
      name: 'rigid',
      options:RigidOptions,
      errorMsg: "Rigid is required",
      placeHolder:"Rigid is"
    }
    return this.fb.group({
      rigid : [null,[Validators.required]],
      options: this.fb.control(opt),
    })
  }
  addNewRoof(){
    let roof = this.roofInputs();
    roof?.get('insulationMaterial')?.valueChanges.subscribe(
      (obj:any)=>{
        let dyObj = roof.get('insulationMaterialDynamicOptions') as FormArray;
        dyObj.clear();
        if(obj == 'Batt'){
          dyObj.push(this.BattsInputs())
        }else if(obj == 'LooseFill'){
          dyObj.push(this.LooseFillInputs())
        }else if(obj == 'Rigid'){
          dyObj.push(this.RigidInputs())
        }
      }
    )
    this.addToFormArray(this.roofsObj,roof);
  }

  addToFormArray(array: FormArray, formToBeAdded: FormGroup) {
    array?.push(formToBeAdded);
  }

  removeFromFormArray(array: FormArray, index: number) {
    if (array.controls.length == 1) return;
    array.removeAt(index);
  }

  clearFormArray(array:FormArray){
    array.clear();
  }
}
