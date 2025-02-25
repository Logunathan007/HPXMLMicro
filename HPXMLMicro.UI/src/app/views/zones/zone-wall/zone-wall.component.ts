import { InstallationTypeOptions, OrientationOptions, WallTypeOptions, InsulationMaterialOptions, GlassTypeOptions, GlassLayersOptions, GasFillOptions, FrameTypeOptions, SidingOptions, ExteriorAdjacentToOptions, InteriorAdjacentToOptions } from './../../../shared/lookups/about-lookups';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonService } from '../../../shared/services/common.service';
import { nameValidator } from '../../../shared/modules/Validators/validators';
import { BattOptions, BooleanOptions, FramingTypeOptions, LooseFillOptions, RigidOptions } from '../../../shared/lookups/about-lookups';

@Component({
  selector: 'app-zone-wall',
  templateUrl: './zone-wall.component.html',
  styleUrl: './zone-wall.component.scss'
})
export class ZoneWallComponent {
  //variable initialization
  wallForm!: FormGroup;
  enableNext: boolean = false;
  buildingId!: string;
  hpxmlString!: string;
  validationMsg!: any;
  wallTracker!: any[];
  wallTypeOptions = WallTypeOptions;
  orientationOptions = OrientationOptions;
  installationTypeOptions = InstallationTypeOptions;
  insulationMaterialOptions = InsulationMaterialOptions;
  glassTypeOptions = GlassTypeOptions;
  glassLayersOptions = GlassLayersOptions;
  gasFillOptions = GasFillOptions;
  frameTypeOptions = FrameTypeOptions;
  sidingOptions = SidingOptions;
  ExteriorAdjacentToOptions = ExteriorAdjacentToOptions
  interiorAdjacentToOptions = InteriorAdjacentToOptions;
  get wallsObj(): FormArray {
    return this.wallForm.get('walls') as FormArray;
  }

  // act like a getter
  wallTypeDynamicOptionsObj(index: number): FormArray {
    return this.wallsObj?.at(index).get('wallTypeDynamicOptions') as FormArray;
  }
  wallInsulationsObj(index: number): FormArray {
    return this.wallsObj?.at(index).get('insulations') as FormArray;
  }
  windowsObj(index: number): FormArray {
    return this.wallsObj?.at(index).get('windows') as FormArray;
  }
  frameTypeDynamicOptionsObj(pindx: number, cindex: number): FormArray {
    return this.windowsObj(pindx).at(cindex).get('frameTypeDynamicOptions') as FormArray;
  }
  insulationMaterialDynamicOptionsObj(pindex: number, cindex: number): FormArray {
    return this.wallInsulationsObj(pindex).at(cindex).get('insulationMaterialDynamicOptions') as FormArray;
  }

  constructor(private fb: FormBuilder, private router: Router, private commonService: CommonService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getBuildingId();
    this.variableDeclaration();
  }

  //variable declaration
  variableDeclaration() {
    this.wallForm = this.fb.group({
      walls: this.fb.array([])
    })
    this.addNewWall()
  }

  getBuildingId() {
    this.route.queryParamMap.subscribe(params => {
      this.buildingId = params.get('id') ?? ""
    })
  }

  //Input Field methods

  wallInputs() {
    return this.fb.group({
      buildingId: [this.buildingId],
      wallName: [null, [Validators.required], nameValidator('wallName')],
      exteriorAdjacentTo: [null, Validators.required],
      interiorAdjacentTo: [null, Validators.required],
      wallType: [null, Validators.required],
      wallTypeDynamicOptions: this.fb.array([]),
      siding: [null, Validators.required],
      area: [null, [Validators.required, Validators.min(0)]],
      azimuth: [null, [Validators.required, Validators.min(0), Validators.max(360)]],
      orientation: [null, Validators.required],
      insulations: this.fb.array([]),
      windows: this.fb.array([]),
    })
  }

  woodStudInputs() {
    let optForExpandedPolystyreneSheathing = {
      label: 'Expanded Polystyrene Sheathing',
      placeHolder: 'Expanded Polystyrene Sheathing',
      options: BooleanOptions,
      name: 'expandedPolystyreneSheathing',
      errorMsg: 'Expanded Polystyrene Sheathing status is required',
    }
    let optForOptimumValueEngineering = {
      label: 'Optimum Value Engineering',
      placeHolder: 'Optimum Value Engineering',
      options: BooleanOptions,
      name: "optimumValueEngineering",
      errorMsg: 'Optimum Value Engineering status is required',
    }
    let optForFramingType = {
      label: 'Framing Type',
      placeHolder: 'Framing Type',
      options: FramingTypeOptions,
      name: "framingType",
      errorMsg: 'Framing Type is required',
    }
    return [
      this.fb.group({
        expandedPolystyreneSheathing: [null, Validators.required],
        options: this.fb.control(optForExpandedPolystyreneSheathing)
      }),
      this.fb.group({
        optimumValueEngineering: [null, Validators.required],
        options: this.fb.control(optForOptimumValueEngineering)
      }),
      this.fb.group({
        framingType: [null, Validators.required],
        options: this.fb.control(optForFramingType)
      }),
    ]
  }

