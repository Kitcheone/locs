import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  lunchCode: string;

  constructor(private router: Router) { }

  ngOnInit() {
  }

  goToLunch() {
    this.router.navigate(['/lunch', this.lunchCode]);
  }

}
