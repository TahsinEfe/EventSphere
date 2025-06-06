import { EventDto } from "@/types/EventDto";
import api from "./api"; 
import { AxiosResponse } from "axios";

export const EventsAPI = {
    getAll: (): Promise<AxiosResponse<EventDto[]>> => api.get("/Events"),
    get: (id: number): Promise<AxiosResponse<EventDto>> => api.get(`/Events/${id}`),
    create: (data: EventDto): Promise<AxiosResponse<EventDto>> => api.post("/Events", data),
    update: (id: number, data: EventDto): Promise<AxiosResponse<void>> => api.put(`/Events/${id}`, data),
    delete: (id: number): Promise<AxiosResponse<void>> => api.delete(`/Events/${id}`),

    createFormData: (data: FormData) =>
        api.post("/Events", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        })

};


