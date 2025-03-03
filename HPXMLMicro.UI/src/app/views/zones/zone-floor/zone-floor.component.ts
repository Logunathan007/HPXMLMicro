import { CommonService } from './../../../shared/services/common.service';
import { BattOptions, BooleanOptions, FoundationTypeOptions, InstallationTypeOptions, InsulationMaterialOptions, LooseFillOptions, RigidOptions, sprayFoamOptions } from './../../../shared/lookups/about-lookups';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { nameValidator } from '../../../shared/modules/Validators/validators';
import { arrayToObjectTransformer } from '../../../shared/modules/Transformers/TransormerFunction';

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
  installationTypeOptions = InstallationTypeOptions;
  insulationMaterialOptions = InsulationMaterialOptions;
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
  foundationWallInsulationObj(pindex: number, cindex: number): FormGroup {
    return this.foundationWallsObj(pindex)?.at(cindex)?.get('insulation') as FormGroup;
  }
  foundationWallInsulationMaterialDynamicOptionsObj(pindex: number, cindex: number, gcindex: number): FormArray {
    return this.foundationWallInsulationLayersObj(pindex, cindex)?.at(gcindex)?.get('insulationMaterialDynamicOptions') as FormArray;
  }
  foundationWallInsulationLayersObj(pindex: number, cindex: number): FormArray {
    return this.foundationWallInsulationObj(pindex, cindex)?.get('layers') as FormArray;
  }

  slabInsulationObj(pindex: number, cindex: number): FormGroup {
    return this.slabsObj(pindex)?.at(cindex)?.get('perimeterInsulation') as FormGroup;
  }
  slabInsulationMaterialDynamicOptionsObj(pindex: number, cindex: number, gcindex: number): FormArray {
    return this.slabInsulationLayersObj(pindex, cindex)?.at(gcindex)?.get('insulationMaterialDynamicOptions') as FormArray;
  }
  slabInsulationLayersObj(pindex: number, cindex: number): FormArray {
    return this.slabInsulationObj(pindex, cindex)?.get('layers') as FormArray;
  }

  frameFloorInsulationObj(pindex: number, cindex: number): FormGroup {
    return this.frameFloorsObj(pindex)?.at(cindex)?.get('insulation') as FormGroup;
  }
  frameFloorInsulationMaterialDynamicOptionsObj(pindex: number, cindex: number, gcindex: number): FormArray {
    return this.frameFloorInsulationLayersObj(pindex, cindex)?.at(gcindex)?.get('insulationMaterialDynamicOptions') as FormArray;
  }
  frameFloorInsulationLayersObj(pindex: number, cindex: number): FormArray {
    return this.frameFloorInsulationObj(pindex, cindex)?.get('layers') as FormArray;
  }

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.getBuildingId();
    this.variableDeclaration();
  }

  getBuildingId() {
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? ""
    })
  }

  //variable declaration
  variableDeclaration() {
    this.foundationForm = this.fb.group({
      foundations: this.fb.array([])
    })
    this.addToFormArray(this.foundationsObj, this.foundationInputs())
  }

  //Input Field methods
  foundationInputs() {
    let found = this.fb.group({
      foundationName: [null, [], [nameValidator('foundationName')]],
      foundationType: [null,],
      foundationTypeDynamicOptions: this.fb.array([
        //dynamic inputs will be added for foundation Type
      ]),
      foundationWalls: this.fb.array([]),
      slabs: this.fb.array([]),
      frameFloors: this.fb.array([]),
      buildingId: [this.buildingId],
    })
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
    return found;
  }

  foundationWallInputs() {
    return this.fb.group({
      buildingId: [this.buildingId],
      foundationWallName: [null, [], [nameValidator('foundationWallName')]],
      area: [null, [Validators.min(0)]],
      insulation: this.insulationInputs()
    })
  }
  frameFloorInput() {
    return this.fb.group({
      buildingId: [this.buildingId],
      frameFloorName: [null, [], [nameValidator('frameFloorName')]],
      area: [null, [Validators.min(0)]],
      insulation: this.insulationInputs()
    })
  }
  slabInputs() {
    return this.fb.group({
      buildingId: [this.buildingId],
      slabName: [null, [], [nameValidator('slabName')]],
      area: [null, [Validators.min(0)]],
      exposedPerimeter: [null, [Validators.min(0)]],
      perimeterInsulation: this.insulationInputs()
    })
  }

  insulationInputs(): FormGroup {
    let insulation = this.fb.group({
      assemblyEffectiveRValue: [null, [Validators.min(0)]],
      layers: this.fb.array([ this.layerInputs() ])
    })
    return insulation;
  }

  layerInputs(): FormGroup {
    let layer = this.fb.group({
      nominalRValue: [null, [Validators.required, Validators.min(0)]],
      installationType: [null, [Validators.required]],
      insulationMaterial: [null, [Validators.required]],
      insulationMaterialDynamicOptions: this.fb.array([])
    })
    layer.get('insulationMaterial')?.valueChanges.subscribe((val: any) => {
      let arr = layer.get('insulationMaterialDynamicOptions') as FormArray;
      let group = this.AddInsulationMaterialOptions(val)
      arr.clear();
      if (group) arr.push(group);
    })
    return layer;
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
          name: "rigid",
          errorMsg: 'Rigid is required',
        }
        fg = this.fb.group({
          rigid: [null, Validators.required],
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
          finished: [null,],
          options: this.fb.control(optForFinished)
        }
      ),
      this.fb.group(
        {
          conditioned: [null,],
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
          vented: [null, []],
          options: this.fb.control(optForVented)
        }
      ),
      this.fb.group(
        {
          conditioned: [null, []],
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
        conditioned: [null, []],
        options: this.fb.control(optForConditioned)
      }
    )
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
      this.foundationForm.markAllAsTouched(); window.scrollTo({ top: 0, behavior: 'smooth' });
      return;
    }
    let found = this.foundationForm.value;

    for (let ele of found.foundations) {
      arrayToObjectTransformer(ele,'foundationTypeDynamicOptions');
      for(let item of ele?.foundationWalls){
        for(let layer of item?.insulation?.layers){
          arrayToObjectTransformer(layer, 'insulationMaterialDynamicOptions');
        }
      }
      for(let item of ele?.slabs){
        for(let layer of item?.perimeterInsulation?.layers){
          arrayToObjectTransformer(layer, 'insulationMaterialDynamicOptions');
        }
      }
      for(let item of ele?.frameFloors){
        for(let layer of item?.insulation?.layers){
          arrayToObjectTransformer(layer, 'insulationMaterialDynamicOptions');
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


