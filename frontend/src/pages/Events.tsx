import { useState, useEffect } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Calendar, Search, Filter, X } from "lucide-react";
import EventCard from "@/components/EventCard";
import Footer from "@/components/Footer";
import Navbar from "@/components/Navbar";
import { Separator } from "@/components/ui/separator";
import { Checkbox } from "@/components/ui/checkbox";
import { Label } from "@/components/ui/label";
import { EventType } from "@/types";
import { Badge } from "@/components/ui/badge";
import { getEventTypeLabel } from "@/data/mockData";
import { EventsAPI } from "@/services/EventsAPI";
import { EventDto } from "@/types/EventDto";

const Events = () => {
    const [searchQuery, setSearchQuery] = useState("");
    const [events, setEvents] = useState<EventDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [selectedFilters, setSelectedFilters] = useState<{
        types: string[];
        dateRanges: string[];
    }>({
        types: [],
        dateRanges: [],
    });
    const [showFilterMenu, setShowFilterMenu] = useState(false);

    const eventTypes = [
        { id: EventType.Concert.toString(), label: getEventTypeLabel(EventType.Concert) },
        { id: EventType.Conference.toString(), label: getEventTypeLabel(EventType.Conference) },
        { id: EventType.Theater.toString(), label: getEventTypeLabel(EventType.Theater) },
        { id: EventType.Sport.toString(), label: getEventTypeLabel(EventType.Sport) },
        { id: EventType.Festival.toString(), label: getEventTypeLabel(EventType.Festival) },
        { id: EventType.Other.toString(), label: getEventTypeLabel(EventType.Other) },
    ];

    useEffect(() => {
        const fetchEvents = async () => {
            try {
                const res = await EventsAPI.getAll();
                setEvents(res.data);
            } catch (error) {
                console.error("Event fetch error:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchEvents();
    }, []);

    const toggleTypeFilter = (typeId: string) => {
        setSelectedFilters(prev => {
            const types = prev.types.includes(typeId)
                ? prev.types.filter(id => id !== typeId)
                : [...prev.types, typeId];
            return { ...prev, types };
        });
    };

    const clearAllFilters = () => {
        setSelectedFilters({
            types: [],
            dateRanges: [],
        });
        setSearchQuery("");
    };

    const filteredEvents = events.filter((event) => {
        const matchesSearch = event.name.toLowerCase().includes(searchQuery.toLowerCase()) ||
            (event.description?.toLowerCase().includes(searchQuery.toLowerCase()) ?? false);
        const matchesType = selectedFilters.types.length === 0 ||
            selectedFilters.types.includes(event.eventTypeId.toString());

        return matchesSearch && matchesType;
    });

    return (
        <div className="min-h-screen flex flex-col">
            <Navbar />

            <div className="container mx-auto px-4 py-8">
                <div className="text-center mb-8">
                    <h1 className="text-3xl md:text-4xl font-bold mb-4">Discover Events</h1>
                    <p className="text-muted-foreground max-w-2xl mx-auto">
                        Find and book tickets for the best events in your city, from concerts and theater shows to conferences and festivals.
                    </p>
                </div>

                <div className="flex flex-col md:flex-row gap-6 mb-8">
                    <div className="w-full md:w-2/3">
                        <div className="relative">
                            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground" />
                            <Input
                                placeholder="Search events by name, artist, venue..."
                                className="pl-10"
                                value={searchQuery}
                                onChange={(e) => setSearchQuery(e.target.value)}
                            />
                        </div>
                    </div>

                    <div className="w-full md:w-1/3 flex gap-2">
                        <Button
                            variant="outline"
                            className="flex-1"
                            onClick={() => setShowFilterMenu(!showFilterMenu)}
                        >
                            <Filter className="mr-2 h-4 w-4" />
                            {showFilterMenu ? "Hide Filters" : "Show Filters"}
                        </Button>
                        <Button onClick={clearAllFilters}>Reset</Button>
                    </div>
                </div>

                {(selectedFilters.types.length > 0) && (
                    <div className="flex flex-wrap gap-2 mb-4">
                        <div className="text-sm font-medium mr-2 flex items-center">Active filters:</div>
                        {selectedFilters.types.map(typeId => {
                            const type = eventTypes.find(t => t.id === typeId);
                            return type ? (
                                <Badge key={typeId} variant="secondary" className="flex items-center gap-1">
                                    {type.label}
                                    <X
                                        className="h-3 w-3 cursor-pointer"
                                        onClick={() => toggleTypeFilter(typeId)}
                                    />
                                </Badge>
                            ) : null;
                        })}
                    </div>
                )}

                <div className="flex flex-col lg:flex-row gap-6">
                    {showFilterMenu && (
                        <div className="w-full lg:w-1/4 p-4 border rounded-lg">
                            <h3 className="font-medium text-lg mb-3">Filter Events</h3>
                            <div className="mb-6">
                                <h4 className="font-medium mb-2">Event Type</h4>
                                <div className="space-y-2">
                                    {eventTypes.map(type => (
                                        <div key={type.id} className="flex items-center space-x-2">
                                            <Checkbox
                                                id={`type-${type.id}`}
                                                checked={selectedFilters.types.includes(type.id)}
                                                onCheckedChange={() => toggleTypeFilter(type.id)}
                                            />
                                            <Label htmlFor={`type-${type.id}`}>{type.label}</Label>
                                        </div>
                                    ))}
                                </div>
                            </div>
                        </div>
                    )}

                    <div className={`w-full ${showFilterMenu ? 'lg:w-3/4' : 'lg:w-full'}`}>
                        <div className="flex justify-between items-center mb-4">
                            <h2 className="text-xl font-semibold">All Events</h2>
                            <span className="text-muted-foreground">{filteredEvents.length} events found</span>
                        </div>

                        {filteredEvents.length > 0 ? (
                            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-3 gap-6">
                                {filteredEvents.map((event) => (
                                    <EventCard key={event.eventId} event={event} />
                                ))}
                            </div>
                        ) : (
                            <div className="text-center py-12">
                                <h3 className="text-lg font-medium">No events found</h3>
                                <p className="text-muted-foreground mt-2">Try adjusting your search or filters</p>
                            </div>
                        )}
                    </div>
                </div>
            </div>

            <div className="mt-auto">
                <Footer />
            </div>
        </div>
    );
};

export default Events;
