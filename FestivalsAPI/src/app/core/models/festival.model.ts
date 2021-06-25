export interface Band {
  name: string;
  recordLabel: string;
}

export interface Festival {
  name: string;
  bands: Array<Band>;
}

export interface FestivalBand {
  festivalName: string;
  bandName: string;
}
