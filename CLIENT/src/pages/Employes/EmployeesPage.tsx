'use client'
import { Metadata } from "next"
import Image from "next/image"

import { columns } from "@/app/employe/components/columns"
import { DataTable } from "@/app/employe/components/data-table"
import { UserNav } from "@/app/employe/components/user-nav"
import { useEffect, useState } from "react"
import apiService from "@/service/axiosClient"
import { Employee } from "@/lib/Types"

export const metadata: Metadata = {
  title: "Tasks",
  description: "A task and issue tracker build using Tanstack Table.",
}

// Simulate a database read for tasks.

export function EmployeesPage() {
  const [data, setData] = useState<Employee[]>(); // Estado para almacenar los datos de la API
  const [loading, setLoading] = useState(true); // Estado de carga
  const [error, setError] = useState(null); // Estado de error
  const fetchData = async () => {
    try {
      // Comienza la carga
      setLoading(true);
      console.log("haciendo fetching");
      // Llama al método genérico de apiService
      const data = await apiService.get<Employee>(
        `/emp/EmployeeResponse`
      );
      console.log(data);
      // Guarda los usuarios en el estado
      setData(data);
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

  if (loading) {
    return <>Cargandoooo</>;
  }

  if (error) {
    return (
      <main className="flex flex-1 flex-col gap-4 p-4 md:gap-8 md:p-8">
        <div>Error: {error}</div>;
      </main>
    );
  }
  return (
    <>
      <div className="md:hidden">
        <Image
          src="/examples/tasks-light.png"
          width={1280}
          height={998}
          alt="Playground"
          className="block dark:hidden"
        />
        <Image
          src="/examples/tasks-dark.png"
          width={1280}
          height={998}
          alt="Playground"
          className="hidden dark:block"
        />
      </div>
      <div className="hidden h-full flex-1 flex-col space-y-8 p-8 md:flex">
        <div className="flex items-center justify-between space-y-2">
          <div>
            <h2 className="text-2xl font-bold tracking-tight">Welcome back!</h2>
            <p className="text-muted-foreground">
            Here's a list of your employees!
            </p>
          </div>
          <div className="flex items-center space-x-2">
            <UserNav />
          </div>
        </div>
        <DataTable data={data} columns={columns} />
      </div>
    </>
  )
}
