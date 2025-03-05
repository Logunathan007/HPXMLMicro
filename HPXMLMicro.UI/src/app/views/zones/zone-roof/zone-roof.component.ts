import { RoofColorOptions, BooleanOptions, AtticTypeOptions, sprayFoamOptions, RigidOptions, LooseFillOptions, BattOptions, InstallationTypeOptions, InsulationMaterialOptions, ExteriorAdjacentToOptions, InteriorAdjacentToOptions } from './../../../shared/lookups/about-lookups';
import { Component } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from '../../../shared/services/common.service';
import { nameValidator } from '../../../shared/modules/Validators/validators';
import { atticWallTypeOptions, RoofTypeOptions } from '../../../shared/lookups/about-lookups';
import { arrayToObjectTransformer } from '../../../shared/modules/Transformers/TransormerFunction';

@Component({
  selector: 'app-zone-roof',
  templateUrl: './zone-roof.component.html',
  styleUrl: './zone-roof.component.scss'
})
export class ZoneRoofComponent {

  //variable initialization
  atticForm!: FormGroup;
  enableNext: boolean = false;
  buildingId!: string;
  hpxmlString!: string;
  validationMsg!: any;
  atticWallTypeOptions = atticWallTypeOptions;
  RoofTypeOptions = RoofTypeOptions;
  RoofColorOptions = RoofColorOptions;
  BooleanOptions = BooleanOptions;
  AtticTypeOptions = AtticTypeOptions;
  tempAttic = []
  installationTypeOptions = InstallationTypeOptions;
  insulationMaterialOptions = InsulationMaterialOptions;
  ExteriorAdjacentToOptions = ExteriorAdjacentToOptions
  interiorAdjacentToOptions = InteriorAdjacentToOptions;
  tracker: any;
  errorList: string[] = []

  get atticsObj(): FormArray {
    return this.atticForm.get('attics') as FormArray;
  }

  // act like a getter
  roofsObj(index: number): FormArray {
    return this.atticsObj.at(index).get('roofs') as FormArray;
  }
  wallsObj(index: number): FormArray {
    return this.atticsObj.at(index).get('walls') as FormArray;
  }
  frameFloorsObj(index: number): FormArray {
    return this.atticsObj.at(index).get('frameFloors') as FormArray;
  }
  skylightsObj(pIndex: number, cIndex: number): FormArray {
    return this.roofsObj(pIndex).at(cIndex).get('skylights') as FormArray;
  }
  roofInsulationObj(pindex: number, cindex: number): FormGroup {
    return this.roofsObj(pindex)?.at(cindex)?.get('insulation') as FormGroup;
  }
  roofInsulationMaterialDynamicOptionsObj(pindex: number, cindex: number, gcindex: number): FormArray {
    return this.roofInsulationLayersObj(pindex, cindex)?.at(gcindex)?.get('insulationMaterialDynamicOptions') as FormArray;
  }
  roofInsulationLayersObj(pindex: number, cindex: number): FormArray {
    return this.roofInsulationObj(pindex, cindex)?.get('layers') as FormArray;
  }
  atticTypeDynamicOptionsObj(index: number): FormArray {
    return this.atticsObj?.at(index).get('atticTypeDynamicOptions') as FormArray;
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
  wallInsulationObj(pindex: number, cindex: number): FormGroup {
    return this.wallsObj(pindex)?.at(cindex)?.get('insulation') as FormGroup;
  }
  wallInsulationMaterialDynamicOptionsObj(pindex: number, cindex: number, gcindex: number): FormArray {
    return this.wallInsulationLayersObj(pindex, cindex)?.at(gcindex)?.get('insulationMaterialDynamicOptions') as FormArray;
  }
  wallInsulationLayersObj(pindex: number, cindex: number): FormArray {
    return this.wallInsulationObj(pindex, cindex)?.get('layers') as FormArray;
  }

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getBuildingId();
    this.variableDeclaration();
  }

  //variable declaration
  variableDeclaration() {
    this.tracker = {
      minimumRoof: 0,
      maximumRoof: 0,
      minimumFrameFloor: 0,
      maximumFrameFloor: 0,
      minimumWall: 0,
      maximumWall: 0,
    }
    this.atticForm = this.fb.group({
      attics: this.fb.array([])
    })
    this.addToFormArray(this.atticsObj, this.atticInputs())
  }

