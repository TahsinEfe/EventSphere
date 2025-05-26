
import { useState } from "react";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Checkbox } from "@/components/ui/checkbox";
import { UserRole, EventType, Event } from "@/types";
import { mockEvents } from "@/data/mockData";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { User, Settings, Edit, Trash } from "lucide-react";

// Mock users for admin panel
const mockUsers = [
  { 
    id: 1, 
    username: "admin", 
    firstName: "Admin", 
    lastName: "User", 
    email: "admin@example.com", 
    isActive: true, 
    roleId: UserRole.Admin 
  },
  { 
    id: 2, 
    username: "johndoe", 
    firstName: "John", 
    lastName: "Doe", 
    email: "john@example.com", 
    isActive: true, 
    roleId: UserRole.Organizer 
  },
  { 
    id: 3, 
    username: "janedoe", 
    firstName: "Jane", 
    lastName: "Doe", 
    email: "jane@example.com", 
    isActive: true, 
    roleId: UserRole.User 
  },
  { 
    id: 4, 
    username: "bobsmith", 
    firstName: "Bob", 
    lastName: "Smith", 
    email: "bob@example.com", 
    isActive: false, 
    roleId: UserRole.User 
  },
  { 
    id: 5, 
    username: "sarahjones", 
    firstName: "Sarah", 
    lastName: "Jones", 
    email: "sarah@example.com", 
    isActive: true, 
    roleId: UserRole.Organizer 
  }
];

const getRoleName = (roleId: UserRole): string => {
  switch (roleId) {
    case UserRole.Admin:
      return "Admin";
    case UserRole.Organizer:
      return "Organizer";
    case UserRole.User:
      return "User";
    default:
      return "Unknown";
  }
};

const getRoleBadgeVariant = (roleId: UserRole): "default" | "secondary" | "outline" | "destructive" => {
  switch (roleId) {
    case UserRole.Admin:
      return "destructive";
    case UserRole.Organizer:
      return "secondary";
    case UserRole.User:
      return "outline";
    default:
      return "default";
  }
};

