import { RoofColorOptions, BooleanOptions, AtticTypeOptions, sprayFoamOptions, RigidOptions, LooseFillOptions, BattOptions, InstallationTypeOptions, InsulationMaterialOptions } from './../../../shared/lookups/about-lookups';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
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

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getBuildingId();
    this.variableDeclaration();
  }

  //variable declaration
  variableDeclaration() {
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
      buildingId: [this.buildingId]
    })
    attic.get('atticType')?.valueChanges.subscribe(
      (val) => {
        let arr = attic.get('atticTypeDynamicOptions') as FormArray;
        arr.clear();
        if (val == 'Attic') {
          this.atticTypeInputs().forEach(group => arr.push(group));
        }
      }
    )
    return attic;
  }

  roofInputs() {
    return this.fb.group({
      roofName: [null, [Validators.required], [nameValidator('roofName')]],
      area: [null, [Validators.required,Validators.min(0)] ],
      solarAbsorptance: [null, [Validators.required, Validators.min(0), Validators.max(1)]],
      roofColor: [null, [Validators.required]],
      roofType: [null, [Validators.required]],
      radiantBarrier: [null, [Validators.required]],
      buildingId: [this.buildingId],
      insulation: this.insulationInputs(),
      skylights: this.fb.array([])
    })
  }

  wallInputs() {
    return this.fb.group({
      wallName: [null, [Validators.required], [nameValidator('wallName')]],
      atticWallType: [null, [Validators.required]],
      buildingId: [this.buildingId],
      area: [null, [Validators.required]],
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

  skylightInputs() {
    return this.fb.group({
      buildingId: [this.buildingId],
      area: [null, [Validators.required, Validators.min(0)]],
      uFactor: [null, [Validators.required, Validators.min(0)]],
      sHGC: [null, [Validators.required, Validators.max(1), Validators.min(0)]]
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
  atticTypeInputs(): FormGroup[] {
    let optForVented = {
      label: 'Attic Status for Vented',
      placeHolder: 'Vented or not',
      options: BooleanOptions,
      name: 'vented',
      errorMsg: 'Finished status is required',
    }
    let optForConditioned = {
      label: 'Attic Status for Conditioned',
      placeHolder: 'Conditioned or not',
      options: BooleanOptions,
      name: "conditioned",
      errorMsg: 'Conditioned status is required',
    }
    let optForCapeCod = {
      label: 'Attic Status for CapeCod',
      placeHolder: 'CapeCod or not',
      options: BooleanOptions,
      name: "capeCod",
      errorMsg: 'Cape Cod status is required',
    }
    return [
      this.fb.group({
        vented: [null, Validators.required],
        options: this.fb.control(optForVented)
      }),
      this.fb.group({
        conditioned: [null, Validators.required],
        options: this.fb.control(optForConditioned)
      }),
      this.fb.group({
        capeCod: [null, Validators.required],
        options: this.fb.control(optForCapeCod)
      }),
    ]
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
    if (this.atticForm.invalid) {
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
