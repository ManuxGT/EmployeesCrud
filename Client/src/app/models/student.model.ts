import { FieldValidation } from "./validation.model";

export interface Student {
  id: string;
  name: string;
  lastname: string;
  age: number;
  email: string;
  phoneNumber: string;
}

export  class StudentValidations {
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

  public static age: FieldValidation = {
    minLength: 18,
    maxLength: 100
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
