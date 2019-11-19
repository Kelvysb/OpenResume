import { MatSnackBarConfig, MatSnackBar, MatSnackBarVerticalPosition, MatSnackBarHorizontalPosition } from '@angular/material/snack-bar';
import { NgModule, Injectable } from '@angular/core';

@Injectable()
@NgModule()
export class NotifyService {
    constructor(private snackBar: MatSnackBar) { }

    verticalPosition: MatSnackBarVerticalPosition = 'top';
    horizontalPosition: MatSnackBarHorizontalPosition = 'center';

    notify(message: string, action: string, timeout: number) {
        const config = new MatSnackBarConfig();
        config.duration = timeout;
        config.verticalPosition = this.verticalPosition;
        config.horizontalPosition = this.horizontalPosition;
        this.snackBar.open(message, action, config);
    }
}
