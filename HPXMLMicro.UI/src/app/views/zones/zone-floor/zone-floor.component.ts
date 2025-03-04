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
  errorList: string[] = []
  tracker: any;

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
    this.tracker = {
      minimumFoundationWall: 0,
      maximumFoundationWall: 0,
      minimumFrameFloor: 0,
      maximumFrameFloor: 0,
      minimumSlab: 0,
      maximumSlab: 0,
    }
    this.foundationForm = this.fb.group({
      foundations: this.fb.array([])
    })
    this.addToFormArray(this.foundationsObj, this.foundationInputs())
  }

  //Input Field methods
  foundationInputs() {
    let found = this.fb.group({
      foundationName: [null, [Validators.required], [nameValidator('foundationName')]],
      foundationType: [null, [Validators.required]],
      foundationTypeDynamicOptions: this.fb.array([
        //dynamic inputs will be added for foundation Type
      ]),
      foundationWalls: this.fb.array([]),
      slabs: this.fb.array([]),
      frameFloors: this.fb.array([]),
      buildingId: [this.buildingId],
      tracker: this.fb.control(this.tracker)
    })
    found.get('foundationType')?.valueChanges.subscribe(
      (val: any) => {
        let arr = found.get('foundationTypeDynamicOptions') as FormArray;
        let fndWalls = (found.get('foundationWalls') as FormArray);
        let slabs = (found.get('slabs') as FormArray);
        let frameFloors = (found.get('frameFloors') as FormArray);
        arr.clear();
        fndWalls.clear();slabs.clear();frameFloors.clear();
        if (val == 'Basement') {
          this.basementInputs().forEach(group => arr.push(group));
          this.tracker = {
            minimumFoundationWall: 1,
            maximumFoundationWall: 100,
            minimumFrameFloor: 1,
            maximumFrameFloor: 100,
            minimumSlab: 0,
            maximumSlab: 100,
          }
          fndWalls.push(this.foundationWallInputs())
          frameFloors.push(this.frameFloorInput())
          found.get('tracker')?.setValue(this.tracker);
        }
        else if (val == 'Crawlspace') {
          this.crawlspaceInputs().forEach(group => arr.push(group));
          this.tracker = {
            minimumFoundationWall: 1,
            maximumFoundationWall: 100,
            minimumFrameFloor: 1,
            maximumFrameFloor: 100,
            minimumSlab: 0,
            maximumSlab: 100,
          }
          fndWalls.push(this.foundationWallInputs())
          frameFloors.push(this.frameFloorInput())
          found.get('tracker')?.setValue(this.tracker);
        }
        else if (val == 'SlabOnGrade') {
          this.tracker = {
            minimumFoundationWall: 0,
            maximumFoundationWall: 0,
            minimumFrameFloor: 0,
            maximumFrameFloor: 100,
            minimumSlab: 1,
            maximumSlab: 100,
          }
          slabs.push(this.slabInputs())
          found.get('tracker')?.setValue(this.tracker);
        }
        else if (val == 'Garage') {
          this.tracker = {
            minimumFoundationWall: 1,
            maximumFoundationWall: 100,
            minimumFrameFloor: 1,
            maximumFrameFloor: 100,
            minimumSlab: 0,
            maximumSlab: 100,
          }
          fndWalls.push(this.foundationWallInputs())
          frameFloors.push(this.frameFloorInput())
          found.get('tracker')?.setValue(this.tracker);
          arr.push(this.garageInputs())
        }
        else if (val == 'AboveApartment') {
          this.tracker = {
            minimumFoundationWall: 0,
            maximumFoundationWall: 0,
            minimumFrameFloor: 0,
            maximumFrameFloor: 100,
            minimumSlab: 0,
            maximumSlab: 100,
          }
          found.get('tracker')?.setValue(this.tracker);
        }
      }
    )
    return found;
  }

  foundationWallInputs() {
    return this.fb.group({
      buildingId: [this.buildingId],
      foundationWallName: [null, [Validators.required], [nameValidator('foundationWallName')]],
      area: [null, [Validators.min(0)]],
      insulation: this.insulationInputs()
    })
  }

  frameFloorInput() {
    return this.fb.group({
      buildingId: [this.buildingId],
      frameFloorName: [null, [Validators.required], [nameValidator('frameFloorName')]],
      area: [null, [Validators.min(0)]],
      insulation: this.insulationInputs()
    })
  }

  slabInputs() {
    return this.fb.group({
      buildingId: [this.buildingId],
      slabName: [null, [Validators.required], [nameValidator('slabName')]],
      area: [null, [Validators.min(0)]],
      exposedPerimeter: [null, [Validators.min(0)]],
      perimeterInsulation: this.insulationInputs()
    })
  }

  insulationInputs(frameFloor: boolean = false): FormGroup {
    let insulation = this.fb.group({
      assemblyEffectiveRValue: [null, [Validators.min(0)]],
      layers: this.fb.array([this.layerInputs()])
    })
    return insulation;
  }

  layerInputs(): FormGroup {
    let layer = this.fb.group({
      nominalRValue: [null, [Validators.required, Validators.min(0)]],
      installationType: [null, []],
      insulationMaterial: [null, []],
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
          batt: [null,],
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
          looseFill: [null,],
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
          rigid: [null,],
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
          sprayFoam: [null,],
          options: this.fb.control(optForSprayFoam)
        })
        break;
    }
    return fg;
  }

  basementInputs(): FormGroup[] {
    let optForFinished = {
      label: 'Basement tracker for Finished',
      placeHolder: 'Finished or not',
      options: BooleanOptions,
      name: 'finished',
      errorMsg: 'Finished tracker is required',
    }
    let optForConditioned = {
      label: 'Basement tracker for Conditioned',
      placeHolder: 'Conditioned or not',
      options: BooleanOptions,
      name: "conditioned",
      errorMsg: 'Conditioned tracker is required',
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
      label: 'Crawlspace tracker for Vented',
      placeHolder: 'Vented or not',
      options: BooleanOptions,
      name: 'vented',
      errorMsg: 'Finished tracker is required',
    }
    let optForConditioned = {
      label: 'Crawlspace tracker for Conditioned',
      placeHolder: 'Conditioned or not',
      options: BooleanOptions,
      name: "conditioned",
      errorMsg: 'Crawlspace tracker is required',
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
      label: 'Garage tracker for Conditioned',
      placeHolder: 'Conditioned or not',
      options: BooleanOptions,
      name: "conditioned",
      errorMsg: 'Crawlspace tracker is required',
    }
    return this.fb.group(
      {
        conditioned: [null, []],
        options: this.fb.control(optForConditioned)
      }
    )
  }

  // for add and remove a dynamic form
  addToFormArray(array: FormArray, formToBeAdded: FormGroup, max: number = 100) {
    if (array.invalid) {
      array.markAllAsTouched();
      return
    }
    if (array.length == max) {
      return;
    }
    array?.push(formToBeAdded);
  }
  removeFromFormArray(array: FormArray, index: number, min: number = 0) {
    if (min == array.length) {
      if (array.length == 1) return;
    }
    array.removeAt(index);
  }

  //Validations
  foundationValidation() {
    let len = this.foundationsObj.length
    if (len == 0) {
      this.errorList.push("There are no Foundation elements in this building.");
    }
    else if (len == 1) {
      this.foundationWallValidation(0);
      this.slabValidation(0);
      this.frameFloorValidation(0);
    }
    else {
      var foundationIndex = 0;
      for (var element of this.foundationsObj.controls) {
        const frameFloors = this.frameFloorsObj(foundationIndex);
        const foundationWall = this.foundationWallsObj(foundationIndex);
        const slabs = this.slabsObj(foundationIndex);
        if (slabs.controls.length == 0 && frameFloors.controls.length == 0) {
          this.errorList.push(`If there is more than one foundation, each needs an area specified on either "Slab" or "FrameFloor" attached.`);
          return;
        }
        else {
          frameFloors.controls.some(floor => {
            if (floor.get('area')?.value == null) {
              this.errorList.push(`Foundation ${foundationIndex + 1} -> FrameFloors: ` + `If there is more than one foundation, each needs an area specified on either "Slab" or "FrameFloor" attached.`);
              return true;
            }
            return false;
          })
          slabs.controls.some(slab => {
            if (slab.get('area')?.value == null) {
              this.errorList.push(`Foundation ${foundationIndex + 1} -> Slabs: ` + `If there is more than one foundation, each needs an area specified on either "Slab" or "FrameFloor" attached.`);
              return true;
            }
            return false;
          })
        }
        this.foundationWallValidation(foundationIndex);
        this.slabValidation(foundationIndex);
        this.frameFloorValidation(foundationIndex);
        foundationIndex++;
      };
    }
  }
  foundationWallValidation(foundationIndex: number) {
    var foundationWalls = this.foundationWallsObj(foundationIndex);
    if (foundationWalls.length > 1) {
      foundationWalls.controls.some(element => {
        if (element.get('area')?.value == null) {
          this.errorList.push(`Foundation ${foundationIndex + 1} -> Foundationwalls: ` + "If there is more than one FoundationWall, an area is required for each.")
          return true;
        }
        return false;
      })
    }
  }
  slabValidation(foundationIndex: number) {
    var slabs = this.slabsObj(foundationIndex);
    if (slabs.length > 1) {
      slabs.controls.some(element => {
        if (element.get('exposedPerimeter')?.value == null) {
          this.errorList.push(`Foundation ${foundationIndex} -> Slabs: ` + "If there is more than one Slab, an ExposedPerimeter is required for each.")
          return true;
        }
        return false;
      })
    }
  }
  frameFloorValidation(foundationIndex: number) {
    var frameFloors = this.frameFloorsObj(foundationIndex);
    if (frameFloors.length == 0) {

    } else if (frameFloors.length == 1) {

    }
    else {
      for (var element of frameFloors.controls) {
        if (element.get('area')?.value == null) {
          this.errorList.push(`Foundation ${foundationIndex + 1} -> FrameFloors: ` + "If there is more than one FrameFloor, an Area is required for each.")
          break;
        }
      }
    }
  }

  // On submit Validation
  onSubmitValidation() {
    this.errorList.length = 0
    this.foundationValidation();
  }

  // for click event functions
  onSubmit() {
    this.onSubmitValidation()
    if (this.foundationForm.invalid || this.errorList.length != 0) {
      this.foundationForm.markAllAsTouched(); window.scrollTo({ top: 0, behavior: 'smooth' });
      // setTimeout(() => {
      //   this.errorList.length = 0;
      // }, 10000)
      return;
    }
    let found = this.foundationForm.value;
    for (let ele of found.foundations) {
      arrayToObjectTransformer(ele, 'foundationTypeDynamicOptions');
      for (let item of ele?.foundationWalls) {
        for (let layer of item?.insulation?.layers) {
          arrayToObjectTransformer(layer, 'insulationMaterialDynamicOptions');
        }
      }
      for (let item of ele?.slabs) {
        for (let layer of item?.perimeterInsulation?.layers) {
          arrayToObjectTransformer(layer, 'insulationMaterialDynamicOptions');
        }
      }
      for (let item of ele?.frameFloors) {
        for (let layer of item?.insulation?.layers) {
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