  getBuildingId() {
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? ""
    })
  }

  //Input Field methods
  atticInputs() {
    let attic = this.fb.group({
      atticName: [null, [Validators.required], [nameValidator('atticName')]],
      atticType: [null, [Validators.required]],
      atticTypeDynamicOptions: this.fb.array([]),
      roofs: this.fb.array([]),
      walls: this.fb.array([]),
      frameFloors: this.fb.array([]),
      buildingId: [this.buildingId],
      tracker: [this.tracker]
    })
    attic.get('atticType')?.valueChanges.subscribe(
      (val) => {
        let arr = attic.get('atticTypeDynamicOptions') as FormArray;
        const roofs = attic.get('roofs') as FormArray;
        const walls = attic.get('walls') as FormArray;
        const frameFloors = attic.get('frameFloors') as FormArray;
        arr.clear();
        roofs.clear(); walls.clear(); frameFloors.clear()

        //all types must contains roofs
        this.tracker.minimumRoof = 1,
          this.tracker.maximumRoof = 100,
          roofs.push(this.roofInputs())
        if (val == 'Attic') {
          this.tracker = {
            minimumRoof: 1,
            maximumRoof: 100,
            minimumFrameFloor: 1,
            maximumFrameFloor: 100,
            minimumWall: 0,
            maximumWall: 100,
          }
          frameFloors.push(this.frameFloorInput())
          arr.push(this.atticTypeInputs(roofs, walls, frameFloors, attic.get('tracker') as AbstractControl))
        } else if (val == 'CathedralCeiling') {
          this.tracker = {
            minimumRoof: 1,
            maximumRoof: 100,
            minimumFrameFloor: 0,
            maximumFrameFloor: 100,
            minimumWall: 0,
            maximumWall: 100,
          }
        } else if (val == 'FlatRoof') {
          this.tracker = {
            minimumRoof: 1,
            maximumRoof: 100,
            minimumFrameFloor: 0,
            maximumFrameFloor: 100,
            minimumWall: 0,
            maximumWall: 100,
          }
        } else if (val == 'BelowApartment') {
          this.tracker = {
            minimumRoof: 1,
            maximumRoof: 100,
            minimumFrameFloor: 0,
            maximumFrameFloor: 100,
            minimumWall: 0,
            maximumWall: 100,
          }
        }
        attic.get('tracker')?.setValue(this.tracker);
      }
    )
    return attic;
  }
  atticTypeInputs(roofs: FormArray, walls: FormArray, frameFloors: FormArray, tracker: AbstractControl) {
    var atticTypes = this.fb.group({
      vented: [null,],
      capeCod: [null,],
      conditioned: [null,],
    })
    const capeCod = atticTypes.get('capeCod')
    const conditioned = atticTypes.get('conditioned')
    capeCod?.valueChanges.subscribe((val) => {
      if (val != null) {
        this.tracker = {
          minimumRoof: 1,
          maximumRoof: 100,
          minimumFrameFloor: 0,
          maximumFrameFloor: 100,
          minimumWall: 0,
          maximumWall: 100,
        }
        frameFloors.clear()
      }
      if (capeCod?.value == null && conditioned?.value == null) {
        this.tracker = {
          minimumRoof: 1,
          maximumRoof: 100,
          minimumFrameFloor: 1,
          maximumFrameFloor: 100,
          minimumWall: 0,
          maximumWall: 100,
        }
        frameFloors.push(this.frameFloorInput())
      }
      tracker?.setValue(this.tracker);
    })
    conditioned?.valueChanges.subscribe((val) => {
      if (val != null) {
        debugger
        this.tracker = {
          minimumRoof: 1,
          maximumRoof: 100,
          minimumFrameFloor: 0,
          maximumFrameFloor: 100,
          minimumWall: 0,
          maximumWall: 100,
        }
        frameFloors.clear()
      }
      if (capeCod?.value == null && conditioned?.value == null) {
        this.tracker = {
          minimumRoof: 1,
          maximumRoof: 100,
          minimumFrameFloor: 1,
          maximumFrameFloor: 100,
          minimumWall: 0,
          maximumWall: 100,
        }
        frameFloors.push(this.frameFloorInput())
      }
      tracker?.setValue(this.tracker);
    })
    return atticTypes;
  }
  roofInputs() {
    return this.fb.group({
      roofName: [null, [Validators.required], [nameValidator('roofName')]],
      area: [null, [Validators.required, Validators.min(0)]],
      solarAbsorptance: [null, [, Validators.min(0), Validators.max(1)]],
      roofColor: [null, []],
      roofType: [null, [Validators.required]],
      radiantBarrier: [null, []],
      buildingId: [this.buildingId],
      insulation: this.insulationInputs(),
      skylights: this.fb.array([])
    })
  }

  wallInputs() {
    return this.fb.group({
      wallName: [null, [Validators.required], [nameValidator('wallName')]],
      exteriorAdjacentTo: [null, Validators.required],
      interiorAdjacentTo: [null, Validators.required],
      atticWallType: [null, [Validators.required]],
      wallType: [null],
      area: [null, [Validators.required, Validators.min(0)]],
      insulation: this.insulationInputs(),
      buildingId: [this.buildingId],
    })
  }

  frameFloorInput() {
    return this.fb.group({
      buildingId: [this.buildingId],
      frameFloorName: [null, [Validators.required], [nameValidator('frameFloorName')]],
      area: [null, [Validators.required, Validators.min(0)]],
      insulation: this.insulationInputs()
    })
  }

  skylightInputs() {
    return this.fb.group({
      buildingId: [this.buildingId],
      area: [null, [, Validators.min(0)]],
      uFactor: [null, [, Validators.min(0)]],
      sHGC: [null, [, Validators.max(1), Validators.min(0)]]
    })
  }

  insulationInputs(): FormGroup {
    let insulation = this.fb.group({
      assemblyEffectiveRValue: [null, [Validators.min(0)]],
      layers: this.fb.array([this.layerInputs()])
    })
    return insulation;
  }

  layerInputs(): FormGroup {
    let layer = this.fb.group({
      nominalRValue: [null, [, Validators.min(0)]],
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
          errorMsg: 'Batt is invalid',
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
          errorMsg: 'Loose Fill is invalid',
        }
        fg = this.fb.group({
          looseFill: [null, []],
          options: this.fb.control(optForLooseFill)
        })
        break;
      case 'Rigid':
        let optForRigid = {
          label: 'Rigid Type',
          placeHolder: 'Rigid Type',
          options: RigidOptions,
          name: "rigid",
          errorMsg: 'Rigid is invalid',
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
          errorMsg: 'Spray Foam is invalid',
        }
        fg = this.fb.group({
          sprayFoam: [null,],
          options: this.fb.control(optForSprayFoam)
        })
        break;
    }
    return fg;
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

  //customValidation
  onSubmitValidation() {
    this.errorList.length = 0
    this.atticValidations();
  }
  atticValidations() {
    var atticIndex = 0;
    var attics = this.atticsObj;
    for (var attic of attics.controls) {
      this.roofValidations(atticIndex)
      this.wallValidations(atticIndex)
      this.frameFloorValidation(atticIndex)
      atticIndex++;
    }
  }
  wallValidations(atticIndex: number) {
    var wallIndex = 0;
    var walls = this.wallsObj(atticIndex)
    for (var wall of walls.controls) {
      if (!wall?.get('insulation')?.get("assemblyEffectiveRValue")?.value) {
        const layers = this.roofInsulationLayersObj(atticIndex, wallIndex)
        for (var layer of layers.controls) {
          if (!layer.get('nominalRValue')?.value) {
            this.errorList.push(`Attic ${atticIndex + 1} -> Wall ${wallIndex + 1} Every roof insulation layer needs a NominalRValue or AssemblyEffectiveRValue needs to be defined`)
            break;
          }
        }
      }
    }
  }

  frameFloorValidation(atticIndex:number) {
    var frameFloorIndex = 0;
    var frameFloors = this.frameFloorsObj(atticIndex);
    for(var frameFloor of frameFloors.controls){
      if (!frameFloor?.get('insulation')?.get("assemblyEffectiveRValue")?.value) {
        const layers = this.roofInsulationLayersObj(atticIndex, frameFloorIndex)
        for (var layer of layers.controls) {
          if (!layer.get('nominalRValue')?.value) {
            this.errorList.push(`Attic ${atticIndex + 1} -> FrameFloor ${frameFloorIndex + 1} Every roof insulation layer needs a NominalRValue or AssemblyEffectiveRValue needs to be defined`)
            break;
          }
        }
      }
    }
  }

  roofValidations(atticIndex: number) {
    var roofIndex = 0
    var roofs = this.roofsObj(atticIndex);
    for (var roof of roofs.controls) {
      if (!roof.get('solarAbsorptance')?.value && !roof.get('roofColor')?.value) {
        this.errorList.push(`Attic ${atticIndex + 1}: Invalid or missing RoofColor in Roof-${roofIndex + 1}`)
      }
      if (!roof?.get('insulation')?.get("assemblyEffectiveRValue")?.value) {
        const layers = this.roofInsulationLayersObj(atticIndex, roofIndex)
        for (var layer of layers.controls) {
          if (!layer.get('nominalRValue')?.value) {
            this.errorList.push(`Attic ${atticIndex + 1} -> Roof ${roofIndex + 1} Every roof insulation layer needs a NominalRValue or AssemblyEffectiveRValue needs to be defined`)
            break;
          }
        }
      }
    }
  }

  // for click event functions
  onSubmit() {
    this.onSubmitValidation()
    if (this.atticForm.invalid || this.errorList.length) {
      this.atticForm.markAllAsTouched(); window.scrollTo({ top: 0, behavior: 'smooth' });
      return;
    }
    let attic = this.atticForm.value;
    for (let ele of attic?.attics) {
      arrayToObjectTransformer(ele, 'atticTypeDynamicOptions');
      for (let item of ele?.roofs) {
        for (let layer of item?.insulation?.layers) {
          arrayToObjectTransformer(layer, 'insulationMaterialDynamicOptions');
        }
      }
      for (let item of ele?.frameFloors) {
        for (let layer of item?.insulation?.layers) {
          arrayToObjectTransformer(layer, 'insulationMaterialDynamicOptions');
        }
      }
    }
    this.commonService.sendZoneRoof(attic, this.buildingId).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err) => {
        console.log("Error occured");
      }
    })
  }
  goNext() {
    this.router.navigate(['zones/wall'], {
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
