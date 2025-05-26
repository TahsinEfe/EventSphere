
import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";
import { Button } from "@/components/ui/button";
import {
  Calendar,
  Clock,
  MapPin,
  Share2,
  Users,
  Ticket,
  ArrowLeft,
} from "lucide-react";
import { Breadcrumb, BreadcrumbItem, BreadcrumbLink, BreadcrumbList, BreadcrumbSeparator } from "@/components/ui/breadcrumb";
import { AspectRatio } from "@/components/ui/aspect-ratio";
import { Badge } from "@/components/ui/badge";
import { useToast } from "@/components/ui/use-toast";
import { formatDate, getEventTypeLabel } from "@/data/mockData";
import { Event, EventType } from "@/types";

// Mock event data
const mockEvent: Event = {
  id: 1,
  organizationId: 1,
  organizationName: "Music Productions Inc.",
  name: "Summer Music Festival",
  startDateTime: "2025-06-15T18:00:00",
  endDateTime: "2025-06-15T23:00:00",
  eventTypeId: EventType.Festival,
  eventStatusId: 1, // Upcoming
  isPublic: true,
  maxAttendees: 5000,
  availableSeats: 2350,
  price: 250,
  imageUrl: "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?q=80&w=2070&auto=format&fit=crop",
  description: "Join us for an unforgettable night of music under the stars! Experience live performances from top artists across multiple genres. Food and beverages will be available for purchase. This event is suitable for all ages, though parental guidance is suggested for younger attendees due to crowd size. Don't miss the biggest music festival of the summer!",
  address: {
    id: 1,
    street: "123 Festival Avenue",
    city: "Istanbul",
    district: "Kadıköy",
    postalCode: "34710",
    country: "Turkey",
  },
};

