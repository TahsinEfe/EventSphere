// src/components/EventCard.tsx

import { Link } from "react-router-dom";
import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Calendar } from "lucide-react";
import { EventType } from "@/types";
import { formatDate, getEventTypeLabel } from "@/data/mockData";

export interface EventCardProps {
    event: {
        eventId?: number;
        name: string;
        imageUrl?: string;
        startDateTime: string;
        eventTypeId?: number;
        location?: string;
        city?: string;
        country?: string;
    };
}

const EventCard = ({ event }: EventCardProps) => {
    const {
        eventId,
        name,
        imageUrl,
        startDateTime,
        eventTypeId = EventType.Concert,
        location,
        city,
        country,
    } = event;

    const formattedDate = formatDate(startDateTime);
    const eventTypeLabel = getEventTypeLabel(eventTypeId);

    const getBadgeVariant = (type: number) => {
        switch (type) {
            case EventType.Concert:
                return "default";
            case EventType.Conference:
                return "secondary";
            case EventType.Theater:
                return "outline";
            case EventType.Sport:
                return "destructive";
            case EventType.Festival:
                return "secondary";
            default:
                return "default";
        }
    };

    const resolvedImageUrl = imageUrl?.startsWith("/") ? imageUrl : "/placeholder.svg";

    return (
        <Card className="overflow-hidden hover:shadow-lg transition-shadow duration-300">
            <Link to={`/events/${eventId}`}>
                <img
                    src={resolvedImageUrl}
                    alt={name}
                    className="w-full h-48 object-cover"
                />
                <CardContent className="p-4">
                    <Badge variant={getBadgeVariant(eventTypeId)} className="mb-2">
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
