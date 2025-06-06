import axios, { AxiosResponse } from "axios";
import { OrganizationDto } from "@/types/OrganizationDto";
import api from "./api";

type CreateOrganizationPayload = Omit<OrganizationDto, "organizationId" | "createdDate">;
type UpdateOrganizationPayload = Partial<CreateOrganizationPayload>;

export const OrganizationsAPI = {
    getAll: (): Promise<AxiosResponse<OrganizationDto[]>> =>
        api.get("/Organizations"), 

    getById: (id: number): Promise<AxiosResponse<OrganizationDto>> =>
        api.get(`/Organizations/${id}`),

    create: (
        data: CreateOrganizationPayload,
        userId: number
    ): Promise<AxiosResponse<OrganizationDto>> =>
        api.post(`/Organizations?userId=${userId}`, data),

    update: (
        id: number,
        data: UpdateOrganizationPayload,
        userId: number
    ): Promise<AxiosResponse<void>> =>
        api.put(`/Organizations/${id}?userId=${userId}`, data),

    delete: (id: number, userId: number): Promise<AxiosResponse<void>> =>
        api.delete(`/Organizations/${id}?userId=${userId}`)
};
