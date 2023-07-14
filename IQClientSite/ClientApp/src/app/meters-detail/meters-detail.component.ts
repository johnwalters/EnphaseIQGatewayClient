import { Component, Input, OnInit } from '@angular/core';
import { Meter } from '../IQResponses/Meter';
import { Helpers } from '../Helpers';

@Component({
  selector: 'app-meters-detail',
  templateUrl: './meters-detail.component.html',
  styleUrls: ['./meters-detail.component.css']
})
export class MetersDetailComponent implements OnInit {


  @Input() meters: Meter[];

  constructor() { }

  ngOnInit(): void {
  }

  getStatusFlags(statusFlags: string[]): string {
    return Helpers.getStatusFlags(statusFlags);
  }

}
