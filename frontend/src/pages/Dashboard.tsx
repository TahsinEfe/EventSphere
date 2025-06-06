import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {
    Card, CardContent, CardDescription, CardHeader, CardTitle,
} from "@/components/ui/card";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Button } from "@/components/ui/button";
import {
    Calendar, Clock, Ticket, User, Bell, Plus, Edit, Building, ArrowLeft,
} from "lucide-react";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Badge } from "@/components/ui/badge";
import { NotificationDto } from "@/types/NotificationDto";
import { LoginResponse } from "@/services/auth";
import { AuthAPI } from "@/services/auth";
import { NotificationsAPI } from "@/services/NotificationsAPI";
import EventCard from "@/components/EventCard";
import { EventDto } from "@/types/EventDto";
import { EventsAPI } from "@/services/EventsAPI";
import { OrganizationsAPI } from "@/services/OrganizationsAPI";
import { OrganizationDto } from "@/types/OrganizationDto";
import { Dialog, DialogContent, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from "../components/ui/dialog";
import { Input } from "../components/ui/input";
import { EventTypesAPI } from "@/services/EventTypesAPI";
import api from "../services/api";
import { EventTypesDto } from "../types/EventTypesDto";
import { EventStatusesDto } from "../types/EventStatusesDto";

const Dashboard = () => {
    const [activeTab, setActiveTab] = useState("upcoming");
    const [user, setUser] = useState<LoginResponse | null>(null);
    const [notifications, setNotifications] = useState<NotificationDto[]>([]);
    const [upcomingEvents, setUpcomingEvents] = useState<EventDto[]>([]);
    const [registeredEvents, setRegisteredEvents] = useState<EventDto[]>([]);
    const [pastEvents, setPastEvents] = useState<EventDto[]>([]);
    const [organizations, setOrganizations] = useState<OrganizationDto[]>([]);
    const [totalEventCount, setTotalEventCount] = useState(0);
    const [newEventDialogOpen, setNewEventDialogOpen] = useState(false);
    const [eventTypes, setEventTypes] = useState<EventTypesDto[]>([]);
    const [allUsers, setAllUsers] = useState<{ userId: number; firstName: string; lastName: string }[]>([]);
    const [eventStatuses, setEventStatuses] = useState<EventStatusesDto[]>([]);
    const [addresses, setAddresses] = useState<{ addressId: number; street: string; city: string }[]>([]);

    const [newEventForm, setNewEventForm] = useState<EventDto>({
        organizationId: 1,
        name: "",
        startDateTime: "",
        endDateTime: "",
        eventTypeId: 1,
        eventStatusId: 1,
        organizerUserId: 1,
        isPublic: true,
        city: "",
        country: "",
        location: "",
        addressId: 1,
        imageUrl: "",
        description: "",
    });

    const handleCreateNewEvent = async () => {
        try {
            const formData = new FormData();
            formData.append("name", newEventForm.name);
            formData.append("startDateTime", newEventForm.startDateTime);
            formData.append("endDateTime", newEventForm.endDateTime);
            formData.append("eventTypeId", String(newEventForm.eventTypeId));
            formData.append("eventStatusId", String(newEventForm.eventStatusId));
            formData.append("organizationId", String(newEventForm.organizationId));
            formData.append("organizerUserId", String(newEventForm.organizerUserId));
            formData.append("isPublic", String(newEventForm.isPublic));
            formData.append("location", newEventForm.location || "");
            formData.append("city", newEventForm.city || "");
            formData.append("country", newEventForm.country || "");
            formData.append("description", newEventForm.description);
            formData.append("addressId", String(newEventForm.addressId));
            if (newEventForm.imageUrl instanceof File) {
                formData.append("imageFile", newEventForm.imageUrl);
            }
            if (newEventForm.maxAttendees != null)
                formData.append("maxAttendees", String(newEventForm.maxAttendees));
            if (newEventForm.registrationDeadline)
                formData.append("registrationDeadline", newEventForm.registrationDeadline);

            await api.post("/Events", formData, {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            });

            setNewEventDialogOpen(false);
            fetchAllEvents();
        } catch (err) {
            console.error("Create event error:", err);
        }
    };

    useEffect(() => {
        const fetchEventStatuses = async () => {
            try {
                const res = await api.get("/EventStatuses");
                setEventStatuses(res.data);
            } catch (err) {
                console.error("Event status fetch error:", err);
            }
        };
        fetchEventStatuses();
    }, []);

    useEffect(() => {
        const fetchAddresses = async () => {
            try {
                const res = await api.get("/Addresses");
                setAddresses(res.data);
            } catch (err) {
                console.error("Address fetch error:", err);
            }
        };

        fetchAddresses();
    }, []);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const res = await api.get("/Users");
                setAllUsers(res.data);
            } catch (err) {
                console.error("Kullanıcılar alınamadı:", err);
            }
        };

        fetchUsers();
    }, []);

    useEffect(() => {
        const fetchEventTypes = async () => {
            try {
                const res = await api.get("/EventTypes");
                setEventTypes(res.data);
            } catch (err) {
                console.error("Event types fetch error:", err);
            }
        };
        fetchEventTypes();
    }, []);

    useEffect(() => {
        const currentUser = AuthAPI.getCurrentUser();
        if (currentUser) {
            setUser(currentUser);
            setNewEventForm(prev => ({ ...prev, organizerUserId: currentUser.userId }));
            fetchUserData(currentUser.userId);
        }
        fetchAllEvents();
        fetchOrganizations();
    }, []);

    const fetchUserData = async (userId: number) => {
        try {
            const notifRes = await NotificationsAPI.getByUserId(userId);
            // Ensure notifications is always an array
            const notificationData = Array.isArray(notifRes.data) ? notifRes.data : [];
            setNotifications(notificationData);
        } catch (err) {
            console.error("Dashboard data fetch error:", err);
            setNotifications([]); // Fallback to empty array on error
        }
    };


    


    const fetchAllEvents = async () => {
        try {
            const res = await EventsAPI.getAll();
            const allEvents = Array.isArray(res.data) ? res.data : [];
            const now = new Date();

            setTotalEventCount(allEvents.length);

            const upcoming = allEvents
                .filter((e: EventDto) => new Date(e.startDateTime) > now)
                .sort((a, b) => new Date(a.startDateTime).getTime() - new Date(b.startDateTime).getTime());

            const past = allEvents
                .filter((e: EventDto) => new Date(e.startDateTime) <= now)
                .sort((a, b) => new Date(b.startDateTime).getTime() - new Date(a.startDateTime).getTime());

            setUpcomingEvents(upcoming);
            setPastEvents(past);

            const currentUser = AuthAPI.getCurrentUser();
            if (currentUser) {
                await fetchUserRegisteredEvents(currentUser.userId, allEvents);
            }
        } catch (err) {
            console.error("Event fetch error:", err);
            setTotalEventCount(0); 
        }
    };


    const fetchUserRegisteredEvents = async (userId: number, allEvents: EventDto[]) => {
        try {
            setRegisteredEvents([]);
        } catch (err) {
            console.error("User events fetch error:", err);
            setRegisteredEvents([]);
        }
    };

    const fetchOrganizations = async () => {
        try {
            const res = await OrganizationsAPI.getAll();
            setOrganizations(Array.isArray(res.data) ? res.data : []);
        } catch (error) {
            console.error("Organization fetch error:", error);
            setOrganizations([]);
        }
    };

    return (
        <div className="container mx-auto px-4 py-8">
            <Button
                variant="ghost"
                className="mb-4 text-blue-600 hover:text-blue-800 flex items-center"
                onClick={() => (window.location.href = "/")}
            >
                <ArrowLeft className="mr-2 h-4 w-4" />
                Back to Home
            </Button>
            <div className="flex flex-col md:flex-row gap-6">
                {/* Sidebar */}
                <div className="md:w-1/4">
                    <Card>
                        <CardContent className="p-6">
                            <div className="flex flex-col items-center">
                                <Avatar className="h-24 w-24 mb-4">
                                    <AvatarImage src="/blank-profile.png" />
                                    <AvatarFallback>{user?.firstName?.[0] || "U"}</AvatarFallback>
                                </Avatar>
                                <h2 className="text-xl font-semibold">
                                    {user?.firstName} {user?.lastName}
                                </h2>
                                <p className="text-sm text-muted-foreground">{user?.email}</p>

                                <Link to="/edit-profile">
                                    <Button variant="outline" className="w-full mt-4">
                                        <Edit className="mr-2 h-4 w-4" />
                                        Edit Profile
                                    </Button>
                                </Link>
                            </div>

                            <div className="mt-8">
                                <h3 className="font-medium mb-3">Dashboard</h3>
                                <nav className="space-y-2">
                                    <Link to="/events-dashboard">
                                        <Button variant="ghost" className="w-full justify-start">
                                            <Ticket className="mr-2 h-4 w-4" />
                                            My Events
                                            <Badge className="ml-auto">
                                                {totalEventCount}
                                            </Badge>
                                        </Button>
                                    </Link>

                                    <Link to="/organizations">
                                        <Button variant="ghost" className="w-full justify-start">
                                            <Building className="mr-2 h-4 w-4" />
                                            Organizations
                                        </Button>
                                    </Link>
                                    <Link to="/user-dashboard">
                                        <Button variant="ghost" className="w-full justify-start">
                                            <User className="mr-2 h-4 w-4" />
                                            Users
                                        </Button>
                                    </Link>
                                    
                                </nav>
                            </div>
                        </CardContent>
                    </Card>
                </div>

                {/* Main Content */}
                <div className="flex-1">
                    <h1 className="text-3xl font-bold mb-6">My Dashboard</h1>

                    <Tabs defaultValue="upcoming" className="w-full" onValueChange={setActiveTab}>
                        <div className="flex justify-between items-center mb-4">
                            <TabsList>
                                <TabsTrigger value="upcoming">Upcoming Events</TabsTrigger>
                                <TabsTrigger value="past">Past Events</TabsTrigger>
                                <TabsTrigger value="notifications">Notifications</TabsTrigger>
                            </TabsList>

                            <Dialog open={newEventDialogOpen} onOpenChange={setNewEventDialogOpen}>
                                <DialogTrigger asChild>
                                    <Button size="sm">
                                        <Plus className="mr-2 h-4 w-4" />
                                        New Event
                                    </Button>
                                </DialogTrigger>

                                <DialogContent className="max-w-2xl w-full max-h-[90vh] overflow-y-auto">
                                    <DialogHeader>
                                        <DialogTitle>Create New Event</DialogTitle>
                                    </DialogHeader>

                                    <div className="space-y-4">
                                        <div>
                                            <p className="text-sm font-semibold mb-1">Event Name</p>
                                            <Input
                                                value={newEventForm.name}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, name: e.target.value }))}
                                            />
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">Start Date</p>
                                            <Input
                                                type="datetime-local"
                                                value={newEventForm.startDateTime}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, startDateTime: e.target.value }))}
                                            />
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">End Date</p>
                                            <Input
                                                type="datetime-local"
                                                value={newEventForm.endDateTime}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, endDateTime: e.target.value }))}
                                            />
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">Event Type</p>
                                            <select
                                                className="w-full border rounded px-3 py-2 text-sm"
                                                value={newEventForm.eventTypeId}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, eventTypeId: parseInt(e.target.value) }))}
                                            >
                                                <option value="">Select</option>
                                                {eventTypes.map(type => (
                                                    <option key={type.eventTypeId} value={type.eventTypeId}>{type.typeName}</option>
                                                ))}
                                            </select>
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">Event Status</p>
                                            <select
                                                className="w-full border rounded px-3 py-2 text-sm"
                                                value={newEventForm.eventStatusId}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, eventStatusId: parseInt(e.target.value) }))}
                                            >
                                                <option value="">Select</option>
                                                {eventStatuses.map(status => (
                                                    <option key={status.eventStatusId} value={status.eventStatusId}>{status.statusName}</option>
                                                ))}
                                            </select>
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">Organization</p>
                                            <select
                                                className="w-full border rounded px-3 py-2 text-sm"
                                                value={newEventForm.organizationId}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, organizationId: parseInt(e.target.value) }))}
                                            >
                                                <option value="">Select</option>
                                                {organizations.map(org => (
                                                    <option key={org.organizationId} value={org.organizationId}>{org.name}</option>
                                                ))}
                                            </select>
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">Organizer</p>
                                            <select
                                                className="w-full border rounded px-3 py-2 text-sm"
                                                value={newEventForm.organizerUserId}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, organizerUserId: parseInt(e.target.value) }))}
                                            >
                                                <option value="">Select</option>
                                                {allUsers.map(user => (
                                                    <option key={user.userId} value={user.userId}>{user.firstName} {user.lastName}</option>
                                                ))}
                                            </select>
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">Max Attendees</p>
                                            <Input
                                                type="number"
                                                value={newEventForm.maxAttendees}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, maxAttendees: parseInt(e.target.value) }))}
                                            />
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">Registration Deadline</p>
                                            <Input
                                                type="datetime-local"
                                                value={newEventForm.registrationDeadline}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, registrationDeadline: e.target.value }))}
                                            />
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">Location</p>
                                            <Input
                                                value={newEventForm.location}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, location: e.target.value }))}
                                            />
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">Image URL</p>
                                            <Input
                                                type="file"
                                                accept="image/jpeg, image/png, image/jpg"
                                                onChange={(e) => {
                                                    const file = e.target.files?.[0];
                                                    if (file) {
                                                        setNewEventForm(prev => ({ ...prev, imageUrl: file }));
                                                    }
                                                }}
                                            />
                                        </div>

                                        <div>
                                            <p className="text-sm font-semibold mb-1">Description</p>
                                            <Input
                                                value={newEventForm.description}
                                                onChange={(e) => setNewEventForm(prev => ({ ...prev, description: e.target.value }))}
                                            />
                                        </div>
                                    </div>

                                    <DialogFooter>
                                        <Button onClick={handleCreateNewEvent}>Create</Button>
                                    </DialogFooter>
                                </DialogContent>
                            </Dialog>




                        </div>

                        <TabsContent value="upcoming" className="mt-0">
                            <Card>
                                <CardHeader>
                                    <CardTitle>Upcoming Events</CardTitle>
                                    <CardDescription>All upcoming public events listed by date.</CardDescription>
                                </CardHeader>
                                <CardContent>
                                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                                        {upcomingEvents.length > 0 ? (
                                            upcomingEvents.map((event) => (
                                                <EventCard key={event.eventId} event={event} />
                                            ))
                                        ) : (
                                            <div className="text-muted-foreground text-center p-8 col-span-full">
                                                No upcoming events found.
                                            </div>
                                        )}
                                    </div>
                                </CardContent>
                            </Card>
                        </TabsContent>

                        <TabsContent value="registered" className="mt-0">
                            <Card>
                                <CardHeader>
                                    <CardTitle>My Registered Events</CardTitle>
                                    <CardDescription>Events you have registered for.</CardDescription>
                                </CardHeader>
                                <CardContent>
                                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                                        {registeredEvents.length > 0 ? (
                                            registeredEvents.map((event) => (
                                                <EventCard key={event.eventId} event={event} />
                                            ))
                                        ) : (
                                            <div className="text-muted-foreground text-center p-8 col-span-full">
                                                You haven't registered for any events yet.
                                                <Link to="/events" className="block mt-2">
                                                    <Button variant="outline" size="sm">
                                                        Browse Events
                                                    </Button>
                                                </Link>
                                            </div>
                                        )}
                                    </div>
                                </CardContent>
                            </Card>
                        </TabsContent>

                        <TabsContent value="past" className="mt-0">
                            <Card>
                                <CardHeader>
                                    <CardTitle>Past Events</CardTitle>
                                    <CardDescription>Events that have already occurred.</CardDescription>
                                </CardHeader>
                                <CardContent>
                                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                                        {pastEvents.length > 0 ? (
                                            pastEvents.map((event) => (
                                                <EventCard key={event.eventId} event={event} />
                                            ))
                                        ) : (
                                            <div className="text-muted-foreground text-center p-8 col-span-full">
                                                No past events to show.
                                            </div>
                                        )}
                                    </div>
                                </CardContent>
                            </Card>
                        </TabsContent>

                        <TabsContent value="notifications" className="mt-0">
                            <Card>
                                <CardHeader>
                                    <CardTitle>Notifications</CardTitle>
                                    <CardDescription>Stay updated with your events.</CardDescription>
                                </CardHeader>
                                <CardContent>
                                    <div className="space-y-4">
                                        {notifications.length > 0 ? (
                                            notifications.map((n) => (
                                                <div
                                                    key={n.notificationId}
                                                    className={`p-4 border rounded-lg ${!n.isRead ? "bg-muted/20" : ""}`}
                                                >
                                                    <div className="flex justify-between items-start">
                                                        <div>
                                                            <h3 className="font-semibold">{n.title}</h3>
                                                            <p className="text-sm mt-1">{n.message}</p>
                                                            <p className="text-xs text-muted-foreground mt-1">
                                                                {new Date(n.createdDate).toLocaleString()}
                                                            </p>
                                                        </div>
                                                        <div className="space-x-2">
                                                            {!n.isRead && (
                                                                <Button
                                                                    onClick={async () => {
                                                                        try {
                                                                            await NotificationsAPI.markAsRead(n.notificationId, user!.userId);
                                                                            fetchUserData(user!.userId);
                                                                        } catch (err) {
                                                                            console.error("Okundu işaretleme hatası", err);
                                                                        }
                                                                    }}
                                                                >
                                                                    Mark as Read
                                                                </Button>

                                                            )}
                                                            <Button
                                                                size="sm"
                                                                variant="destructive"
                                                                onClick={async () => {
                                                                    try {
                                                                        await NotificationsAPI.delete(n.notificationId, user!.userId);
                                                                        fetchUserData(user!.userId);
                                                                    } catch (err) {
                                                                        console.error("Bildirim silinemedi", err);
                                                                    }
                                                                }}
                                                            >
                                                                Delete
                                                            </Button>
                                                        </div>
                                                    </div>
                                                    {!n.isRead && <Badge variant="secondary" className="mt-2">New</Badge>}
                                                </div>
                                            ))
                                        ) : (
                                            <div className="text-muted-foreground text-center p-8">
                                                No notifications.
                                            </div>
                                        )}
                                    </div>
                                </CardContent>
                            </Card>
                        </TabsContent>


                    </Tabs>
                </div>
            </div>
        </div>
    );
};

export default Dashboard;