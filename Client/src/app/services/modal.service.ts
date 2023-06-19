import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class ModalService {
  constructor() {}

  public createSuccessModal(title: string, duration: number = 2000) {
    Swal.fire({
      icon: 'success',
      title: title,
      showConfirmButton: false,
      timer: duration
    })
  }

  public createErrorModal(errorMessage: string) {
    Swal.fire({
      icon: 'error',
      title: errorMessage
    });
  }
}
