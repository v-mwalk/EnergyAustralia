export interface Band {
  name?: string;
  festivals?: Array<string>;
}

export interface RecordLabel {
  name: string;
  bands?: Array<Band>;
}
