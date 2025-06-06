import { useEffect, useState } from "react";
import { EventsAPI } from "@/services/EventsAPI";
import { EventDto } from "@/types/EventDto";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Edit, Trash2, RefreshCw, ArrowLeft } from "lucide-react";
import { useNavigate } from "react-router-dom";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
    DialogFooter,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";


const EventsDashboard = () => {
    const [events, setEvents] = useState<EventDto[]>([]);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
    const [editingEvent, setEditingEvent] = useState<EventDto | null>(null);
    const [dialogOpen, setDialogOpen] = useState(false);



    useEffect(() => {
        fetchEvents();
    }, []);

    const fetchEvents = async () => {
        setLoading(true);
        try {
            const res = await EventsAPI.getAll();
            setEvents(res.data || []);
        } catch (err) {
            console.error("Event fetch error", err);
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async (eventId: number) => {
        if (!window.confirm("Are you sure you want to delete this event?")) return;
        try {
            await EventsAPI.delete(eventId);
            setEvents(events.filter((e) => e.eventId !== eventId));
        } catch (err) {
            console.error("Delete failed", err);
        }
    };

    const handleEditClick = (event: EventDto) => {
        setEditingEvent({ ...event });
        setDialogOpen(true);
    };

    const handleInputChange = (field: keyof EventDto, value: string | number | boolean) => {
        if (!editingEvent) return;
        setEditingEvent({ ...editingEvent, [field]: value });
    };

    const handleUpdateEvent = async () => {
        if (!editingEvent || !editingEvent.eventId) return;
        try {
            await EventsAPI.update(editingEvent.eventId, editingEvent);
            setDialogOpen(false);
            await fetchEvents(); // listeyi yenile
        } catch (err) {
            console.error("Update error", err);
        }
    };



    return (
        <div className="container mx-auto px-4 py-8">
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-3xl font-bold">All Events</h1>
                <div className="flex gap-2">
                    <Button variant="outline" onClick={() => navigate("/dashboard")}>
                        <ArrowLeft className="mr-2 h-4 w-4" /> Back to Dashboard
                    </Button>
                    <Button onClick={fetchEvents} disabled={loading}>
                        <RefreshCw className="mr-2 h-4 w-4" /> Refresh
                    </Button>
                </div>
            </div>
            <Card>
                <CardHeader>
                    <CardTitle>Events</CardTitle>
                </CardHeader>
                <CardContent>
                    <div className="overflow-x-auto">
                        <table className="min-w-full">
                            <thead>
                                <tr className="border-b">
                                    <th className="p-3 text-left">Image</th>
                                    <th className="p-3 text-left">ID</th>
                                    <th className="p-3 text-left">Name</th>
                                    <th className="p-3 text-left">Start Date</th>
                                    <th className="p-3 text-left">Location</th>
                                    <th className="p-3 text-left">Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                {events.map((event) => (
                                    <tr key={event.eventId} className="border-b hover:bg-gray-50">
                                        <td className="p-3">
                                            <img
                                                src={
                                                    event.imageUrl && event.imageUrl.startsWith("/")
                                                        ? event.imageUrl
                                                        : "/placeholder.svg"
                                                }
                                                alt={event.name}
                                                className="w-24 h-16 object-cover rounded"
                                            />
                                        </td>
                                        <td className="p-3">{event.eventId}</td>
                                        <td className="p-3">{event.name}</td>
                                        <td className="p-3">{new Date(event.startDateTime).toLocaleDateString()}</td>
                                        <td className="p-3">{event.location || "-"}</td>
                                        <td className="p-3 space-x-2">
                                            <Button size="sm" variant="outline" onClick={() => handleEditClick(event)}>
                                                <Edit className="h-4 w-4" />
                                            </Button>

                                            <Button
                                                size="sm"
                                                variant="outline"
                                                onClick={() => handleDelete(event.eventId!)}
                                                className="text-red-600"
                                            >
                                                <Trash2 className="h-4 w-4" />
                                            </Button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </CardContent>
            </Card>
            <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
                <DialogContent className="max-w-md">
                    <DialogHeader>
                        <DialogTitle>Edit Event</DialogTitle>
                    </DialogHeader>
                    <div className="space-y-4">
                        <Label htmlFor="name">Event Name</Label>
                        <Input
                            id="name"
                            value={editingEvent?.name || ""}
                            onChange={(e) => handleInputChange("name", e.target.value)}
                        />
                        <Label htmlFor="imageUrl">Image URL</Label>
                        <Input
                            id="imageUrl"
                            value={editingEvent?.imageUrl || ""}
                            onChange={(e) => handleInputChange("imageUrl", e.target.value)}
                        />
                        <Label htmlFor="start">Start Date</Label>
                        <Input
                            id="start"
                            type="datetime-local"
                            value={editingEvent?.startDateTime || ""}
                            onChange={(e) => handleInputChange("startDateTime", e.target.value)}
                        />
                        <Label htmlFor="end">End Date</Label>
                        <Input
                            id="end"
                            type="datetime-local"
                            value={editingEvent?.endDateTime || ""}
                            onChange={(e) => handleInputChange("endDateTime", e.target.value)}
                        />
                        <Label htmlFor="description">Description</Label>
                        <Input
                            id="description"
                            value={editingEvent?.description || ""}
                            onChange={(e) => handleInputChange("description", e.target.value)}
                        />
                    </div>
                    <DialogFooter className="pt-4">
                        <Button onClick={handleUpdateEvent} className="bg-blue-600 hover:bg-blue-700">
                            Update
                        </Button>
                    </DialogFooter>
                </DialogContent>
            </Dialog>

        </div>
    );
};

export default EventsDashboard;
