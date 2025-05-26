import api from "./api";
import { EventRegistration } from "@/types/EventRegistration";

export const EventRegistrationsAPI = {
    getByUserId: (userId: number) =>
        api.get<EventRegistration[]>(`/EventRegistrations/user/${userId}`),
};
