import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/employee.model';
import { tap } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeFormComponent } from '../employee-form/employee-form.component';
import { BehaviorSubject } from 'rxjs';
import { ConfirmDeleteModalContentComponent } from '../confirm-delete-modal-content/confirm-delete-modal-content.component';
import { ModalService } from '../services/modal.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  public title: string = '';
  public currentPage: number = 1;
  public allEmployees$: BehaviorSubject<Array<Employee>> = new BehaviorSubject<Array<Employee>>([]);
  public readonly displayedColumns: string[] = [
    'id',
    'name',
    'lastname',
    'email',
    'actions',
  ];

  constructor(
    private readonly _employeeService: EmployeeService,
    private readonly _dialog: MatDialog,
    private readonly _modalService: ModalService
  ) {}

  ngOnInit(): void {
    this._bootstrap().subscribe();
  }

  private _bootstrap() {
    return this._employeeService.getAll(this.currentPage).pipe(
      tap({
        next: (employees: Employee[]) => {
          this.allEmployees$.next(employees);
        },
        error: () => {
          this._modalService.createErrorModal('Error getting all employees');
        },
      })
    );
  }

  public onRefresh() {
    this._bootstrap().subscribe();
  }

  public onCreateEmployee(): void {
    const modalRef = this._dialog.open(EmployeeFormComponent, {
      width: '560px',
      disableClose: true,
    });

    modalRef.afterClosed().subscribe(() => {
      this._bootstrap().subscribe();
    });
  }

  public onDeleteEmployee(employee: Employee): void {
    const modalRef = this._dialog.open(ConfirmDeleteModalContentComponent, {
      width: '350px',
    });

    modalRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this._performDelete(employee.id);
      }
    });
  }

  private _performDelete(employeeId: string): void {
    this._employeeService
      .delete(employeeId)
      .pipe(
        tap({
          next: () => {
            this._modalService.createSuccessModal(
              'Employee deleted successfully'
            );
            this.onRefresh();
          },
          error: () => {
            this._modalService.createErrorModal(
              'Error while deleting this employee'
            );
          },
        })
      )
      .subscribe();
  }

  public onEditEmployee(employee: Employee): void {
    const modalRef = this._dialog.open(EmployeeFormComponent, {
      data: employee,
      width: '560px',
      disableClose: true,
    });

    modalRef.afterClosed().subscribe(() => {
      this._bootstrap().subscribe();
    });
  }
}
