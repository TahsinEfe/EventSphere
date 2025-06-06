import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Calendar, MapPin, ArrowRight } from "lucide-react";
import { EventsAPI } from "@/services/EventsAPI";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import EventCard from "@/components/EventCard";
import { EventDto } from "@/types/EventDto";
import { Badge } from "@/components/ui/badge";
import { getEventTypeLabel } from "@/data/mockData";
import EventTypeCarousel from "../components/EventTypeCarousel";
import { EventTypesDto } from "../types/EventTypesDto";
import axios from "axios";

const Index = () => {
    const [featuredEvents, setFeaturedEvents] = useState<EventDto[]>([]);
    const [upcomingEvents, setUpcomingEvents] = useState<EventDto[]>([]);
    const [uniqueEventTypes, setUniqueEventTypes] = useState<EventDto[]>([]);
    const [eventTypes, setEventTypes] = useState<EventTypesDto[]>([]);


    useEffect(() => {
        const fetchEvents = async () => {
            try {
                const res = await EventsAPI.getAll();
                const events: EventDto[] = res.data;

                // Featured Events
                const shuffled = [...events].sort(() => 0.5 - Math.random());
                setFeaturedEvents(shuffled.slice(0, 3));

                // Upcoming Events
                const sorted = [...events].sort(
                    (a, b) => new Date(a.startDateTime).getTime() - new Date(b.startDateTime).getTime()
                );
                setUpcomingEvents(sorted.slice(0, 4));

                // Unique Event Types (first event per type)
                const seen = new Set<number>();
                const unique = events.filter((e) => {
                    if (seen.has(e.eventTypeId)) return false;
                    seen.add(e.eventTypeId);
                    return true;
                });
                setUniqueEventTypes(unique);
            } catch (error) {
                console.error("Event fetch error:", error);
            }
        };

        fetchEvents();
    }, []);

    useEffect(() => {
        const fetchEventTypes = async () => {
            try {
                const res = await axios.get("/api/EventTypes");
                setEventTypes(res.data);
            } catch (err) {
                console.error("Failed to fetch event types", err);
            }
        };

        fetchEventTypes();
    }, []);

    return (
        <div className="flex flex-col min-h-screen">
            <Navbar />

            {/* Hero Section */}
            <section className="relative h-[500px] md:h-[600px] w-full overflow-hidden bg-gradient-to-br from-primary/90 to-accent/90">
                <div
                    className="absolute inset-0 bg-cover bg-center opacity-30"
                    style={{
                        backgroundImage:
                            "url(https://images.unsplash.com/photo-1540575467063-178a50c2df87?auto=format&fit=crop&w=1470&q=80)",
                    }}
                />
                <div className="container px-4 md:px-6 h-full flex flex-col justify-center relative z-10">
                    <h1 className="text-4xl md:text-6xl font-bold text-white mb-4">
                        Discover Amazing Events
                    </h1>
                    <p className="text-xl md:text-2xl text-white/90 max-w-xl mb-8">
                        Find and book tickets for the best concerts, conferences, theater shows and more.
                    </p>
                    <div className="flex flex-col sm:flex-row gap-4">
                        <Link to="/events">
                            <Button size="lg" className="text-md">
                                Browse Events
                            </Button>
                        </Link>
                        <Link to="/register">
                            <Button size="lg" variant="outline" className="text-md bg-white/20 text-white border-white/40 hover:bg-white/30">
                                Sign Up
                            </Button>
                        </Link>
                    </div>
                </div>
            </section>

            {/* Featured Events */}
            <section className="py-16 bg-muted/30">
                <div className="container px-4 md:px-6">
                    <div className="flex justify-between items-center mb-8">
                        <h2 className="text-3xl font-bold">Featured Events</h2>
                        <Link to="/events" className="text-primary flex items-center hover:underline">
                            View All <ArrowRight className="h-4 w-4 ml-1" />
                        </Link>
                    </div>

                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                        {featuredEvents.map((event) => (
                            <EventCard key={event.eventId} event={event} />
                        ))}
                    </div>
                </div>
            </section>

            {/* How It Works */}
            <section className="py-16 bg-background">
                <div className="container px-4 md:px-6">
                    <h2 className="text-3xl font-bold text-center mb-12">How It Works</h2>

                    <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
                        <div className="flex flex-col items-center text-center">
                            <div className="w-16 h-16 rounded-full bg-primary/10 flex items-center justify-center mb-4">
                                <Calendar className="h-8 w-8 text-primary" />
                            </div>
                            <h3 className="text-xl font-semibold mb-3">Find Events</h3>
                            <p className="text-muted-foreground">
                                Search for events by type, location, or date.
                            </p>
                        </div>

                        <div className="flex flex-col items-center text-center">
                            <div className="w-16 h-16 rounded-full bg-primary/10 flex items-center justify-center mb-4">
                                <MapPin className="h-8 w-8 text-primary" />
                            </div>
                            <h3 className="text-xl font-semibold mb-3">Book Tickets</h3>
                            <p className="text-muted-foreground">
                                Select your seats and complete your purchase securely.
                            </p>
                        </div>

                        <div className="flex flex-col items-center text-center">
                            <div className="w-16 h-16 rounded-full bg-primary/10 flex items-center justify-center mb-4">
                                <Calendar className="h-8 w-8 text-primary" />
                            </div>
                            <h3 className="text-xl font-semibold mb-3">Attend Events</h3>
                            <p className="text-muted-foreground">
                                Get your e-tickets and enjoy the experience!
                            </p>
                        </div>
                    </div>
                </div>
            </section>

            {/* Event Types Gallery */}
            <section className="py-16 bg-muted/30">
                <div className="container px-4 md:px-6">
                    <div className="flex justify-between items-center mb-8">
                        <h2 className="text-3xl font-bold">Event Types</h2>
                        <Link to="/events" className="text-primary flex items-center hover:underline">
                            View All <ArrowRight className="h-4 w-4 ml-1" />
                        </Link>
                    </div>

                    {/* Carousel */}
                    <EventTypeCarousel eventTypes={eventTypes} />
                </div>
            </section>


            <Footer />
        </div>
    );
};

export default Index;
