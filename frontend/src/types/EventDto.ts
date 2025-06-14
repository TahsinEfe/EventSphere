export interface EventDto {
    eventId?: number; // create işlemi sırasında olmayabilir
    organizationId: number;
    name: string;
    startDateTime: string;
    endDateTime: string;
    eventTypeId: number;
    eventStatusId: number;
    organizerUserId?: number;
    maxAttendees?: number;
    isPublic: boolean;
    registrationDeadline?: string;
    addressId: number
    location?: string; // sadece GET işlemlerinde geliyor olabilir
    imageUrl?: File | string;
    description?: string;
    city: string; // sadece GET işlemlerinde geliyor olabilir
    organizationName?: string;
    country: string; // sadece GET işlemlerinde geliyor olabilir
}
