import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
//import { HomeComponent } from './home/home.component';
//import { CounterComponent } from './counter/counter.component';
//import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { FetchContactComponent } from './fetch-contact/fetch-contact.component';
import { CreateContactComponent } from './fetch-contact/create-contact.component';
import { ContactService } from './fetch-contact/contact.service';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    //HomeComponent,
    //CounterComponent,
    //FetchDataComponent,
    FetchContactComponent,
    CreateContactComponent,
    //CreateContact,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      //{ path: '', component: HomeComponent, pathMatch: 'full' },
      //{ path: 'counter', component: CounterComponent },
      //{ path: 'fetch-data', component: FetchDataComponent },
      { path: '', component: FetchContactComponent },
      { path: 'fetch-data-contact', component: FetchContactComponent},
      { path: 'create-new-contact', component: CreateContactComponent },
      { path: 'create-new-contact/:id', component: CreateContactComponent },
      ////{ path: 'register-contact', component: CreateContact },
      ////{ path: 'contact/edit/:id', component: CreateContact },
      //{ path: 'contact-add', component: contactAddComponent },
      //{ path: 'product-edit/:id', component: contactAddComponent }

    ])
  ],
  providers: [HttpClientModule, ContactService],
  bootstrap: [AppComponent]
})
export class AppModule { }
