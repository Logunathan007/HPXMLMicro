import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  //variables initialization
  headerNavs:any;
  selectedNav:any;

  constructor() {
    this.variableDeclaration();
  }

  //variable default declaration
  variableDeclaration(){
    this.headerNavs = [
      {
        name: "Address",
        routerLink: "address"
      },
      {
        name: "About",
        routerLink: "about"
      },
      {
        name: "Zones",
        routerLink: "zones",
      },
      {
        name: "Systems",
        routerLink: "systems",
      }
    ]
    this.selectedNav = this.headerNavs[0];
  }


}
