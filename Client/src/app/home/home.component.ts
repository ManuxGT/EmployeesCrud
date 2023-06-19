import { Component, OnInit } from '@angular/core';
import { StudentService } from '../services/student.service';
import { Student } from '../models/student.model';
import { tap } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { StudentFormComponent } from '../student-form/student-form.component';
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
  public allStudents$: BehaviorSubject<Array<Student>> = new BehaviorSubject<Array<Student>>([]);
  public readonly displayedColumns: string[] = [
    'id',
    'name',
    'lastname',
    'email',
    'actions',
  ];

  constructor(
    private readonly _studentService: StudentService,
    private readonly _dialog: MatDialog,
    private readonly _modalService: ModalService
  ) {}

  ngOnInit(): void {
    this._bootstrap().subscribe();
  }

  private _bootstrap() {
    return this._studentService.getAll().pipe(
      tap({
        next: (students: Student[]) => {
          this.allStudents$.next(students);
        },
        error: () => {
          this._modalService.createErrorModal('Error getting all students');
        },
      })
    );
  }

  public onRefresh() {
    this._bootstrap().subscribe();
  }

  public onCreateStudent(): void {
    const modalRef = this._dialog.open(StudentFormComponent, {
      width: '560px',
      disableClose: true,
    });

    modalRef.afterClosed().subscribe(() => {
      this._bootstrap().subscribe();
    });
  }

  public onDeleteStudent(student: Student): void {
    const modalRef = this._dialog.open(ConfirmDeleteModalContentComponent, {
      width: '350px',
    });

    modalRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this._performDelete(student.id);
      }
    });
  }

  private _performDelete(studentId: string): void {
    this._studentService
      .delete(studentId)
      .pipe(
        tap({
          next: () => {
            this._modalService.createSuccessModal(
              'Student deleted successfully'
            );
            this.onRefresh();
          },
          error: () => {
            this._modalService.createErrorModal(
              'Error while deleting this student'
            );
          },
        })
      )
      .subscribe();
  }

  public onEditStudent(student: Student): void {
    const modalRef = this._dialog.open(StudentFormComponent, {
      data: student,
      width: '560px',
      disableClose: true,
    });

    modalRef.afterClosed().subscribe(() => {
      this._bootstrap().subscribe();
    });
  }
}
