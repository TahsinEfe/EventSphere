
import { useState } from "react";
import { useSearchParams } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Search } from "lucide-react";
import { EventType } from "@/types";
import { getEventTypeLabel } from "@/data/mockData";

const EventFilter = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  const [searchTerm, setSearchTerm] = useState(searchParams.get("q") || "");
  
  const currentType = searchParams.get("type");
  
  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    const params = new URLSearchParams(searchParams);
    
    if (searchTerm) {
      params.set("q", searchTerm);
    } else {
      params.delete("q");
    }
    
    setSearchParams(params);
  };
  
  const handleTypeFilter = (type: string | null) => {
    const params = new URLSearchParams(searchParams);
    
    if (type) {
      params.set("type", type);
    } else {
      params.delete("type");
    }
    
    setSearchParams(params);
  };
  
  // Generate event type options
  const eventTypes = [
    EventType.Concert,
    EventType.Conference,
    EventType.Theater,
    EventType.Sport,
    EventType.Festival,
    EventType.Other,
  ];

  return (
    <div className="space-y-4">
      <form onSubmit={handleSearch} className="flex w-full max-w-sm items-center space-x-2">
        <Input
          type="text"
          placeholder="Search events..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="flex-1"
        />
        <Button type="submit" size="icon">
          <Search className="h-4 w-4" />
          <span className="sr-only">Search</span>
        </Button>
      </form>
      
      <div className="flex flex-wrap gap-2">
        <Button
          variant={!currentType ? "default" : "outline"}
          size="sm"
          onClick={() => handleTypeFilter(null)}
        >
          All
        </Button>
        
        {eventTypes.map((type) => (
          <Button
            key={type}
            variant={currentType === getEventTypeLabel(type) ? "default" : "outline"}
            size="sm"
            onClick={() => handleTypeFilter(getEventTypeLabel(type))}
          >
            {getEventTypeLabel(type)}
          </Button>
        ))}
      </div>
    </div>
  );
};

export default EventFilter;
