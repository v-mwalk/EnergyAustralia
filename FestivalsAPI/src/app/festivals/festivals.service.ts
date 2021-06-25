import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FestivalDataService } from '../core/data-services/festival-data.service';
import { FestivalBand } from '../core/models/festival.model';

@Injectable({ providedIn: 'root' })
export class FestivalsService {

  constructor(
    private readonly festivalDataService: FestivalDataService,
  ) { }

  public getFestivalsData(): Observable<Array<FestivalBand>> {
    return this.festivalDataService.getFestivalsData()
      .pipe(
        map(festivals => {
          const festivalBands: Array<FestivalBand> = [];
          festivals.forEach(festival => {
            festival.bands.forEach(band => {
              festivalBands.push({bandName: band.name, festivalName: festival.name})
            });
          });  
          return festivalBands.sort((a, b) => a.bandName < b.bandName ? -1: 1);
        }),
      );
  }
}
