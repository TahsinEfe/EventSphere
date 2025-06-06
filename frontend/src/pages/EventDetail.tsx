import { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import {
    Calendar, Clock, MapPin, Users, ArrowLeft, Star, Share2
} from "lucide-react";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { Breadcrumb, BreadcrumbItem, BreadcrumbLink, BreadcrumbList, BreadcrumbSeparator } from "@/components/ui/breadcrumb";
import { useToast } from "@/components/ui/use-toast";
import { Textarea } from "@/components/ui/textarea";
import { AspectRatio } from "@/components/ui/aspect-ratio";
import { formatDate, getEventTypeLabel } from "@/data/mockData";
import api from "@/services/api";
import { EventDto } from "@/types/EventDto";
import { FeedbacksDto } from "@/types/FeedbacksDto";

const EventDetail = () => {
    const { id } = useParams<{ id: string }>();
    const [event, setEvent] = useState<EventDto | null>(null);
    const [feedbacks, setFeedbacks] = useState<FeedbacksDto[]>([]);
    const [comment, setComment] = useState("");
    const [rating, setRating] = useState<number>(0);
    const [userId] = useState(1); // Giriş yapan kullanıcı
    const { toast } = useToast();

    useEffect(() => {
        if (!id) return;
        fetchEventData();
        fetchFeedbacks();
    }, [id]);

    const fetchEventData = async () => {
        try {
            const res = await api.get(`/Events/${id}`);
            setEvent(res.data);
        } catch (err) {
            console.error("Etkinlik verisi alınamadı", err);
        }
    };

    const fetchFeedbacks = async () => {
        try {
            const res = await api.get("/Feedbacks");
            const filtered = res.data.filter((f: FeedbacksDto) => f.eventId === Number(id));
            setFeedbacks(filtered);
        } catch (err) {
            console.error("Feedback verisi alınamadı", err);
        }
    };

    const handleSubmitFeedback = async () => {
        try {
            const payload: FeedbacksDto = {
                eventId: Number(id),
                userId,
                rating,
                comments: comment,
            };
            await api.post(`/Feedbacks?userId=${userId}`, payload);
            toast({ title: "Feedback sent." });
            setComment("");
            setRating(0);
            fetchFeedbacks();
        } catch (err) {
            toast({ title: "Feedback couldn't send.", variant: "destructive" });
        }
    };

    const isPastEvent = event ? new Date(event.endDateTime) < new Date() : false;

    if (!event) return <div className="p-10 text-center">Etkinlik bulunamadı.</div>;

    return (
        <div className="min-h-screen bg-background">
            <div className="event-banner relative">
                <img
                    src={event.imageUrl || "/placeholder.svg"}
                    alt={event.name}
                    className="w-full h-[320px] object-cover"
                />
                <div className="absolute inset-0 bg-gradient-to-t from-background to-transparent" />
            </div>

            <div className="container mx-auto px-4 -mt-12 relative z-10">
                <Breadcrumb className="mb-8">
                    <BreadcrumbList>
                        <BreadcrumbItem><BreadcrumbLink asChild><Link to="/">Home</Link></BreadcrumbLink></BreadcrumbItem>
                        <BreadcrumbSeparator />
                        <BreadcrumbItem><BreadcrumbLink asChild><Link to="/events">Events</Link></BreadcrumbLink></BreadcrumbItem>
                        <BreadcrumbSeparator />
                        <BreadcrumbItem><BreadcrumbLink>{event.name}</BreadcrumbLink></BreadcrumbItem>
                    </BreadcrumbList>
                </Breadcrumb>

                <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
                    {/* Sol Kısım */}
                    <div className="lg:col-span-2 space-y-6">
                        <div className="bg-card p-6 rounded-lg shadow-sm">
                            <Badge className="mb-3">{getEventTypeLabel(event.eventTypeId)}</Badge>
                            <h1 className="text-3xl font-bold mb-4">{event.name}</h1>

                            <div className="flex flex-wrap items-center gap-4 text-muted-foreground mb-6">
                                <div className="flex items-center gap-1"><Calendar className="h-4 w-4" />{formatDate(event.startDateTime)}</div>
                                <div className="flex items-center gap-1"><Clock className="h-4 w-4" />{new Date(event.startDateTime).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })} - {new Date(event.endDateTime).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}</div>
                                <div className="flex items-center gap-1"><MapPin className="h-4 w-4" />{event.city}, {event.country}</div>
                                {event.maxAttendees && <div className="flex items-center gap-1"><Users className="h-4 w-4" /> Max {event.maxAttendees} attendees</div>}
                            </div>

                            <p className="mb-6">{event.description}</p>

                            <div className="bg-muted rounded-lg p-4 mb-4">
                                <p className="font-medium">{event.location}</p>
                                <p>{event.city}, {event.country}</p>
                            </div>

                            {/* Google Maps */}
                            <div className="mt-4 rounded-lg overflow-hidden border">
                                <AspectRatio ratio={16 / 9}>
                                    <iframe
                                        src={`https://www.google.com/maps?q=${encodeURIComponent(event.city)}&output=embed`}
                                        width="100%"
                                        height="100%"
                                        style={{ border: 0 }}
                                        loading="lazy"
                                        allowFullScreen
                                    ></iframe>
                                </AspectRatio>
                            </div>

                            
                        </div>
                    </div>

                    {/* Sağ Kısım - Feedback */}
                    <div className="lg:col-span-1">
                        <div className="bg-card p-6 rounded-lg shadow-sm border sticky top-8">
                            <h2 className="text-xl font-semibold mb-4">Feedback</h2>

                            {isPastEvent ? (
                                <>
                                    <div className="mb-4 space-y-2">
                                        <label className="text-sm font-medium">Rating</label>
                                        <div className="flex space-x-1">
                                            {[1, 2, 3, 4, 5].map((star) => (
                                                <Star
                                                    key={star}
                                                    className={`h-6 w-6 cursor-pointer ${star <= rating ? "text-yellow-500" : "text-gray-300"}`}
                                                    fill={star <= rating ? "#facc15" : "none"}
                                                    onClick={() => setRating(star)}
                                                />
                                            ))}
                                        </div>

                                        <label className="text-sm font-medium">Comment</label>
                                        <Textarea
                                            placeholder="Your thoughts about this event..."
                                            value={comment}
                                            onChange={(e) => setComment(e.target.value)}
                                        />
                                        <Button className="mt-2 w-full" onClick={handleSubmitFeedback}>Submit Feedback</Button>
                                    </div>

                                    <div className="mt-6">
                                        <h3 className="font-semibold mb-2">Previous Feedback</h3>
                                        {feedbacks.length > 0 ? feedbacks.map(f => (
                                            <div key={f.feedbackId} className="border rounded-lg p-3 mb-2">
                                                <div className="flex justify-between mb-1">
                                                    <strong>{f.userName}</strong>
                                                    <span className="text-yellow-500 flex items-center">
                                                        <Star className="w-4 h-4 mr-1" /> {f.rating}
                                                    </span>
                                                </div>
                                                <p className="text-sm">{f.comments}</p>
                                            </div>
                                        )) : <p className="text-muted-foreground">No feedback yet.</p>}
                                    </div>
                                </>
                            ) : (
                                <div className="bg-muted p-4 rounded-lg text-center">
                                    <p className="font-medium text-muted-foreground">
                                        Feedback will be available after the event has ended.
                                    </p>
                                </div>
                            )}

                        </div>
                    </div>
                </div>

                <div className="mt-12 pb-8">
                    <Button variant="ghost" asChild>
                        <Link to="/events">
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
