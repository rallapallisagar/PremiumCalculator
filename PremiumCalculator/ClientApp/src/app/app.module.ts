import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { GetPremiumComponent } from './get-premium/get-premium.component';
import { GetMonthlyPremiumService } from './get-premium/get-premium.service';
import { DataContextService } from './datacontext/datacontext.service';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatNativeDateModule} from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material';
 
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GetPremiumComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
    MatToolbarModule,
    MatSelectModule,
    NoopAnimationsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: GetPremiumComponent, pathMatch: 'full' },
      { path: 'get-premium', component: GetPremiumComponent },
    ])
  ],
  providers: [GetMonthlyPremiumService, DataContextService],
  bootstrap: [AppComponent]
})
export class AppModule { }
