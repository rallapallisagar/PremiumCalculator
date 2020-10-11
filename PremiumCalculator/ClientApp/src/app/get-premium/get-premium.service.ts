import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ProspectiveUserInfo, Rating } from './get-premium.component';
import { HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable, pipe } from 'rxjs'; 
import { DataContextService } from '../datacontext/datacontext.service';

 
export class GetMonthlyPremiumService {

  @Injectable({
    providedIn: 'root'
  })
   
  public premium: number;
  public apiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private dataContextService: DataContextService) {
    this.apiUrl = baseUrl + 'api/GeneratePremium';
  }

    
  public GenerateMonthlyPremiumRate(userInfo: ProspectiveUserInfo) {
    console.log('Occupation changed...');
    return this.dataContextService.httpPost(this.apiUrl, userInfo, true);

  }
} 

