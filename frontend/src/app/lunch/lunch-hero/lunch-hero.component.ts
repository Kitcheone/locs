import * as apiClient from '../../../api/apiclient';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-lunch-hero',
  templateUrl: './lunch-hero.component.html',
  styleUrls: ['./lunch-hero.component.scss']
})
export class LunchHeroComponent implements OnInit {

  lunch: apiClient.Lunch;
  url = window.location.href;
  isLoading = false;

  constructor(
    private route: ActivatedRoute,
    private lunchesClient: apiClient.LunchesClient,
    private attendeesClient: apiClient.AttendeesClient
  ) { }

  ngOnInit() {
    this.refresh();
  }

  private refresh() {
    this.isLoading = true;
    this.lunchesClient.get(this.route.snapshot.paramMap.get('lunchCode')).subscribe(result => {
      this.lunch = result;
      this.isLoading = false;
    });
  }

}
