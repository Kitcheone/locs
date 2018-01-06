import * as apiClient from '../../../api/apiclient';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-lunch-detail',
  templateUrl: './lunch-detail.component.html',
  styleUrls: ['./lunch-detail.component.scss']
})
export class LunchDetailComponent implements OnInit {

  lunch: apiClient.Lunch;
  isLoading = false;
  deletingAttendeeId: string;

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

  deleteAttendee(attendeeId: string) {
    this.attendeesClient.delete(attendeeId).subscribe(result => {
      this.refresh();
    });
  }
}
