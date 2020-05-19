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
import { ModalModule } from 'ngx-bootstrap/modal';
import { NotifierModule, NotifierOptions } from 'angular-notifier';

/**
 * Custom angular notifier options
 */
const customNotifierOptions: NotifierOptions = {
  position: {
    horizontal: {
      position: 'left',
      distance: 12
    },
    vertical: {
      position: 'bottom',
      distance: 12,
      gap: 10
    }
  },
  theme: 'material',
  behaviour: {
    autoHide: 5000,
    onClick: 'hide',
    onMouseover: 'pauseAutoHide',
    showDismissButton: true,
    stacking: 4
  },
  animations: {
    enabled: true,
    show: {
      preset: 'slide',
      speed: 300,
      easing: 'ease'
    },
    hide: {
      preset: 'fade',
      speed: 300,
      easing: 'ease',
      offset: 50
    },
    shift: {
      speed: 300,
      easing: 'ease'
    },
    overlap: 150
  }
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FetchContactComponent,
    CreateContactComponent,
    //NotifierModule,

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NotifierModule.withConfig(customNotifierOptions),
    ModalModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: FetchContactComponent },
      { path: 'fetch-data-contact', component: FetchContactComponent},
      { path: 'create-new-contact', component: CreateContactComponent },
      { path: 'create-new-contact/:id', component: CreateContactComponent },
      ////{ path: 'register-contact', component: CreateContact },
      ////{ path: 'contact/edit/:id', component: CreateContact },
      //{ path: 'contact-add', component: contactAddComponent },
      //{ path: 'product-edit/:id', component: contactAddComponent },
            //{ path: '', component: HomeComponent, pathMatch: 'full' },
      //{ path: 'counter', component: CounterComponent },
      //{ path: 'fetch-data', component: FetchDataComponent },

    ])
  ],
  providers: [HttpClientModule, ContactService],
  bootstrap: [AppComponent]
})
export class AppModule { }
