import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {
    Card, CardContent, CardDescription, CardHeader, CardTitle,
} from "@/components/ui/card";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Button } from "@/components/ui/button";
import {
    Calendar, Clock, Ticket, User, Bell, Plus, Edit, Building,
} from "lucide-react";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Badge } from "@/components/ui/badge";
import { formatDate } from "@/data/mockData";
import { EventRegistration } from "@/types/EventRegistration";
import { NotificationDto } from "@/types/NotificationDto";
import { LoginResponse } from "@/services/auth";
import { AuthAPI } from "@/services/auth";
import { NotificationsAPI } from "@/services/NotificationsAPI";
import EventCard from "@/components/EventCard";
import { EventDto } from "@/types/EventDto";
import { EventsAPI } from "@/services/EventsAPI";
import { OrganizationsAPI } from "@/services/OrganizationsAPI";
import { OrganizationDto } from "@/types/OrganizationDto";


const Dashboard = () => {
    const [activeTab, setActiveTab] = useState("upcoming");
    const [user, setUser] = useState<LoginResponse | null>(null);
    const [notifications, setNotifications] = useState<NotificationDto[]>([]);
    const [upcomingEvents, setUpcomingEvents] = useState<EventDto[]>([]);
    const [registeredEvents, setRegisteredEvents] = useState<EventDto[]>([]);
    const [pastEvents, setPastEvents] = useState<EventDto[]>([]);
    const [organizations, setOrganizations] = useState<OrganizationDto[]>([]);

    useEffect(() => {
        const currentUser = AuthAPI.getCurrentUser();
        if (currentUser) {
            setUser(currentUser);
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

            // Filter upcoming events (public events)
            const upcoming = allEvents
                .filter((e: EventDto) => new Date(e.startDateTime) > now)
                .sort((a, b) => new Date(a.startDateTime).getTime() - new Date(b.startDateTime).getTime());

            // Filter past events
            const past = allEvents
                .filter((e: EventDto) => new Date(e.startDateTime) <= now)
                .sort((a, b) => new Date(b.startDateTime).getTime() - new Date(a.startDateTime).getTime());

            setUpcomingEvents(upcoming);
            setPastEvents(past);

            // If user is logged in, fetch their registered events
            const currentUser = AuthAPI.getCurrentUser();
            if (currentUser) {
                await fetchUserRegisteredEvents(currentUser.userId, allEvents);
            }
        } catch (err) {
            console.error("Event fetch error:", err);
            setUpcomingEvents([]);
            setPastEvents([]);
        }
    };

    const fetchUserRegisteredEvents = async (userId: number, allEvents: EventDto[]) => {
        try {
            // This would typically be a separate API call to get user's registered events
            // For now, we'll use a placeholder - you'll need to implement this based on your API
            // const registrations = await EventRegistrationAPI.getByUserId(userId);
            // const userEventIds = registrations.data.map(r => r.eventId);
            // const userEvents = allEvents.filter(e => userEventIds.includes(e.eventId));
            // setRegisteredEvents(userEvents);

            // Placeholder for now
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
                                    <Button variant="ghost" className="w-full justify-start">
                                        <Ticket className="mr-2 h-4 w-4" />
                                        My Events
                                        <Badge className="ml-auto">
                                            {registeredEvents.length}
                                        </Badge>
                                    </Button>
                                    <Link to="/organizations">
                                        <Button variant="ghost" className="w-full justify-start">
                                            <Building className="mr-2 h-4 w-4" />
                                            Organizations
                                        </Button>
                                    </Link>
                                    <Link to="/edit-profile">
                                        <Button variant="ghost" className="w-full justify-start">
                                            <User className="mr-2 h-4 w-4" />
                                            Profile
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
                                <TabsTrigger value="registered">My Events</TabsTrigger>
                                <TabsTrigger value="past">Past Events</TabsTrigger>
                                <TabsTrigger value="notifications">Notifications</TabsTrigger>
                            </TabsList>

                            <Link to="/events">
                                <Button size="sm">
                                    <Plus className="mr-2 h-4 w-4" />
                                    Find Events
                                </Button>
                            </Link>
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
                                                    key={n.id}
                                                    className={`p-4 border rounded-lg ${!n.isRead ? "bg-muted/20" : ""}`}
                                                >
                                                    <div className="flex justify-between items-start">
                                                        <h3 className="font-semibold">{n.title}</h3>
                                                        <span className="text-xs text-muted-foreground">
                                                            {new Date(n.createdDate).toLocaleDateString()}
                                                        </span>
                                                    </div>
                                                    <p className="text-sm mt-1">{n.message}</p>
                                                    {!n.isRead && (
                                                        <Badge variant="secondary" className="mt-2">New</Badge>
                                                    )}
                                                </div>
                                            ))
                                        ) : (
                                            <div className="text-muted-foreground text-center p-8">No notifications.</div>
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