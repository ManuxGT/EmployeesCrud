import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-delete-modal-content',
  templateUrl: './confirm-delete-modal-content.component.html',
  styleUrls: ['./confirm-delete-modal-content.component.scss']
})
export class ConfirmDeleteModalContentComponent {

  constructor(private readonly _dialogRef: MatDialogRef<ConfirmDeleteModalContentComponent>) {}

  public confirm() {
    this._dialogRef.close(true);
  }

  public deny() {
    this._dialogRef.close(false);
  }
}
