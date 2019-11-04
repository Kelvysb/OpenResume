import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ThrowStmt } from '@angular/compiler';

@Component({
  selector: 'openr-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {

  @Input() Message: string = "";
  @Input() Error: boolean = true;
  @Input() Timeout: number = 3000;
  ShowMessage: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  Show (message: string, error: boolean, timeout?: number)
  {
    this.Message = message;
    this.Error = error;
    if(timeout !== undefined)
    {
      this.Timeout = timeout;
    }
    else 
    {
      this.Timeout = 3000;
    }

    this.ShowMessage = true;

    if(this.Timeout != 0)
    {
      setTimeout(() => { this.Hide(); }, this.Timeout);
    }

  }

  Hide() 
  {
    this.ShowMessage = false;
  }

}
