import { DetailsEmp } from "@/pages/Details/DetailEmp";

export default function Page({ params }: { params: { id: string } }) {
  return <><DetailsEmp emp={params.id} /></>
}