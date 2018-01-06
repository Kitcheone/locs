import * as apiClient from '../../../api/apiclient';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-attendee',
  templateUrl: './attendee.component.html',
  styleUrls: ['./attendee.component.scss']
})
export class AttendeeComponent implements OnInit {

  lunch: apiClient.Lunch;
  attendee: apiClient.Attendee;
  isLoading = false;

  constructor(
    private route: ActivatedRoute,
    private lunchesClient: apiClient.LunchesClient,
    private attendeesClient: apiClient.AttendeesClient,
    private router: Router
  ) { }

  ngOnInit() {
    this.isLoading = true;
    this.lunchesClient.get(this.route.parent.snapshot.paramMap.get('lunchCode')).subscribe(result => {
      this.lunch = result;
      if (this.route.snapshot.paramMap.get('attendeeId')) {
        this.attendeesClient.getSingle(this.route.snapshot.paramMap.get('attendeeId')).subscribe(attendeeResult => {
          this.attendee = attendeeResult;
          this.isLoading = false;
        });
      } else {
        this.attendee = new apiClient.Attendee();
        this.attendee.lunchId = result.id;
        this.isLoading = false;
      }
    });
  }

  addAttendee() {
    if (!this.attendee.name || !this.attendee.choice) {
      return;
    }

    if (this.attendee.id) {
      this.attendeesClient.update(this.attendee).subscribe(result => {
        this.router.navigate(['/lunch', this.lunch.navigationUrl]);
      });
    } else {
      this.attendeesClient.insert(this.attendee).subscribe(result => {
        this.router.navigate(['/lunch', this.lunch.navigationUrl]);
      });
    }
  }

}
