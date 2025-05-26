import axios, { AxiosResponse } from "axios";
import { OrganizationMemberDto } from "@/types/OrganizationMemberDto";

type CreateMemberPayload = Omit<
    OrganizationMemberDto,
    "memberId" | "joinDate" | "organizationName" | "userName"
>;

export const OrganizationMembersAPI = {
    getAll: (): Promise<AxiosResponse<OrganizationMemberDto[]>> =>
        axios.get("/api/organizationmembers"),

    getById: (id: number): Promise<AxiosResponse<OrganizationMemberDto>> =>
        axios.get(`/api/organizationmembers/${id}`),

    create: (
        data: CreateMemberPayload,
        userId: number
    ): Promise<AxiosResponse<OrganizationMemberDto>> =>
        axios.post(`/api/organizationmembers?userId=${userId}`, data),

    update: (
        id: number,
        data: OrganizationMemberDto,
        userId: number
    ): Promise<AxiosResponse<void>> =>
        axios.put(`/api/organizationmembers/${id}?userId=${userId}`, data),

    delete: (id: number, userId: number): Promise<AxiosResponse<void>> =>
        axios.delete(`/api/organizationmembers/${id}?userId=${userId}`)
};
