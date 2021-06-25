import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Festival } from '../models/festival.model';

@Injectable({ providedIn: 'root' })
export class FestivalDataService {

  private urls = {
    festivals: '/codingtest/api/v1/festivals',
  };

  constructor(private http: HttpClient) { }

  public getFestivalsData(): Observable<Array<Festival>> {
    return this.http.get<Array<Festival>>(`${this.urls.festivals}`);
  }
}
