import axios, { AxiosResponse } from "axios";
import { OrganizationDto } from "@/types/OrganizationDto";

type CreateOrganizationPayload = Omit<OrganizationDto, "organizationId" | "createdDate">;
type UpdateOrganizationPayload = Partial<CreateOrganizationPayload>;

export const OrganizationsAPI = {
    getAll: (): Promise<AxiosResponse<OrganizationDto[]>> =>
        axios.get("/api/Organizations"),

    getById: (id: number): Promise<AxiosResponse<OrganizationDto>> =>
        axios.get(`/api/Organizations/${id}`),

    create: (
        data: CreateOrganizationPayload,
        userId: number
    ): Promise<AxiosResponse<OrganizationDto>> =>
        axios.post(`/api/Organizations?userId=${userId}`, data),

    update: (
        id: number,
        data: UpdateOrganizationPayload,
        userId: number
    ): Promise<AxiosResponse<void>> =>
        axios.put(`/api/Organizations/${id}?userId=${userId}`, data),

    delete: (id: number, userId: number): Promise<AxiosResponse<void>> =>
        axios.delete(`/api/Organizations/${id}?userId=${userId}`)
};
