import { Component, OnInit } from '@angular/core';
import * as apiClient from '../../api/apiclient';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-lunch',
  templateUrl: './new-lunch.component.html',
  styleUrls: ['./new-lunch.component.scss']
})
export class NewLunchComponent implements OnInit {

  lunch: apiClient.Lunch;

  constructor(
    private lunchesClient: apiClient.LunchesClient,
    private router: Router
  ) { }

  ngOnInit() {
    this.lunch = new apiClient.Lunch;
  }

  create() {
    if (!this.lunch.location) {
      return;
    }

    this.lunchesClient.post(this.lunch).subscribe(result => {
      this.router.navigate(['/lunch', result]);
    });
  }
}
