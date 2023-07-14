import { Component, Input, OnInit } from '@angular/core';
import { Inverter } from '../IQResponses/Inverter';
import { Helpers } from '../Helpers';

@Component({
  selector: 'app-inverters-detail',
  templateUrl: './inverters-detail.component.html',
  styleUrls: ['./inverters-detail.component.css']
})
export class InvertersDetailComponent implements OnInit {
  @Input() inverters: Inverter[];
  constructor() { }

  ngOnInit(): void {
  }

  formatDate(ephochDate: number): string {
    return Helpers.formatDate(ephochDate);
  }
}
