import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FestivalBand } from '../core/models/festival.model';
import { FestivalsService } from './festivals.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-festivals',
  templateUrl: './festivals.component.html',
})
export class FestivalsComponent implements OnInit {

  public festivalBands$: Observable<Array<FestivalBand>> = new Observable();
  constructor(
    private readonly festivalsService: FestivalsService,
  ) { }

  public ngOnInit(): void {
    this.festivalBands$ = this.festivalsService.getFestivalsData();
  }

}
