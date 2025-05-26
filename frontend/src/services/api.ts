import axios, { AxiosInstance } from "axios";
import { EventDto } from "@/types/EventDto";
import { UsersDto } from "../types/UsersDto";
import { RolesDto } from "../types/RolesDto";
import { AddressesDto } from "../types/AddressesDto";
import { TasksDto } from "../types/TasksDto";
import { TaskStatusesDto } from "../types/TaskStatusesDto";
import { FeedbacksDto } from "../types/FeedbacksDto";
import { SeatsDto } from "../types/SeatsDto";
import { EventTypesDto } from "../types/EventTypesDto";
import { EventStatusesDto } from "../types/EventStatusesDto";
import { OrganizationMembersDto } from "../types/OrganizationMembersDto";




const api: AxiosInstance = axios.create({
    baseURL: "http://localhost:5172/api",
    headers: {
        "Content-Type": "application/json",
        "Accept": "application/json",
    },
});

export const EventsAPI = {
    getAll: () => api.get("/Events"),
    get: (id: number) => api.get(`/Events/${id}`),
    create: (data: EventDto) => api.post("/Events", data),
    update: (id: number, data: EventDto) => api.put(`/Events/${id}`, data),
    delete: (id: number) => api.delete(`/Events/${id}`),
};

export const UsersAPI = {
    getAll: () => api.get("/Users"),
    get: (id: number) => api.get(`/Users/${id}`),
    register: (data: UsersDto) => api.post("/Users", data),
    update: (id: number, data: UsersDto) => api.put(`/Users/${id}`, data),
    delete: (id: number) => api.delete(`/Users/${id}`),
};

export const RolesAPI = {
    getAll: () => api.get("/Roles"),
    get: (id: number) => api.get(`/Roles/${id}`),
    create: (userId: number, data: RolesDto) => api.post(`/Roles?userId=${userId}`, data),
    update: (id: number, userId: number, data: RolesDto) => api.put(`/Roles/${id}?userId=${userId}`, data),
    delete: (id: number, userId: number) => api.delete(`/Roles/${id}?userId=${userId}`),
};

export const AddressesAPI = {
    getAll: () => api.get("/Addresses"),
    get: (id: number) => api.get(`/Addresses/${id}`),
    create: (data: AddressesDto) => api.post("/Addresses", data),
    update: (id: number, data: AddressesDto) => api.put(`/Addresses/${id}`, data),
    delete: (id: number) => api.delete(`/Addresses/${id}`),
};

export const TasksAPI = {
    getAll: () => api.get("/Tasks"),
    get: (id: number) => api.get(`/Tasks/${id}`),
    create: (userId: number, data: TasksDto) => api.post(`/Tasks?userId=${userId}`, data),
    update: (id: number, userId: number, data: TasksDto) => api.put(`/Tasks/${id}?userId=${userId}`, data),
    delete: (id: number, userId: number) => api.delete(`/Tasks/${id}?userId=${userId}`),
};

export const TaskStatusesAPI = {
    getAll: () => api.get("/TaskStatuses"),
    get: (id: number) => api.get(`/TaskStatuses/${id}`),
    create: (userId: number, data: TaskStatusesDto) => api.post(`/TaskStatuses?userId=${userId}`, data),
    update: (id: number, userId: number, data: TaskStatusesDto) => api.put(`/TaskStatuses/${id}?userId=${userId}`, data),
};

export const FeedbacksAPI = {
    getAll: () => api.get("/Feedbacks"),
    get: (id: number) => api.get(`/Feedbacks/${id}`),
    create: (userId: number, data: FeedbacksDto) => api.post(`/Feedbacks?userId=${userId}`, data),
    update: (id: number, userId: number, data: FeedbacksDto) => api.put(`/Feedbacks/${id}?userId=${userId}`, data),
    delete: (id: number, userId: number) => api.delete(`/Feedbacks/${id}?userId=${userId}`),
};

export const SeatsAPI = {
    getAll: () => api.get("/Seats"),
    get: (id: number) => api.get(`/Seats/${id}`),
    create: (userId: number, data: SeatsDto) => api.post(`/Seats?userId=${userId}`, data),
    update: (id: number, userId: number, data: SeatsDto) => api.put(`/Seats/${id}?userId=${userId}`, data),
    delete: (id: number, userId: number) => api.delete(`/Seats/${id}?userId=${userId}`),
};

export const EventTypesAPI = {
    getAll: () => api.get("/EventTypes"),
    get: (id: number) => api.get(`/EventTypes/${id}`),
    create: (userId: number, data: EventTypesDto) => api.post(`/EventTypes?userId=${userId}`, data),
    update: (id: number, userId: number, data: EventTypesDto) => api.put(`/EventTypes/${id}?userId=${userId}`, data),
};

export const EventStatusesAPI = {
    getAll: () => api.get("/EventStatuses"),
    get: (id: number) => api.get(`/EventStatuses/${id}`),
    create: (userId: number, data: EventStatusesDto) => api.post(`/EventStatuses?userId=${userId}`, data),
    update: (id: number, userId: number, data: EventStatusesDto) => api.put(`/EventStatuses/${id}?userId=${userId}`, data),
};

export const OrganizationMembersAPI = {
    getAll: () => api.get("/OrganizationMembers"),
    get: (id: number) => api.get(`/OrganizationMembers/${id}`),
    create: (userId: number, data: OrganizationMembersDto) => api.post(`/OrganizationMembers?userId=${userId}`, data),
    update: (id: number, userId: number, data: OrganizationMembersDto) => api.put(`/OrganizationMembers/${id}?userId=${userId}`, data),
    delete: (id: number, userId: number) => api.delete(`/OrganizationMembers/${id}?userId=${userId}`),
};

export default api;