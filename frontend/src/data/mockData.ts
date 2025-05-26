
import { Event, EventRegistration, EventStatus, EventType, UserRole } from "@/types";

export const mockEvents: Event[] = [
  {
    id: 1,
    organizationId: 1,
    organizationName: "Live Nation Entertainment",
    name: "Summer Rock Festival",
    startDateTime: "2025-07-15T18:00:00",
    endDateTime: "2025-07-15T23:00:00",
    eventTypeId: EventType.Concert,
    eventStatusId: EventStatus.Upcoming,
    organizerUserId: 2,
    maxAttendees: 5000,
    isPublic: true,
    registrationDeadline: "2025-07-10T23:59:59",
    imageUrl: "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1470&q=80",
    description: "Join us for the biggest rock festival of the summer featuring top bands from around the world. Experience amazing music, great food, and unforgettable memories.",
    price: 150,
    availableSeats: 2500,
    address: {
      id: 1,
      street: "123 Festival Grounds",
      city: "Istanbul",
      district: "Kadıköy",
      postalCode: "34000",
      country: "Turkey"
    }
  },
  {
    id: 2,
    organizationId: 2,
    organizationName: "Tech Conferences Inc.",
    name: "Web Development Summit 2025",
    startDateTime: "2025-06-05T09:00:00",
    endDateTime: "2025-06-07T18:00:00",
    eventTypeId: EventType.Conference,
    eventStatusId: EventStatus.Upcoming,
    maxAttendees: 800,
    isPublic: true,
    registrationDeadline: "2025-05-20T23:59:59",
    imageUrl: "https://images.unsplash.com/photo-1540575467063-178a50c2df87?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1470&q=80",
    description: "The premier web development conference covering the latest technologies, frameworks, and best practices in the industry. Network with peers and learn from experts.",
    price: 300,
    availableSeats: 500,
    address: {
      id: 2,
      street: "456 Tech Center",
      city: "Ankara",
      district: "Çankaya",
      postalCode: "06000",
      country: "Turkey"
    }
  },
  {
    id: 3,
    organizationId: 3,
    organizationName: "National Theater Association",
    name: "Romeo and Juliet",
    startDateTime: "2025-05-30T19:30:00",
    endDateTime: "2025-05-30T22:00:00",
    eventTypeId: EventType.Theater,
    eventStatusId: EventStatus.Upcoming,
    maxAttendees: 350,
    isPublic: true,
    registrationDeadline: "2025-05-29T18:00:00",
    imageUrl: "https://images.unsplash.com/photo-1607998803461-4e9aef3be418?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1470&q=80",
    description: "Experience Shakespeare's timeless tale of love and tragedy in this modern adaptation by award-winning director. An unforgettable theater experience.",
    price: 75,
    availableSeats: 200,
    address: {
      id: 3,
      street: "789 Arts Boulevard",
      city: "Istanbul",
      district: "Beyoğlu",
      postalCode: "34000",
      country: "Turkey"
    }
  },
  {
    id: 4,
    organizationId: 4,
    organizationName: "Sports Management Agency",
    name: "Championship Basketball Finals",
    startDateTime: "2025-06-15T17:00:00",
    endDateTime: "2025-06-15T19:30:00",
    eventTypeId: EventType.Sport,
    eventStatusId: EventStatus.Upcoming,
    maxAttendees: 12000,
    isPublic: true,
    registrationDeadline: "2025-06-14T23:59:59",
    imageUrl: "https://images.unsplash.com/photo-1504450758481-7338eba7524a?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1469&q=80",
    description: "Witness the thrilling conclusion of the national basketball championship between the top two teams in the country. Don't miss this epic showdown!",
    price: 120,
    availableSeats: 9000,
    address: {
      id: 4,
      street: "1000 Stadium Complex",
      city: "Istanbul",
      district: "Şişli",
      postalCode: "34000",
      country: "Turkey"
    }
  },
  {
    id: 5,
    organizationId: 5,
    organizationName: "City Cultural Department",
    name: "Annual Food Festival",
    startDateTime: "2025-07-05T11:00:00",
    endDateTime: "2025-07-07T22:00:00",
    eventTypeId: EventType.Festival,
    eventStatusId: EventStatus.Upcoming,
    isPublic: true,
    imageUrl: "https://images.unsplash.com/photo-1555939594-58d7cb561ad1?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1374&q=80",
    description: "Celebrate the diverse culinary traditions with local and international cuisine, cooking demonstrations, and entertainment for the whole family.",
    price: 20,
    availableSeats: 5000,
    address: {
      id: 5,
      street: "Park Plaza",
      city: "Izmir",
      district: "Konak",
      postalCode: "35000",
      country: "Turkey"
    }
  },
  {
    id: 6,
    organizationId: 2,
    organizationName: "Tech Conferences Inc.",
    name: "AI Innovation Summit",
    startDateTime: "2025-08-10T09:00:00",
    endDateTime: "2025-08-12T17:00:00",
    eventTypeId: EventType.Conference,
    eventStatusId: EventStatus.Upcoming,
    maxAttendees: 600,
    isPublic: true,
    registrationDeadline: "2025-07-25T23:59:59",
    imageUrl: "https://images.unsplash.com/photo-1531482615713-2afd69097998?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1470&q=80",
    description: "Explore the frontier of artificial intelligence and machine learning with industry pioneers. Featuring workshops, panel discussions, and networking sessions.",
    price: 400,
    availableSeats: 300,
    address: {
      id: 6,
      street: "Tech Innovation Center",
      city: "Istanbul",
      district: "Maslak",
      postalCode: "34000",
      country: "Turkey"
    }
  }
];

