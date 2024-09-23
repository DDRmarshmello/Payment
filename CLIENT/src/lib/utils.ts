import { clsx, type ClassValue } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function StringToDate( date : string) {
  const dt = new Date(date);
  const formatoFecha = dt.toLocaleDateString('es-ES', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
  });

  return formatoFecha.toString()
}
export function CurrencyFormat (Currency : number) {
  const formattedCurrency = new Intl.NumberFormat('es-DO', {
    currency: 'DOP',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  }).format(Currency);

  return formattedCurrency;
}