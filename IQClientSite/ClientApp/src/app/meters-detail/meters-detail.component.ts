import { Component, Input, OnInit } from '@angular/core';
import { Meter } from '../IQResponses/Meter';

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
    let concatedFlags = '';
    for (let flag of statusFlags) {
      concatedFlags += ' ' + flag;
    }
    return concatedFlags;
  }

}