export const mockRegistrations: EventRegistration[] = [
  {
    id: 1,
    eventId: 1,
    userId: 1,
    registrationDate: "2025-05-15T10:30:00",
    eventName: "Summer Rock Festival",
    eventImage: "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1470&q=80",
    eventDate: "2025-07-15T18:00:00"
  },
  {
    id: 2,
    eventId: 3,
    userId: 1,
    registrationDate: "2025-05-10T14:15:00",
    eventName: "Romeo and Juliet",
    eventImage: "https://images.unsplash.com/photo-1607998803461-4e9aef3be418?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1470&q=80",
    eventDate: "2025-05-30T19:30:00"
  }
];

export const mockUser = {
  id: 1,
  username: "demo_user",
  firstName: "Demo",
  lastName: "User",
  email: "demo@example.com",
  isActive: true,
  roleId: UserRole.User
};

export const getEventTypeLabel = (type: EventType): string => {
  const labels = {
    [EventType.Concert]: "Concert",
    [EventType.Conference]: "Conference",
    [EventType.Sport]: "Sport",
    [EventType.Theater]: "Theater",
    [EventType.Festival]: "Festival",
    [EventType.Other]: "Other"
  };
  return labels[type] || "Unknown";
};

export const getEventStatusLabel = (status: EventStatus): string => {
  const labels = {
    [EventStatus.Upcoming]: "Upcoming",
    [EventStatus.Live]: "Live",
    [EventStatus.Completed]: "Completed",
    [EventStatus.Canceled]: "Canceled"
  };
  return labels[status] || "Unknown";
};

export const formatDate = (dateString: string): string => {
  const options: Intl.DateTimeFormatOptions = { 
    year: 'numeric', 
    month: 'long', 
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  };
  return new Date(dateString).toLocaleDateString('en-US', options);
};

export const formatEventDate = (startDate: string, endDate: string): string => {
  const start = new Date(startDate);
  const end = new Date(endDate);
  
  const sameDay = start.getDate() === end.getDate() &&
                  start.getMonth() === end.getMonth() &&
                  start.getFullYear() === end.getFullYear();
  
  const startOptions: Intl.DateTimeFormatOptions = { 
    month: 'long', 
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  };
  
  if (sameDay) {
    const endOptions: Intl.DateTimeFormatOptions = { 
      hour: '2-digit',
      minute: '2-digit'
    };
    return `${start.toLocaleDateString('en-US', startOptions)} - ${end.toLocaleTimeString('en-US', endOptions)}`;
  } else {
    return `${start.toLocaleDateString('en-US', startOptions)} - ${formatDate(endDate)}`;
  }
};
