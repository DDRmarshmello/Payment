"use client";
import { useRouter } from 'next/navigation'
import { Badge } from "@/components/ui/badge";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import React, { useEffect, useState } from "react";
import { PayrollDetail, PayrollResponse } from "@/lib/Types";
import apiService from "@/service/axiosClient";

export default function TablePayroll() {
  const [data, setData] = useState<PayrollDetail[]>(); // Estado para almacenar los datos de la API
  const [loading, setLoading] = useState(true); // Estado de carga
  const [error, setError] = useState(null); // Estado de error
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize] = useState(10); // Número de elementos por página
  const [totalPages, setTotalPages] = useState(0);

  const router = useRouter()

  // Función para obtener usuarios
  const fetchData = async () => {
    try {
      // Comienza la carga
      setLoading(true);
      // Llama al método genérico de apiService
      const data = await apiService.get<PayrollResponse>("/Payroll/PayrollEntry");
      // Guarda los usuarios en el estado 
      setData(data.payrollDetails); 
      // Calculo y seteo la cantidad de panginas de la tabla.
      setTotalPages(Math.ceil(data.payrollDetails.length / pageSize));
    } catch (err: any) {
      // Guarda el error si ocurre
      setError(err.message); 
    } finally {
      // Finaliza la carga
      setLoading(false); 
    }
  };

  useEffect(() => {
    fetchData();
  }, []); 
  // El array vacío indica que solo se ejecuta cuando el componente se monta

  // Función para obtener los datos paginados
  const getPaginatedData = () => {
    const startIndex = (currentPage - 1) * pageSize;
    const endIndex = startIndex + pageSize;
    // Divide los datos en base a la página
    return data.slice(startIndex, endIndex); 
  };

  // Función para cambiar de página
  const handlePageChange = (newPage: any) => {
    if (newPage > 0 && newPage <= totalPages) {
      setCurrentPage(newPage);
    }
  };

  if (loading) {
    return <div>Cargando...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <Card x-chunk="dashboard-05-chunk-3">
      <CardHeader className="px-7">
        <CardTitle>Payroll Details</CardTitle>
        <CardDescription>Recent payroll details.</CardDescription>
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Empleado</TableHead>
              <TableHead className="hidden sm:table-cell">Cedula</TableHead>
              <TableHead className="hidden sm:table-cell">Cargo</TableHead>
              <TableHead className="hidden md:table-cell">Salario</TableHead>
              <TableHead className="text-right">Total</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {getPaginatedData().map((emp: PayrollDetail) => {
              return (
                <TableRow
                  className=""
                  onClick={() => router.push(`/details/${emp.empDto.numEntry}`)}
                >
                  <TableCell>
                    <div className="font-medium">{emp.empDto.nombre}</div>
                    <div className="hidden text-sm text-muted-foreground md:inline">
                      {emp.empDto.apellido}
                    </div>
                  </TableCell>
                  <TableCell className="hidden sm:table-cell">
                    {emp.empDto.cedula}
                  </TableCell>
                  <TableCell className="hidden sm:table-cell">
                    <Badge className="text-xs" variant="secondary">
                      {emp.empDto.cargo}
                    </Badge>
                  </TableCell>
                  <TableCell className="hidden md:table-cell">
                    {emp.empDto.salary}
                  </TableCell>
                  <TableCell className="text-right">
                    $ {emp.payrollAmount}
                  </TableCell>
                </TableRow>
              );
            })}

            <div className="flex justify-between mt-4">
              <Button
                onClick={() => handlePageChange(currentPage - 1)}
                disabled={currentPage === 1}
              >
                Anterior
              </Button>

              <p>
                Página {currentPage} de {totalPages}
              </p>

              <Button
                onClick={() => handlePageChange(currentPage + 1)}
                disabled={currentPage === totalPages}
              >
                Siguiente
              </Button>
            </div>
          </TableBody>
        </Table>
      </CardContent>
    </Card>
  );
}
