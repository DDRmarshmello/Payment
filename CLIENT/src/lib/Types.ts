export type PayrollResponse = {
  totalAmount: number;
  payrollId: string;
  payrollType: number;
  payrollDescription: string;
  payrollDate: string;
  payrollPeriodStar: string;
  payrollPeriodEnd: string;
  payrollPeriodPay: string;
  payrollPeriodProcces: number;
  totalEmps: number;
  payrollDetails: PayrollDetail[];
};

export type PayrollDetail = {
  empDto: EmpDto;
  totalAmount: number;
  totalTax: number;
  totalProfits: number;
  payrollAmount: number;
};

export type EmpDto = {
  numEntry: number;
  nombre: string;
  apellido: string;
  cedula: string;
  fechaNacimiento: string;
  compania: number;
  cargo: number;
  salary: number;
  emIngs: EmIng[];
  emDescs: EmDesc[];
};

export type EmIng = {
  numEntry: number;
  monto: number;
  createdAt: string;
  aplicationDate: string;
  numEmp: number;
  emTypeIng: number;
  emTypeIngNavigation: EmTypeIngNavigation;
};

export type EmTypeIngNavigation = {
  numEntry: number;
  descripcion: string;
  configuracion: number;
  thisTax: boolean;
  thisExempt: boolean;
  thisTaxMedical: boolean;
  thisLaborBenefits: boolean;
  thisChristmasSalary: boolean;
};

export type EmDesc = {
  numEntry: number;
  monto: number;
  createdAt: string;
  aplicationDate: string;
  numEmp: number;
  emTypeDesc: number;
  emTypeDescNavigation: EmTypeDescNavigation;
};

export type EmTypeDescNavigation = {
  numEntry: number;
  descripcion: string;
  configuracion: number;
  thisLoan: boolean;
  numQuotas: number;
  thisPercentage: number;
};
