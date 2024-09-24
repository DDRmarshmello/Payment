import { ColumnDef } from "@tanstack/react-table";
import { DataTableColumnHeader } from "./data-table-column-header";
import { DataTableRowActions } from "./data-table-row-actions";
import { Employee } from "@/lib/Types";
import { Checkbox } from "@/components/ui/checkbox";
// Definir la interfaz del modelo actualizado


export const columns: ColumnDef<Employee>[] = [
  {
    id: "select",
    header: ({ table }) => (
      <Checkbox
        checked={
          table.getIsAllPageRowsSelected() ||
          (table.getIsSomePageRowsSelected() && "indeterminate")
        }
        onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
        aria-label="Select all"
        className="translate-y-[2px]"
      />
    ),
    cell: ({ row }) => (
      <Checkbox
        checked={row.getIsSelected()}
        onCheckedChange={(value) => row.toggleSelected(!!value)}
        aria-label="Select row"
        className="translate-y-[2px]"
      />
    ),
    enableSorting: false,
    enableHiding: false,
  },
  {
    accessorKey: "numEntry",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Num Entry" />
    ),
    cell: ({ row }) => <div>{row.getValue("numEntry")}</div>,
  },
  {
    accessorKey: "nombre",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Nombre" />
    ),
    cell: ({ row }) => <div>{row.getValue("nombre")}</div>,
  },
  {
    accessorKey: "apellido",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Apellido" />
    ),
    cell: ({ row }) => <div>{row.getValue("apellido")}</div>,
  },
  {
    accessorKey: "cedula",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Cédula" />
    ),
    cell: ({ row }) => <div>{row.getValue("cedula")}</div>,
  },
  {
    accessorKey: "fechaNacimiento",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Fecha de Nacimiento" />
    ),
    cell: ({ row }) => (
      <div>{new Date(row.getValue("fechaNacimiento")).toLocaleDateString()}</div>
    ),
  },
  {
    accessorKey: "compania",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Compañía" />
    ),
    cell: ({ row }) => <div>{row.getValue("compania")}</div>,
  },
  {
    accessorKey: "cargo",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Cargo" />
    ),
    cell: ({ row }) => <div>{row.getValue("cargo")}</div>,
  },
  {
    accessorKey: "salary",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Salario" />
    ),
    cell: ({ row }) => <div>{row.getValue("salary")}</div>,
  },
  {
    accessorKey: "departament",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Departamento" />
    ),
    cell: ({ row }) => <div>{row.getValue("departament")}</div>,
  },
  {
    id: "actions",
    cell: ({ row }) => <DataTableRowActions row={row} />,
  },
];
