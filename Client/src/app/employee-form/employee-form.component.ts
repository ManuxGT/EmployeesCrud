import { Component, Inject, OnInit } from '@angular/core';
import { Employee, EmployeeValidations } from '../models/employee.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EmployeeService } from '../services/employee.service';
import { tap } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalService } from '../services/modal.service';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.scss'],
})
export class EmployeeFormComponent implements OnInit {
  public employee!: Employee;
  public editMode: boolean = false;
  public formGroup: FormGroup;
  public title: string = '';
  public disableButton: boolean = false;

  private readonly _updateTitle: string = 'Update employee';
  private readonly _addTitle: string = 'Add employee';

  private readonly _formBuilder: FormBuilder = new FormBuilder();

  constructor(
    @Inject(MAT_DIALOG_DATA) private readonly _data: Employee,
    private readonly _employeeService: EmployeeService,
    private readonly _modalService: ModalService,
    public _dialogRef: MatDialogRef<EmployeeFormComponent>
  ) {
    this.formGroup = this._buildForm();
  }

  ngOnInit(): void {
    this._getEmployeeData();
    this._subscribeToFormChanges();
  }

  private _subscribeToFormChanges() {
    this.formGroup.valueChanges
      .pipe(
        tap((value) => {
          this.employee = this.employee
            ? Object.assign(this.employee, value)
            : value;

          this.employee.personalInformation = this.employee.personalInformation
            ? Object.assign(this.employee.personalInformation, value)
            : value;
        })
      )
      .subscribe();
  }

  private _getEmployeeData() {
    if (this._data) {
      this.editMode = true;
      this._employeeService
        .getById(this._data.id)
        .pipe(
          tap({
            next: (employeeData: Employee) => {
              this.employee = employeeData;
              this._patchValues(employeeData);
            },
            error: () => {
              this._modalService.createErrorModal(
                'Error while getting employee information'
              );
            },
          })
        )
        .subscribe();
    }

    this.title = this.editMode ? this._updateTitle : this._addTitle;
  }

  private _patchValues(employee: Employee) {
    const valueToPatch = {
      name: employee.name,
      lastname: employee.lastname,
      position: employee.position,
      hiredDate: this.formatDate(employee.hiredDate),
      birthDate: this.formatDate(employee.personalInformation.birthDate),
      address: employee.personalInformation.address,
      email: employee.personalInformation.email,
      phoneNumber: employee.personalInformation.phoneNumber,
    };

    this.formGroup.patchValue(valueToPatch);
  }

  private _buildForm(): FormGroup {
    const formGroup = this._formBuilder.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(EmployeeValidations.nameValidation.minLength),
          Validators.maxLength(EmployeeValidations.nameValidation.maxLength),
          Validators.pattern(EmployeeValidations.nameValidation.regex!),
        ],
      ],
      lastname: [
        '',
        [
          Validators.required,
          Validators.minLength(EmployeeValidations.lastname.minLength),
          Validators.maxLength(EmployeeValidations.lastname.maxLength),
          Validators.pattern(EmployeeValidations.lastname.regex!),
        ],
      ],
      position: ['', [Validators.required]],
      hiredDate: ['', [Validators.required]],
      birthDate: ['', [Validators.required]],
      phoneNumber: [
        '',
        [
          Validators.required,
          Validators.minLength(EmployeeValidations.phoneNumber.minLength),
          Validators.maxLength(EmployeeValidations.phoneNumber.maxLength),
          Validators.pattern(EmployeeValidations.phoneNumber.regex!),
        ],
      ],
      email: [
        '',
        [
          Validators.required,
          Validators.minLength(EmployeeValidations.email.minLength),
          Validators.maxLength(EmployeeValidations.email.maxLength),
          Validators.pattern(EmployeeValidations.email.regex!),
        ],
      ],
      address: ['', [Validators.required]],
    });

    return formGroup;
  }

  public onSubmit(): void {
    if (this.formGroup.valid) {
      this.disableButton = true;
      switch (this.editMode) {
        case true:
          this._editEmployee();
          break;

        case false:
          this._createEmployee();
          break;
      }
    }
  }

  private _createEmployee(): void {
    this._employeeService.create(this.employee).subscribe({
      next: () => {
        this._modalService.createSuccessModal('Employee created successfully');
        this._closeModal();
      },
      error: (errorData) => {
        this._modalService.createErrorModal(errorData.error.message);
        this.disableButton = false;
      },
    });
  }

  private _editEmployee(): void {
    this._employeeService.update(this.employee).subscribe({
      next: () => {
        this._modalService.createSuccessModal('Employee updated successfully');
        this._closeModal();
      },
      error: (errorData) => {
        this._modalService.createErrorModal(errorData.error.message);
        this.disableButton = false;
      },
    });
  }

  private _closeModal(): void {
    this._dialogRef.close();
  }

  private formatDate(date: Date) {
    const d = new Date(date);
    let month = '' + (d.getMonth() + 1);
    let day = '' + d.getDate();
    const year = d.getFullYear();
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    return [year, month, day].join('-');
  }
}
