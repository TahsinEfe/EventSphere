import axios, { AxiosResponse } from "axios";
import { OrganizationMembersDto } from "@/types/OrganizationMembersDto";

type CreateMemberPayload = Omit<
    OrganizationMembersDto,
    "memberId" | "joinDate" | "organizationName" | "userName"
>;

export const OrganizationMembersAPI = {
    getAll: (): Promise<AxiosResponse<OrganizationMembersDto[]>> =>
        axios.get("/api/OrganizationMembers"), // Fixed case sensitivity

    getById: (id: number): Promise<AxiosResponse<OrganizationMembersDto>> =>
        axios.get(`/api/OrganizationMembers/${id}`), // Fixed case sensitivity

    create: (
        data: CreateMemberPayload,
        userId: number
    ): Promise<AxiosResponse<OrganizationMembersDto>> =>
        axios.post(`/api/OrganizationMembers?userId=${userId}`, data), // Fixed case sensitivity

    update: (
        id: number,
        isAdminOnly: { isAdmin: boolean },
        userId: number
    ): Promise<AxiosResponse<void>> =>
        axios.put(`/api/OrganizationMembers/${id}?userId=${userId}`, isAdminOnly), // Fixed case sensitivity

    delete: (id: number, userId: number): Promise<AxiosResponse<void>> =>
        axios.delete(`/api/OrganizationMembers/${id}?userId=${userId}`) // Fixed case sensitivity
};