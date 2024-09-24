import { Dashboard } from "@/pages/Dash/Dashboard";
import { TooltipProvider } from "@/components/ui/tooltip";

export default function Home() {
  return (
    <div>
      <TooltipProvider>
        <Dashboard /> 
      </TooltipProvider>
    </div>
  );
}
