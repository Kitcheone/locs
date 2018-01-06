import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LunchComponent } from './lunch/lunch.component';
import { NewLunchComponent } from './new-lunch/new-lunch.component';
import { AttendeeComponent } from './lunch/attendee/attendee.component';
import { LunchDetailComponent } from './lunch/lunch-detail/lunch-detail.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'new-lunch', component: NewLunchComponent },
    {
        path: 'lunch/:lunchCode',
        component: LunchComponent,
        children: [
            { path: '', component: LunchDetailComponent },
            { path: 'add-attendee', component: AttendeeComponent },
            { path: 'edit-attendee/:attendeeId', component: AttendeeComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
