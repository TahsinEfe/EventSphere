import { Link } from "react-router-dom";
import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Calendar } from "lucide-react";
import { EventDto } from "@/types/EventDto";

// EventType label ve badge mapping
const EVENT_TYPE_LABELS: Record<number, string> = {
    1: "Concert",
    2: "Theater",
    3: "Conference",
    5: "Festival",
    6: "Workshop",
    8: "Webinar",
    9: "Exhibition",
    10: "Networking Event",
    11: "Charity Event",
    12: "Sports",
    13: "Meetup",
};

const EVENT_TYPE_BADGE_VARIANT: Record<number, "default" | "secondary" | "outline" | "destructive"> = {
    1: "default",       // Concert
    2: "outline",       // Theater
    3: "secondary",     // Conference
    5: "secondary",     // Festival
    6: "secondary",     // Workshop
    8: "secondary",     // Webinar
    9: "secondary",     // Exhibition
    10: "secondary",    // Networking Event
    11: "secondary",    // Charity Event
    12: "destructive",  // Sports
    13: "secondary",    // Meetup
};

const API_BASE_URL = import.meta.env.VITE_API_URL || "http://localhost:5172";

const formatDate = (dateStr: string | Date): string => {
    const date = typeof dateStr === "string" ? new Date(dateStr) : dateStr;
    return date.toLocaleString("en-US", {
        year: "numeric",
        month: "long",
        day: "numeric",
        hour: "2-digit",
        minute: "2-digit",
    });
};

export interface EventCardProps {
    event: EventDto;
}

const EventCard = ({ event }: EventCardProps) => {
    const {
        eventId,
        name,
        imageUrl,
        startDateTime,
        eventTypeId,
        city,
        country,
    } = event;

    const formattedDate = formatDate(startDateTime);
    const eventTypeLabel = EVENT_TYPE_LABELS[eventTypeId] || "Other";
    const badgeVariant = EVENT_TYPE_BADGE_VARIANT[eventTypeId] || "default";

    const resolvedImageUrl =
        typeof imageUrl === "string" && imageUrl.startsWith("/")
            ? `${API_BASE_URL}${imageUrl}`
            : "/placeholder.svg";

    return (
        <Card className="overflow-hidden hover:shadow-lg transition-shadow duration-300">
            <Link to={`/events/${eventId}`}>
                <img
                    src={resolvedImageUrl}
                    alt={name}
                    className="w-full h-48 object-cover"
                />
                <CardContent className="p-4">
                    <Badge variant={badgeVariant} className="mb-2">
                        {eventTypeLabel}
                    </Badge>
                    <h3 className="text-lg font-semibold line-clamp-2 mb-2">{name}</h3>
                    <div className="flex items-center text-sm text-muted-foreground mb-2">
                        <Calendar className="h-4 w-4 mr-1" />
                        <span>{formattedDate}</span>
                    </div>
                    {(city || country) && (
                        <p className="text-sm text-muted-foreground line-clamp-1">
                            {[city, country].filter(Boolean).join(", ")}
                        </p>
                    )}
                </CardContent>
            </Link>
        </Card>
    );
};

export default EventCard;
