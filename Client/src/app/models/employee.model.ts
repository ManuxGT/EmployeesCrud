import { EmployeeStatus } from "../enums/employee-status.enum";
import { PersonalInformation } from "./personal-information.model";
import { FieldValidation } from "./validation.model";

export interface Employee {
  id: string;
  name: string; //
  lastname: string; //
  photoBase64: string;
  position: string; //
  hiredDate: Date; //
  status: EmployeeStatus
  personalInformation: PersonalInformation;
}

export  class EmployeeValidations {
  public static nameValidation: FieldValidation = {
    minLength: 2,
    maxLength: 20,
    regex: /^[a-z ,.'-]+$/i
  };

  public static lastname: FieldValidation = {
    minLength: 2,
    maxLength: 20,
    regex: /^[a-z ,.'-]+$/i
  };

  public static email: FieldValidation = {
    minLength: 5,
    maxLength: 50,
    regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
  };

  public static phoneNumber: FieldValidation = {
    minLength: 4,
    maxLength: 18,
    regex: /^[0-9]+$/i
  };
}
