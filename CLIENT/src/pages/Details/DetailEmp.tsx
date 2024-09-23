"use client";
import Link from "next/link";
import { useState, useEffect } from "react";
import {
  Activity,
  ArrowUpRight,
  CreditCard,
  DollarSign,
  Users,
} from "lucide-react";

import { Button } from "@/components/ui/button";
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
import { PayrollDetail, EmIng, EmDesc } from "@/lib/Types";
import apiService from "@/service/axiosClient";
import { CurrencyFormat, StringToDate } from "@/lib/utils";
import { Skeleton } from "../../components/ui/skeleton";
import { DetailsSkeleton } from "./DetailsSkeleton";

export const description =
  "An application shell with a header and main content area. The header has a navbar, a search input and and a user nav dropdown. The user nav is toggled by a button with an avatar image.";

export function DetailsEmp({ emp }: { emp: string }) {
  const [data, setData] = useState<PayrollDetail>(); // Estado para almacenar los datos de la API
  const [loading, setLoading] = useState(true); // Estado de carga
  const [error, setError] = useState(null); // Estado de error
  const fetchData = async () => {
    try {
      // Comienza la carga
      setLoading(true);
      console.log("haciendo fetching");
      // Llama al método genérico de apiService
      const data = await apiService.get<PayrollDetail>(
        `/Payroll/PayrollEntry/emp/${emp}`
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
    return <DetailsSkeleton />;
  }

  if (error) {
    return (
      <main className="flex flex-1 flex-col gap-4 p-4 md:gap-8 md:p-8">
        <div>Error: {error}</div>;
      </main>
    );
  }

  return (
    <main className="flex flex-1 flex-col gap-4 p-4 md:gap-8 md:p-8">
      <div className="grid gap-4 md:grid-cols-2 md:gap-8 lg:grid-cols-4">
        <Card x-chunk="dashboard-01-chunk-0">
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Total Payment</CardTitle>
            <DollarSign className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">$ {CurrencyFormat(data?.payrollAmount)}</div>
            <p className="text-xs text-muted-foreground">
              +20.1% from last month
            </p>
          </CardContent>
        </Card>
        <Card x-chunk="dashboard-01-chunk-1">
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">
              Total Discounts
            </CardTitle>
            <Users className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">$ {CurrencyFormat(data?.totalAmount)}</div>
            <p className="text-xs text-muted-foreground">
              +180.1% from last month
            </p>
          </CardContent>
        </Card>
        <Card x-chunk="dashboard-01-chunk-2">
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Sales</CardTitle>
            <CreditCard className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">+12,234</div>
            <p className="text-xs text-muted-foreground">
              +19% from last month
            </p>
          </CardContent>
        </Card>
        <Card x-chunk="dashboard-01-chunk-3">
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Active Now</CardTitle>
            <Activity className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">+573</div>
            <p className="text-xs text-muted-foreground">
              +201 since last hour
            </p>
          </CardContent>
        </Card>
      </div>
      <div className="grid gap-4 md:gap-8 lg:grid-cols-2 xl:grid-cols-2">
        <Card className="xl:col-span-2" x-chunk="dashboard-01-chunk-4">
          <CardHeader className="flex flex-row items-center">
            <div className="grid gap-2">
              <CardTitle>Transactions</CardTitle>
              <CardDescription>
                Recent transactions from your payroll.
              </CardDescription>
            </div>
            <Button asChild size="sm" className="ml-auto gap-1">
              <Link href="#">
                View All
                <ArrowUpRight className="h-4 w-4" />
              </Link>
            </Button>
          </CardHeader>
          <CardContent>
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>Transactions</TableHead>
                  <TableHead className="text-center">Date</TableHead>
                  <TableHead className="text-right">Amount</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {data.empDto.emIngs.map((ing: EmIng) => {
                  return (
                    <TableRow>
                      <TableCell>
                        <div className="font-medium">
                          {ing.emTypeIngNavigation.descripcion}
                        </div>
                        <div className="">{StringToDate(ing.createdAt)}</div>
                      </TableCell>
                      <TableCell className="text-center">
                        {StringToDate(ing.aplicationDate)}
                      </TableCell>
                      <TableCell className="text-right">
                        $ {CurrencyFormat(ing.monto)}
                      </TableCell>
                    </TableRow>
                  );
                })}

                {data.empDto.emDescs.map((desc: EmDesc) => {
                  return (
                    <TableRow>
                      <TableCell>
                        <div className="font-medium">
                          {desc.emTypeDescNavigation.descripcion}
                        </div>
                        <div className="">{StringToDate(desc.createdAt)}</div>
                      </TableCell>
                      <TableCell className="text-center">
                        {StringToDate(desc.aplicationDate)}
                      </TableCell>
                      <TableCell className="text-right">
                        $ {CurrencyFormat(desc.monto)}
                      </TableCell>
                    </TableRow>
                  );
                })}
              </TableBody>
            </Table>
          </CardContent>
        </Card>
      </div>
    </main>
  );
}
