import { Directive, ElementRef, Input, OnInit, OnDestroy, Renderer2 } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { Subscription } from 'rxjs';

@Directive({
  selector: '[showError]'
})
export class ShowErrorDirective implements OnInit, OnDestroy {

  @Input('control')
  control!: AbstractControl;

  private subscribeStatus!: Subscription;

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  ngOnInit(): void {
    this.subscribeStatus = this.control.valueChanges.subscribe(() => {
      this.checkError();
    });
  }

  checkError() {
    if (this.control) {
      if (this.control.touched || (this.control.dirty && this.control.invalid)) {
        this.setErrorMsg(this.control.errors);
      }
    }
  }

  setErrorMsg(err: any) {
    let msg = '';
    if (err) {
      if (err['required']) {
        msg = 'This is a required field.';
      } else if (err['min']) {
        msg = `Minimum value is ${err['min']['min']}`;
      } else if (err['max']) {
        msg = `Maximum value is ${err['max']['max']}`;
      } else if (err['nameSame']) {
        msg = 'Name must be unique.';
      } else {
        msg = 'Unknown error.';
      }
    }
    // Use Renderer2 to safely update innerText
    console.log(msg);
    this.renderer.setProperty(this.el.nativeElement, 'innerText', msg);
  }

  ngOnDestroy(): void {
    if (this.subscribeStatus) {
      this.subscribeStatus.unsubscribe();
    }
  }
}
