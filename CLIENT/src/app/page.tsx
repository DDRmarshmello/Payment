import { Dashboard } from "@/components/Dashboard";
import { TooltipProvider } from "@/components/ui/tooltip";
import Image from "next/image";

export default function Home() {
  return (
    <div>
      <TooltipProvider>
        <Dashboard /> 
      </TooltipProvider>
    </div>
  );
}
