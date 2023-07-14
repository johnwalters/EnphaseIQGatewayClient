import * as moment from 'moment-timezone';

export class Helpers {
  public static getStatusFlags(statusFlags: string[]): string {
    let concatedFlags = '';
    for (let flag of statusFlags) {
      concatedFlags += ' ' + flag;
    }
    return concatedFlags;
  }

  public static formatDate(ephochDate: number): string {
    return moment(new Date(ephochDate * 1000)).format('MM-DD-YY h:mm:ss A');
  }
}
