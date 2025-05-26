// src/types/EventRegistration.ts

export interface EventRegistration {
    id: number;
    userId: number;
    eventId: number;
    eventName: string;
    eventImage?: string;
    eventDate: string;
    registrationDate: string;
}
