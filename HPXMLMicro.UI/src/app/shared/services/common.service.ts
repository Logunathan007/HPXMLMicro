import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { envVariable } from '../../environents/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  constructor(private http: HttpClient) { }

  //for Address
  sendAddress(req: any) {
    return this.http.post(envVariable.API_URL + 'Address', req);
  }

  //for About
  sendAbout(req: any, id: string) {
    const params = new HttpParams().set('buildingId', id)
    return this.http.post(envVariable.API_URL + 'About', req, { params });
  }

  //for ZoneFloor
  sendZoneFloor(req: any, id: string) {
    const params = new HttpParams().set('buildingId', id)
    return this.http.post(envVariable.API_URL + 'ZoneFloor', req, { params })
  }

  //for ZoneRoof
  sendZoneRoof(req: any, id: string) {
    const params = new HttpParams().set('buildingId', id)
    return this.http.post(envVariable.API_URL + 'ZoneRoof', req, { params })
  }

  //for ZoneWall
  sendZoneWall(req: any, id: string) {
    const params = new HttpParams().set('buildingId', id)
    return this.http.post(envVariable.API_URL + 'ZoneWall', req, { params })
  }

  //for DistributionSystem
  sendDistributionSystem(req: any, id: string) {
    const params = new HttpParams().set('buildingId', id)
    return this.http.post(envVariable.API_URL + 'DistributionSystem', req, { params })
  }
  getDistributionSystemNamesByBuildingId(id: string) {
    const params = new HttpParams().set('buildingId', id)
    return this.http.get(envVariable.API_URL + 'DistributionSystem/GetNames', { params })
  }

  //for HVAC Systems
  sendHAVCPlant(req: any, id: string) {
    const params = new HttpParams().set('buildingId', id)
    return this.http.post(envVariable.API_URL + 'HVACPlant', req, { params })
  }

  //for HPXMLGeneration
  getHPXMLString(id: string) {
    const params = new HttpParams().set('buildingId', id)
    return this.http.get(envVariable.API_URL + 'HPXMLGeneration/string', { params })
  }

  getHPXMLBase64(id: string) {
    const params = new HttpParams().set('buildingId', id)
    return this.http.get(envVariable.API_URL + 'HPXMLGeneration/base64', { params })
  }

  //for Validation
  validateHpxml(res: any) {
    return this.http.post(envVariable.VALIDATION_URL, res)
  }

}
