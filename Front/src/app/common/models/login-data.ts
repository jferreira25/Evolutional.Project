import { RoleEnum } from '../enuns/role.enum'

export class LoginData {
  email: string;
  role: number;

  constructor(
    email: string,
    role: string
  ) {
    this.email = email;
    this.role = +role;
  }
}