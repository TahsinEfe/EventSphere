
export enum UserRole {
  Admin = 1,
  Organizer = 2,
  User = 3,
}

export enum EventType {
  Concert = 1,
  Conference = 2,
  Sport = 3,
  Theater = 4,
  Festival = 5,
  Other = 6,
}

export enum EventStatus {
  Upcoming = 1,
  Live = 2,
  Completed = 3,
  Canceled = 4,
}

export enum TaskStatus {
  Pending = 1,
  InProgress = 2,
  Completed = 3,
  Canceled = 4,
}

export interface User {
  id: number;
  username: string;
  firstName?: string;
  lastName?: string;
  email: string;
  isActive: boolean;
  roleId: UserRole;
}

export interface Organization {
  id: number;
  name: string;
  contactEmail?: string;
  isActive: boolean;
}

export interface Address {
  id: number;
  street?: string;
  city?: string;
  district?: string;
  postalCode?: string;
  country?: string;
}

export interface Event {
  id: number;
  organizationId: number;
  organizationName: string;
  name: string;
  startDateTime: string;
  endDateTime: string;
  eventTypeId: EventType;
  eventStatusId: EventStatus;
  organizerUserId?: number;
  maxAttendees?: number;
  isPublic: boolean;
  registrationDeadline?: string;
  address?: Address;
  imageUrl?: string;
  description?: string;
  price?: number;
  availableSeats?: number;
}

export interface EventRegistration {
  id: number;
  eventId: number;
  userId: number;
  registrationDate: string;
  eventName?: string;
  eventImage?: string;
  eventDate?: string;
}

export interface AuthState {
  user: User | null;
  isAuthenticated: boolean;
}
