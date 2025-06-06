import axios from "axios";

export interface CreateEventDto {
    organizationId: number;
    name: string;
    startDateTime: string;
    endDateTime: string;
    eventTypeId: number;
    eventStatusId: number;
    organizerUserId: number;
    isPublic: boolean;
    location?: string;
    description?: string;
    maxAttendees?: number;
    registrationDeadline?: string;
    imageFile?: File; // string değil, File
}

const createEvent = async (eventData: CreateEventDto) => {
    const formData = new FormData();

    formData.append("OrganizationId", eventData.organizationId.toString());
    formData.append("Name", eventData.name);
    formData.append("StartDateTime", eventData.startDateTime); // ISO format olmalı
    formData.append("EndDateTime", eventData.endDateTime);
    formData.append("EventTypeId", eventData.eventTypeId.toString());
    formData.append("EventStatusId", eventData.eventStatusId.toString());
    formData.append("OrganizerUserId", eventData.organizerUserId.toString());
    formData.append("IsPublic", eventData.isPublic.toString());

    if (eventData.location) formData.append("Location", eventData.location);
    if (eventData.description) formData.append("Description", eventData.description);
    if (eventData.maxAttendees !== undefined) formData.append("MaxAttendees", eventData.maxAttendees.toString());
    if (eventData.registrationDeadline) formData.append("RegistrationDeadline", eventData.registrationDeadline);
    if (eventData.imageFile) formData.append("ImageFile", eventData.imageFile);

    // axios ile gönder
    return axios.post("/api/events", formData, {
        headers: {
            "Content-Type": "multipart/form-data"
        }
    });
};