const EventDetail = () => {
  const { id } = useParams<{ id: string }>();
  const [event, setEvent] = useState<Event | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [ticketQuantity, setTicketQuantity] = useState<number>(1);
  const { toast } = useToast();
  
  // Fetch event data
  useEffect(() => {
    // Simulate API call
    setTimeout(() => {
      setEvent(mockEvent);
      setIsLoading(false);
    }, 500);
  }, [id]);
  
  const handlePurchase = () => {
    toast({
      title: "Tickets purchased!",
      description: `You have purchased ${ticketQuantity} ticket(s) for ${event?.name}`,
    });
  };
  
  const handleShare = () => {
    navigator.clipboard.writeText(window.location.href);
    toast({
      title: "Link copied to clipboard",
      description: "You can now share this event with your friends!",
    });
  };
  
  if (isLoading) {
    return (
      <div className="container mx-auto px-4 py-12 flex justify-center items-center min-h-[70vh]">
        <div className="animate-pulse space-y-8 w-full max-w-4xl">
          <div className="h-8 bg-muted rounded w-3/4"></div>
          <div className="h-64 bg-muted rounded"></div>
          <div className="space-y-4">
            <div className="h-4 bg-muted rounded w-1/2"></div>
            <div className="h-4 bg-muted rounded w-full"></div>
            <div className="h-4 bg-muted rounded w-full"></div>
            <div className="h-4 bg-muted rounded w-3/4"></div>
          </div>
        </div>
      </div>
    );
  }
  
  if (!event) {
    return (
      <div className="container mx-auto px-4 py-12 flex justify-center items-center min-h-[70vh]">
        <div className="text-center">
          <h2 className="text-2xl font-bold">Event not found</h2>
          <p className="mt-4 text-muted-foreground">The event you're looking for doesn't exist or has been removed.</p>
          <Button asChild className="mt-6">
            <Link to="/">Return to Home</Link>
          </Button>
        </div>
      </div>
    );
  }
  
  return (
    <div className="min-h-screen bg-background">
      {/* Event Banner */}
      <div className="event-banner">
        <img 
          src={event.imageUrl || "/placeholder.svg"} 
          alt={event.name}
          className="w-full h-full object-cover"
        />
        <div className="absolute inset-0 bg-gradient-to-t from-background to-transparent"></div>
      </div>
      
      <div className="container mx-auto px-4 -mt-16 relative z-10">
        {/* Breadcrumbs */}
        <Breadcrumb className="mb-8">
          <BreadcrumbList>
            <BreadcrumbItem>
              <BreadcrumbLink asChild>
                <Link to="/">Home</Link>
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink asChild>
                <Link to="/">Events</Link>
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbLink>Event Details</BreadcrumbLink>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
        
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* Main Content */}
          <div className="lg:col-span-2 space-y-6">
            <div className="bg-card p-6 rounded-lg shadow-sm">
              <Badge className="mb-3">{getEventTypeLabel(event.eventTypeId)}</Badge>
              <h1 className="text-3xl md:text-4xl font-bold mb-4">{event.name}</h1>
              
              <div className="flex flex-wrap items-center gap-4 mb-6 text-muted-foreground">
                <div className="flex items-center gap-1">
                  <Calendar className="h-4 w-4" />
                  <span>{formatDate(event.startDateTime)}</span>
                </div>
                <div className="flex items-center gap-1">
                  <Clock className="h-4 w-4" />
                  <span>
                    {new Date(event.startDateTime).toLocaleTimeString([], {
                      hour: "2-digit",
                      minute: "2-digit",
                    })}
                    {" - "}
                    {new Date(event.endDateTime).toLocaleTimeString([], {
                      hour: "2-digit",
                      minute: "2-digit",
                    })}
                  </span>
                </div>
                <div className="flex items-center gap-1">
                  <MapPin className="h-4 w-4" />
                  <span>
                    {event.address?.city}, {event.address?.country}
                  </span>
                </div>
                {event.maxAttendees && (
                  <div className="flex items-center gap-1">
                    <Users className="h-4 w-4" />
                    <span>Max {event.maxAttendees} attendees</span>
                  </div>
                )}
              </div>
              
              <div className="mb-6">
                <h2 className="text-xl font-semibold mb-3">About this event</h2>
                <p className="whitespace-pre-line">{event.description}</p>
              </div>
              
              <div className="mb-6">
                <h2 className="text-xl font-semibold mb-3">Location</h2>
                
                <div className="bg-muted rounded-lg p-4">
                  <p className="font-medium">{event.address?.street}</p>
                  <p>{event.address?.district}, {event.address?.city}</p>
                  <p>{event.address?.postalCode}, {event.address?.country}</p>
                </div>
                
                <div className="mt-4 rounded-lg overflow-hidden border">
                  <AspectRatio ratio={16 / 9}>
                    <iframe
                      src={`https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d24087.292454157395!2d29.02393491553662!3d40.99044884387752!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14cab7650656bd63%3A0x8ca0155060304e2!2zS2FkxLFrw7Z5LCDEsHN0YW5idWwsIFTDvHJraXll!5e0!3m2!1sen!2sus!4v1715963841202!5m2!1sen!2sus`}
                      width="100%"
                      height="100%"
                      style={{ border: 0 }}
                      allowFullScreen={true}
                      loading="lazy"
                      referrerPolicy="no-referrer-when-downgrade"
                    ></iframe>
                  </AspectRatio>
                </div>
              </div>
              
              <div>
                <h2 className="text-xl font-semibold mb-3">Organizer</h2>
                <p className="font-medium">{event.organizationName}</p>
              </div>
            </div>
          </div>
          
          {/* Ticket Purchase */}
          <div className="lg:col-span-1">
            <div className="bg-card p-6 rounded-lg shadow-sm border sticky top-8">
              <h2 className="text-xl font-semibold mb-4">Get Tickets</h2>
              
              {event.availableSeats && event.availableSeats > 0 ? (
                <>
                  <div className="flex justify-between items-center mb-4">
                    <span>Price per ticket</span>
                    <span className="font-semibold text-lg">
                      {event.price ? `₺${event.price}` : "Free"}
                    </span>
                  </div>
                  
                  <div className="flex items-center justify-between mb-6">
                    <span>Quantity</span>
                    <div className="flex items-center">
                      <Button
                        variant="outline"
                        size="icon"
                        onClick={() => setTicketQuantity(Math.max(1, ticketQuantity - 1))}
                        disabled={ticketQuantity <= 1}
                      >
                        -
                      </Button>
                      <span className="w-12 text-center">{ticketQuantity}</span>
                      <Button
                        variant="outline"
                        size="icon"
                        onClick={() => setTicketQuantity(Math.min(10, ticketQuantity + 1))}
                        disabled={ticketQuantity >= 10 || ticketQuantity >= (event.availableSeats || 0)}
                      >
                        +
                      </Button>
                    </div>
                  </div>
                  
                  <div className="border-t pt-4 mb-4">
                    <div className="flex justify-between items-center mb-2">
                      <span>Subtotal</span>
                      <span>
                        {event.price ? `₺${event.price * ticketQuantity}` : "Free"}
                      </span>
                    </div>
                    <div className="flex justify-between items-center mb-2">
                      <span>Service fee</span>
                      <span>
                        {event.price ? `₺${(event.price * ticketQuantity * 0.05).toFixed(2)}` : "₺0"}
                      </span>
                    </div>
                    <div className="flex justify-between items-center font-semibold text-lg mt-4">
                      <span>Total</span>
                      <span>
                        {event.price 
                          ? `₺${(event.price * ticketQuantity * 1.05).toFixed(2)}` 
                          : "Free"}
                      </span>
                    </div>
                  </div>
                  
                  <AlertDialog>
                    <AlertDialogTrigger asChild>
                      <Button className="w-full mt-2" size="lg">
                        <Ticket className="mr-2 h-4 w-4" />
                        {event.price ? "Purchase Tickets" : "Register for Free"}
                      </Button>
                    </AlertDialogTrigger>
                    <AlertDialogContent>
                      <AlertDialogHeader>
                        <AlertDialogTitle>Confirm your purchase</AlertDialogTitle>
                        <AlertDialogDescription>
                          You are about to purchase {ticketQuantity} ticket{ticketQuantity > 1 ? "s" : ""} for {event.name}.
                          {event.price 
                            ? ` The total amount is ₺${(event.price * ticketQuantity * 1.05).toFixed(2)}.`
                            : " This is a free event."}
                        </AlertDialogDescription>
                      </AlertDialogHeader>
                      <AlertDialogFooter>
                        <AlertDialogCancel>Cancel</AlertDialogCancel>
                        <AlertDialogAction onClick={handlePurchase}>
                          Confirm Purchase
                        </AlertDialogAction>
                      </AlertDialogFooter>
                    </AlertDialogContent>
                  </AlertDialog>
                  
                  <p className="text-sm text-muted-foreground mt-4">
                    {event.availableSeats} tickets remaining
                  </p>
                </>
              ) : (
                <>
                  <div className="bg-muted p-4 rounded-lg text-center mb-4">
                    <h3 className="font-semibold">Sold Out</h3>
                    <p className="text-sm text-muted-foreground">
                      This event has sold out. Join the waitlist to be notified if tickets become available.
                    </p>
                  </div>
                  <Button className="w-full" variant="outline">
                    Join Waitlist
                  </Button>
                </>
              )}
              
              <div className="mt-6 flex justify-center">
                <Button variant="ghost" onClick={handleShare}>
                  <Share2 className="mr-2 h-4 w-4" />
                  Share Event
                </Button>
              </div>
            </div>
          </div>
        </div>
        
        <div className="mt-12 pb-8">
          <Button variant="ghost" asChild>
            <Link to="/">
              <ArrowLeft className="mr-2 h-4 w-4" />
              Back to Events
            </Link>
          </Button>
        </div>
      </div>
    </div>
  );
};

export default EventDetail;
