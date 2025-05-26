
import { useState } from "react";
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Textarea } from "@/components/ui/textarea";
import { Event, EventType, User, UserRole } from "@/types";
import { useToast } from "@/components/ui/use-toast";

interface AdminEditUserModalProps {
  isOpen: boolean;
  onClose: () => void;
  user?: User;
  onSave: (user: User) => void;
}

interface AdminEditEventModalProps {
  isOpen: boolean;
  onClose: () => void;
  event?: Event;
  onSave: (event: Event) => void;
}

export const AdminEditUserModal = ({ isOpen, onClose, user, onSave }: AdminEditUserModalProps) => {
  const { toast } = useToast();
  const [editedUser, setEditedUser] = useState<Partial<User>>(user || {});
  
  const handleSave = () => {
    if (!editedUser.username || !editedUser.email) {
      toast({
        title: "Error",
        description: "Please fill out all required fields.",
        variant: "destructive",
      });
      return;
    }
    
    onSave(editedUser as User);
    toast({
      title: "Success",
      description: "User has been updated successfully.",
    });
    onClose();
  };
  
  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent className="sm:max-w-[500px]">
        <DialogHeader>
          <DialogTitle>{user ? "Edit User" : "Add User"}</DialogTitle>
          <DialogDescription>
            Make changes to the user account here. Click save when you're done.
          </DialogDescription>
        </DialogHeader>
        
        <div className="grid gap-4 py-4">
          <div className="grid grid-cols-2 gap-4">
            <div>
              <Label htmlFor="firstName">First Name</Label>
              <Input
                id="firstName"
                value={editedUser.firstName || ""}
                onChange={(e) => setEditedUser({ ...editedUser, firstName: e.target.value })}
              />
            </div>
            <div>
              <Label htmlFor="lastName">Last Name</Label>
              <Input
                id="lastName"
                value={editedUser.lastName || ""}
                onChange={(e) => setEditedUser({ ...editedUser, lastName: e.target.value })}
              />
            </div>
          </div>
          
          <div>
            <Label htmlFor="username">Username</Label>
            <Input
              id="username"
              value={editedUser.username || ""}
              onChange={(e) => setEditedUser({ ...editedUser, username: e.target.value })}
              required
            />
          </div>
          
          <div>
            <Label htmlFor="email">Email</Label>
            <Input
              id="email"
              value={editedUser.email || ""}
              onChange={(e) => setEditedUser({ ...editedUser, email: e.target.value })}
              required
            />
          </div>
          
          <div>
            <Label htmlFor="role">Role</Label>
            <Select
              value={editedUser.roleId?.toString()}
              onValueChange={(value) => setEditedUser({ ...editedUser, roleId: parseInt(value) as UserRole })}
            >
              <SelectTrigger id="role">
                <SelectValue placeholder="Select role" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value={UserRole.Admin.toString()}>Admin</SelectItem>
                <SelectItem value={UserRole.Organizer.toString()}>Organizer</SelectItem>
                <SelectItem value={UserRole.User.toString()}>User</SelectItem>
              </SelectContent>
            </Select>
          </div>
          
          <div className="flex items-center space-x-2">
            <input
              type="checkbox"
              id="isActive"
              checked={editedUser.isActive}
              onChange={(e) => setEditedUser({ ...editedUser, isActive: e.target.checked })}
            />
            <Label htmlFor="isActive">Active</Label>
          </div>
        </div>
        
        <DialogFooter>
          <Button variant="outline" onClick={onClose}>Cancel</Button>
          <Button onClick={handleSave}>Save Changes</Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export const AdminEditEventModal = ({ isOpen, onClose, event, onSave }: AdminEditEventModalProps) => {
  const { toast } = useToast();
  const [editedEvent, setEditedEvent] = useState<Partial<Event>>(event || {
    isPublic: true,
    organizationId: 1,
    organizationName: "Default Organization",
    eventTypeId: EventType.Other,
    eventStatusId: 1
  });
  
  const handleSave = () => {
    if (!editedEvent.name || !editedEvent.startDateTime) {
      toast({
        title: "Error",
        description: "Please fill out all required fields.",
        variant: "destructive",
      });
      return;
    }
    
    onSave(editedEvent as Event);
    toast({
      title: "Success",
      description: "Event has been updated successfully.",
    });
    onClose();
  };
  
  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent className="sm:max-w-[600px]">
        <DialogHeader>
          <DialogTitle>{event ? "Edit Event" : "Add Event"}</DialogTitle>
          <DialogDescription>
            Make changes to the event details here. Click save when you're done.
          </DialogDescription>
        </DialogHeader>
        
        <div className="grid gap-4 py-4">
          <div>
            <Label htmlFor="name">Event Name</Label>
            <Input
              id="name"
              value={editedEvent.name || ""}
              onChange={(e) => setEditedEvent({ ...editedEvent, name: e.target.value })}
              required
            />
          </div>
          
          <div className="grid grid-cols-2 gap-4">
            <div>
              <Label htmlFor="startDateTime">Start Date & Time</Label>
              <Input
                id="startDateTime"
                type="datetime-local"
                value={editedEvent.startDateTime ? new Date(editedEvent.startDateTime).toISOString().slice(0, 16) : ""}
                onChange={(e) => setEditedEvent({ ...editedEvent, startDateTime: e.target.value })}
                required
              />
            </div>
            <div>
              <Label htmlFor="endDateTime">End Date & Time</Label>
              <Input
                id="endDateTime"
                type="datetime-local"
                value={editedEvent.endDateTime ? new Date(editedEvent.endDateTime).toISOString().slice(0, 16) : ""}
                onChange={(e) => setEditedEvent({ ...editedEvent, endDateTime: e.target.value })}
              />
            </div>
          </div>
          
          <div className="grid grid-cols-2 gap-4">
            <div>
              <Label htmlFor="eventType">Event Type</Label>
              <Select
                value={editedEvent.eventTypeId?.toString()}
                onValueChange={(value) => setEditedEvent({ ...editedEvent, eventTypeId: parseInt(value) as EventType })}
              >
                <SelectTrigger id="eventType">
                  <SelectValue placeholder="Select type" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value={EventType.Concert.toString()}>Concert</SelectItem>
                  <SelectItem value={EventType.Conference.toString()}>Conference</SelectItem>
                  <SelectItem value={EventType.Theater.toString()}>Theater</SelectItem>
                  <SelectItem value={EventType.Sport.toString()}>Sport</SelectItem>
                  <SelectItem value={EventType.Festival.toString()}>Festival</SelectItem>
                  <SelectItem value={EventType.Other.toString()}>Other</SelectItem>
                </SelectContent>
              </Select>
            </div>
            <div>
              <Label htmlFor="price">Price (TL)</Label>
              <Input
                id="price"
                type="number"
                value={editedEvent.price || ""}
                onChange={(e) => setEditedEvent({ ...editedEvent, price: parseFloat(e.target.value) })}
              />
            </div>
          </div>
          
          <div>
            <Label htmlFor="description">Description</Label>
            <Textarea
              id="description"
              value={editedEvent.description || ""}
              onChange={(e) => setEditedEvent({ ...editedEvent, description: e.target.value })}
              className="min-h-[100px]"
            />
          </div>
          
          <div className="grid grid-cols-2 gap-4">
            <div>
              <Label htmlFor="maxAttendees">Max Attendees</Label>
              <Input
                id="maxAttendees"
                type="number"
                value={editedEvent.maxAttendees || ""}
                onChange={(e) => setEditedEvent({ ...editedEvent, maxAttendees: parseInt(e.target.value) })}
              />
            </div>
            <div>
              <Label htmlFor="imageUrl">Image URL</Label>
              <Input
                id="imageUrl"
                value={editedEvent.imageUrl || ""}
                onChange={(e) => setEditedEvent({ ...editedEvent, imageUrl: e.target.value })}
              />
            </div>
          </div>
          
          <div className="flex items-center space-x-2">
            <input
              type="checkbox"
              id="isPublic"
              checked={editedEvent.isPublic}
              onChange={(e) => setEditedEvent({ ...editedEvent, isPublic: e.target.checked })}
            />
            <Label htmlFor="isPublic">Public Event</Label>
          </div>
        </div>
        
        <DialogFooter>
          <Button variant="outline" onClick={onClose}>Cancel</Button>
          <Button onClick={handleSave}>Save Changes</Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};