const Admin = () => {
  const [searchUser, setSearchUser] = useState("");
  const [searchEvent, setSearchEvent] = useState("");
  const [users, setUsers] = useState(mockUsers);
  const [events, setEvents] = useState<Event[]>(mockEvents);
  const [selectedUsers, setSelectedUsers] = useState<number[]>([]);
  
  const filteredUsers = users.filter(user => 
    user.username.toLowerCase().includes(searchUser.toLowerCase()) ||
    user.firstName?.toLowerCase().includes(searchUser.toLowerCase()) ||
    user.lastName?.toLowerCase().includes(searchUser.toLowerCase()) ||
    user.email.toLowerCase().includes(searchUser.toLowerCase())
  );
  
  const filteredEvents = events.filter(event =>
    event.name.toLowerCase().includes(searchEvent.toLowerCase()) ||
    event.organizationName.toLowerCase().includes(searchEvent.toLowerCase())
  );
  
  const toggleUserSelection = (userId: number) => {
    setSelectedUsers(prev => 
      prev.includes(userId) 
        ? prev.filter(id => id !== userId) 
        : [...prev, userId]
    );
  };
  
  const toggleUserActive = (userId: number) => {
    setUsers(prev => prev.map(user => 
      user.id === userId 
        ? { ...user, isActive: !user.isActive } 
        : user
    ));
  };
  
  const updateUserRole = (userId: number, roleId: UserRole) => {
    setUsers(prev => prev.map(user => 
      user.id === userId 
        ? { ...user, roleId } 
        : user
    ));
  };
  
  const deleteUser = (userId: number) => {
    setUsers(prev => prev.filter(user => user.id !== userId));
    setSelectedUsers(prev => prev.filter(id => id !== userId));
  };
  
  const handleSelectAll = (checked: boolean) => {
    if (checked) {
      setSelectedUsers(filteredUsers.map(user => user.id));
    } else {
      setSelectedUsers([]);
    }
  };
  
  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      
      <div className="container mx-auto px-4 py-8 flex-1">
        <div className="flex items-center justify-between mb-6">
          <h1 className="text-3xl font-bold">Admin Dashboard</h1>
          <div className="flex items-center">
            <Avatar className="h-8 w-8 mr-2">
              <AvatarFallback>A</AvatarFallback>
            </Avatar>
            <div>
              <p className="text-sm font-medium">Admin User</p>
              <p className="text-xs text-muted-foreground">admin@example.com</p>
            </div>
          </div>
        </div>
        
        <Tabs defaultValue="users" className="w-full">
          <TabsList className="grid grid-cols-2 mb-8">
            <TabsTrigger value="users" className="flex items-center">
              <User className="mr-2 h-4 w-4" />
              Manage Users
            </TabsTrigger>
            <TabsTrigger value="events" className="flex items-center">
              <Settings className="mr-2 h-4 w-4" />
              Manage Events
            </TabsTrigger>
          </TabsList>
          
          <TabsContent value="users">
            <Card>
              <CardHeader>
                <CardTitle>Users</CardTitle>
                <CardDescription>Manage your users and their roles</CardDescription>
              </CardHeader>
              <CardContent>
                <div className="flex flex-col md:flex-row justify-between gap-4 mb-6">
                  <div className="flex-1">
                    <Input
                      placeholder="Search users..."
                      value={searchUser}
                      onChange={(e) => setSearchUser(e.target.value)}
                    />
                  </div>
                  <div className="flex gap-2">
                    <Button variant="outline">Export</Button>
                    <Button>Add User</Button>
                  </div>
                </div>
                
                <div className="border rounded-md">
                  <div className="grid grid-cols-12 gap-4 p-4 border-b bg-muted/50">
                    <div className="col-span-1 flex items-center">
                      <Checkbox
                        checked={selectedUsers.length === filteredUsers.length && filteredUsers.length > 0}
                        onCheckedChange={handleSelectAll}
                      />
                    </div>
                    <div className="col-span-3 font-medium">User</div>
                    <div className="col-span-3 font-medium">Email</div>
                    <div className="col-span-2 font-medium">Role</div>
                    <div className="col-span-1 font-medium">Status</div>
                    <div className="col-span-2 font-medium text-right">Actions</div>
                  </div>
                  
                  {filteredUsers.length > 0 ? (
                    <div>
                      {filteredUsers.map((user) => (
                        <div 
                          key={user.id} 
                          className="grid grid-cols-12 gap-4 p-4 border-b last:border-b-0 hover:bg-muted/50"
                        >
                          <div className="col-span-1 flex items-center">
                            <Checkbox 
                              checked={selectedUsers.includes(user.id)}
                              onCheckedChange={() => toggleUserSelection(user.id)}
                            />
                          </div>
                          <div className="col-span-3 flex items-center">
                            <Avatar className="h-8 w-8 mr-2">
                              <AvatarFallback>
                                {user.firstName?.charAt(0)}{user.lastName?.charAt(0)}
                              </AvatarFallback>
                            </Avatar>
                            <div>
                              <div className="font-medium">{user.firstName} {user.lastName}</div>
                              <div className="text-sm text-muted-foreground">@{user.username}</div>
                            </div>
                          </div>
                          <div className="col-span-3 flex items-center">{user.email}</div>
                          <div className="col-span-2 flex items-center">
                            <Select
                              defaultValue={user.roleId.toString()}
                              onValueChange={(value) => updateUserRole(user.id, parseInt(value) as UserRole)}
                            >
                              <SelectTrigger className="w-full">
                                <SelectValue>
                                  <Badge variant={getRoleBadgeVariant(user.roleId)}>
                                    {getRoleName(user.roleId)}
                                  </Badge>
                                </SelectValue>
                              </SelectTrigger>
                              <SelectContent>
                                <SelectItem value={UserRole.Admin.toString()}>Admin</SelectItem>
                                <SelectItem value={UserRole.Organizer.toString()}>Organizer</SelectItem>
                                <SelectItem value={UserRole.User.toString()}>User</SelectItem>
                              </SelectContent>
                            </Select>
                          </div>
                          <div className="col-span-1 flex items-center">
                            <Badge variant={user.isActive ? "default" : "outline"}>
                              {user.isActive ? "Active" : "Inactive"}
                            </Badge>
                          </div>
                          <div className="col-span-2 flex items-center justify-end space-x-2">
                            <Button 
                              variant="outline" 
                              size="icon"
                              onClick={() => toggleUserActive(user.id)}
                              title={user.isActive ? "Deactivate User" : "Activate User"}
                            >
                              <Settings className="h-4 w-4" />
                            </Button>
                            <Button 
                              variant="outline" 
                              size="icon"
                              title="Edit User"
                            >
                              <Edit className="h-4 w-4" />
                            </Button>
                            <Button 
                              variant="destructive" 
                              size="icon"
                              onClick={() => deleteUser(user.id)}
                              title="Delete User"
                            >
                              <Trash className="h-4 w-4" />
                            </Button>
                          </div>
                        </div>
                      ))}
                    </div>
                  ) : (
                    <div className="p-8 text-center text-muted-foreground">
                      No users found. Try adjusting your search filters.
                    </div>
                  )}
                </div>
                
                <div className="flex justify-between items-center mt-4">
                  <div className="text-sm text-muted-foreground">
                    {selectedUsers.length} of {filteredUsers.length} row(s) selected
                  </div>
                  <div className="flex gap-2">
                    <Button variant="outline" disabled={selectedUsers.length === 0}>
                      Bulk Edit
                    </Button>
                    <Button variant="destructive" disabled={selectedUsers.length === 0}>
                      Delete Selected
                    </Button>
                  </div>
                </div>
              </CardContent>
            </Card>
          </TabsContent>
          
          <TabsContent value="events">
            <Card>
              <CardHeader>
                <CardTitle>Events</CardTitle>
                <CardDescription>Manage events and their settings</CardDescription>
              </CardHeader>
              <CardContent>
                <div className="flex flex-col md:flex-row justify-between gap-4 mb-6">
                  <div className="flex-1">
                    <Input
                      placeholder="Search events..."
                      value={searchEvent}
                      onChange={(e) => setSearchEvent(e.target.value)}
                    />
                  </div>
                  <div className="flex gap-2">
                    <Button variant="outline">Export</Button>
                    <Button>Add Event</Button>
                  </div>
                </div>
                
                <div className="border rounded-md">
                  <div className="grid grid-cols-12 gap-2 p-4 border-b bg-muted/50">
                    <div className="col-span-3 font-medium">Name</div>
                    <div className="col-span-2 font-medium">Type</div>
                    <div className="col-span-2 font-medium">Date</div>
                    <div className="col-span-2 font-medium">Price</div>
                    <div className="col-span-1 font-medium">Status</div>
                    <div className="col-span-2 font-medium text-right">Actions</div>
                  </div>
                  
                  {filteredEvents.length > 0 ? (
                    <div>
                      {filteredEvents.map((event) => (
                        <div 
                          key={event.id} 
                          className="grid grid-cols-12 gap-2 p-4 border-b last:border-b-0 hover:bg-muted/50"
                        >
                          <div className="col-span-3">
                            <div className="font-medium">{event.name}</div>
                            <div className="text-sm text-muted-foreground">{event.organizationName}</div>
                          </div>
                          <div className="col-span-2 flex items-center">
                            {event.eventTypeId === EventType.Concert && <Badge>Concert</Badge>}
                            {event.eventTypeId === EventType.Conference && <Badge variant="secondary">Conference</Badge>}
                            {event.eventTypeId === EventType.Festival && <Badge variant="secondary">Festival</Badge>}
                            {event.eventTypeId === EventType.Sport && <Badge variant="destructive">Sport</Badge>}
                            {event.eventTypeId === EventType.Theater && <Badge variant="outline">Theater</Badge>}
                            {event.eventTypeId === EventType.Other && <Badge variant="default">Other</Badge>}
                          </div>
                          <div className="col-span-2 flex items-center">
                            {new Date(event.startDateTime).toLocaleDateString()}
                          </div>
                          <div className="col-span-2 flex items-center">
                            {event.price ? `${event.price} TL` : "Free"}
                          </div>
                          <div className="col-span-1 flex items-center">
                            <Badge variant={event.isPublic ? "default" : "outline"}>
                              {event.isPublic ? "Public" : "Private"}
                            </Badge>
                          </div>
                          <div className="col-span-2 flex items-center justify-end space-x-2">
                            <Button 
                              variant="outline" 
                              size="icon"
                              title="Edit Event"
                            >
                              <Edit className="h-4 w-4" />
                            </Button>
                            <Button 
                              variant="destructive" 
                              size="icon"
                              title="Delete Event"
                            >
                              <Trash className="h-4 w-4" />
                            </Button>
                          </div>
                        </div>
                      ))}
                    </div>
                  ) : (
                    <div className="p-8 text-center text-muted-foreground">
                      No events found. Try adjusting your search filters.
                    </div>
                  )}
                </div>
              </CardContent>
            </Card>
          </TabsContent>
        </Tabs>
      </div>
      
      <Footer />
    </div>
  );
};

export default Admin;
