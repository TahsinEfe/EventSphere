import { useRef } from "react";
import { Badge } from "@/components/ui/badge";
import { ChevronLeft, ChevronRight } from "lucide-react";
import { EventTypesDto } from "@/types/EventTypesDto";

interface EventTypeCarouselProps {
    eventTypes: EventTypesDto[];
}

const EventTypeCarousel = ({ eventTypes }: EventTypeCarouselProps) => {
    const scrollRef = useRef<HTMLDivElement>(null);
    const scrollByAmount = 320;

    const scrollLeft = () => {
        scrollRef.current?.scrollBy({ left: -scrollByAmount, behavior: "smooth" });
    };

    const scrollRight = () => {
        if (!scrollRef.current) return;
        const { scrollLeft, offsetWidth, scrollWidth } = scrollRef.current;

        if (scrollLeft + offsetWidth >= scrollWidth) {
            scrollRef.current.scrollTo({ left: 0, behavior: "smooth" });
        } else {
            scrollRef.current.scrollBy({ left: scrollByAmount, behavior: "smooth" });
        }
    };

    const formatImageName = (typeName: string) => {
        const imageMap: Record<string, string> = {
            "Concert": "concert.jpg",
            "Festival": "food-festival.jpg",
            "Conference": "conference.jpg",
            "Theater": "theater.jpg",
            "Exhibition": "exhibition.jpg",
            "Charity Event": "charity.jpg",  
            "Workshop": "workshop.jpg",
            "Webinar": "webinar.jpg",
            "Meetup": "meetup.jpg",
            "Sports": "sport.jpg",
            "Networking Event": "networking.jpg",
        };

        return `/event-images/${imageMap[typeName] || "placeholder.svg"}`;
    };


    return (
        <div className="relative">
            <div className="flex items-center">
                <button
                    onClick={scrollLeft}
                    className="p-2 text-gray-600 hover:text-primary transition"
                >
                    <ChevronLeft className="w-6 h-6" />
                </button>

                <div
                    ref={scrollRef}
                    className="flex gap-4 overflow-x-auto scroll-smooth no-scrollbar"
                >
                    {eventTypes.map((eventType) => (
                        <div
                            key={eventType.eventTypeId}
                            className="min-w-[220px] flex-shrink-0 rounded-xl overflow-hidden shadow-md relative"
                        >
                            <img
                                src={formatImageName(eventType.typeName)}
                                alt={eventType.typeName}
                                className="w-full h-48 object-cover"
                            />
                            <div className="absolute bottom-2 left-2">
                                <Badge variant="secondary">
                                    {eventType.typeName}
                                </Badge>
                            </div>
                        </div>
                    ))}
                </div>

                <button
                    onClick={scrollRight}
                    className="p-2 text-gray-600 hover:text-primary transition"
                >
                    <ChevronRight className="w-6 h-6" />
                </button>
            </div>
        </div>
    );
};

export default EventTypeCarousel;