  doubleWoodStudInputs() {
    let optForStaggered = {
      label: 'Staggered Status',
      placeHolder: 'Staggered or not',
      options: BooleanOptions,
      name: "staggered",
      errorMsg: 'Staggered is required',
    }
    return this.fb.group({
      staggered: [null, Validators.required],
      options: this.fb.control(optForStaggered)
    })
  }

  insulationInputs() {
    let insulation = this.fb.group({
      nominalRValue: [null, [Validators.required, Validators.min(0)]],
      assemblyEffectiveRValue: [null, [Validators.min(0)]],
      installationType: [null, [Validators.required]],
      insulationMaterial: [null, [Validators.required]],
      insulationMaterialDynamicOptions: this.fb.array([])
    })
    insulation.get('insulationMaterial')?.valueChanges.subscribe((val: any) => {
      let arr = insulation.get('insulationMaterialDynamicOptions') as FormArray;
      let group = this.AddInsulationMaterialOptions(val)
      arr.clear();
      if (group) arr.push(group);
    })
    return insulation;
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
          looseFill: [null,[Validators.required]],
          options: this.fb.control(optForLooseFill)
        })
        break;
      case 'Rigid':
        let optForRigid = {
          label: 'Rigid Type',
          placeHolder: 'Rigid Type',
          options: RigidOptions,
          name: "looseFill",
          errorMsg: 'Loose Fill is required',
        }
        fg = this.fb.group({
          looseFill: [null, Validators.required],
          options: this.fb.control(optForRigid)
        })
        break;
    }
    return fg;
  }

  thermalBreakInput() {
    let opt = {
      label: 'Thermal Break',
      placeHolder: 'Thermal Break',
      options: BooleanOptions,
      name: "thermalBreak",
      errorMsg: 'Thermal Break is required',
    }
    return this.fb.group({
      thermalBreak: [null, Validators.required],
      options: this.fb.control(opt)
    })
  }

  windowInputs() {
    let window = this.fb.group({
      buildingId: [this.buildingId],
      area: [null, [Validators.required, Validators.min(0)]],
      uFactor: [null, [Validators.required, Validators.min(0)]],
      sHGC: [null, [Validators.required, Validators.max(1), Validators.min(0)]],
      frameType: [null, [Validators.required]],
      frameTypeDynamicOptions: this.fb.array([]),
      glassType: [null, [Validators.required]],
      glassLayers: [null, [Validators.required]],
      gasFill: [null, [Validators.required]],
      orientation: [null, [Validators.required]],
      azimuth: [null, [Validators.required, Validators.min(0), Validators.max(360)]]
    })
    window.get('frameType')?.valueChanges.subscribe((val) => {
      let arr = window.get('frameTypeDynamicOptions') as FormArray;
      arr.clear();
      if (val == 'Aluminum' || val == 'Metal') {
        arr.push(this.thermalBreakInput());
      }
    })
    return window;
  }

  addNewWall() {
    let wall = this.wallInputs();
    wall.get('wallType')?.valueChanges.subscribe((val) => {
      let arr = wall.get('wallTypeDynamicOptions') as FormArray
      arr.clear();
      if (val == 'WoodStud') {
        this.woodStudInputs().forEach(obj => arr.push(obj));
      } else if (val == 'DoubleWoodStud') {
        arr.push(this.doubleWoodStudInputs())
      }
    })
    this.addToFormArray(this.wallsObj, wall)
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
    if (this.wallForm.invalid) {
      this.wallForm.markAllAsTouched();
      return;
    }
    let wall = this.wallForm.value;
    for (let ele of wall?.walls) {
      this.arrayToObjectTransformer(ele, 'wallTypeDynamicOptions');
      for (let insulation of ele?.insulations) {
        this.arrayToObjectTransformer(insulation, 'insulationMaterialDynamicOptions');
      }
      for (let window of ele?.windows) {
        this.arrayToObjectTransformer(window, 'frameTypeDynamicOptions');
      }
    }
    this.commonService.sendZoneWall(wall, this.buildingId).subscribe({
      next: (res:any) => {
        console.log(res);
      },
      error: (err:any) => {
        console.log("Error occured");
      }
    })
  }

  goNext() {
    this.router.navigate(['wall'], {
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

