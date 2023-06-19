import { Component, Inject, OnInit } from '@angular/core';
import { Student, StudentValidations } from '../models/student.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { StudentService } from '../services/student.service';
import { tap } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalService } from '../services/modal.service';

@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.scss'],
})
export class StudentFormComponent implements OnInit {
  public student!: Student;
  public editMode: boolean = false;
  public formGroup: FormGroup;
  public title: string = '';
  public disableButton: boolean = false;

  private readonly _updateTitle: string = 'Update student';
  private readonly _addTitle: string = 'Add student';

  private readonly _formBuilder: FormBuilder = new FormBuilder();

  constructor(
    @Inject(MAT_DIALOG_DATA) private readonly _data: Student,
    private readonly _studentService: StudentService,
    private readonly _modalService: ModalService,
    public _dialogRef: MatDialogRef<StudentFormComponent>
  ) {
    this.formGroup = this._buildForm();
  }

  ngOnInit(): void {
    this._getStudentData();
    this._subscribeToFormChanges();
  }

  private _subscribeToFormChanges() {
    this.formGroup.valueChanges
      .pipe(
        tap((value) => {
          this.student = this.student
            ? Object.assign(this.student, value)
            : value;
        })
      )
      .subscribe();
  }

  private _getStudentData() {
    if (this._data) {
      this.editMode = true;
      this._studentService
        .getById(this._data.id)
        .pipe(
          tap({
            next: (studentData: Student) => {
              this.student = studentData;
              this._patchValues();
            },
            error: () => {
              this._modalService.createErrorModal(
                'Error while getting student information'
              );
            },
          })
        )
        .subscribe();
    }

    this.title = this.editMode ? this._updateTitle : this._addTitle;
  }

  private _patchValues() {
    this.formGroup.patchValue(this.student);
  }

  private _buildForm(): FormGroup {
    const formGroup = this._formBuilder.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(StudentValidations.nameValidation.minLength),
          Validators.maxLength(StudentValidations.nameValidation.maxLength),
          Validators.pattern(StudentValidations.nameValidation.regex!),
        ],
      ],
      lastname: [
        '',
        [
          Validators.required,
          Validators.minLength(StudentValidations.lastname.minLength),
          Validators.maxLength(StudentValidations.lastname.maxLength),
          Validators.pattern(StudentValidations.lastname.regex!),
        ],
      ],
      age: [
        '',
        [
          Validators.required,
          Validators.min(StudentValidations.age.minLength),
          Validators.max(StudentValidations.age.maxLength),
        ],
      ],
      phoneNumber: [
        '',
        [
          Validators.required,
          Validators.minLength(StudentValidations.phoneNumber.minLength),
          Validators.maxLength(StudentValidations.phoneNumber.maxLength),
          Validators.pattern(StudentValidations.phoneNumber.regex!),
        ],
      ],
      email: [
        '',
        [
          Validators.required,
          Validators.minLength(StudentValidations.email.minLength),
          Validators.maxLength(StudentValidations.email.maxLength),
          Validators.pattern(StudentValidations.email.regex!),
        ],
      ],
    });

    return formGroup;
  }

  public onSubmit(): void {
    if (this.formGroup.valid) {
      this.disableButton = true;
      switch (this.editMode) {
        case true:
          this._editStudent();
          break;

        case false:
          this._createStudent();
          break;
      }
    }
  }

  private _createStudent(): void {
    this._studentService.create(this.student).subscribe({
      next: () => {
        this._modalService.createSuccessModal('Student created successfully');
        this._closeModal();
      },
      error: (errorData) => {
        this._modalService.createErrorModal(errorData.error.message);
        this.disableButton = false;
      },
    });
  }

  private _editStudent(): void {
    this._studentService.update(this.student).subscribe({
      next: () => {
        this._modalService.createSuccessModal('Student updated successfully');
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
}
