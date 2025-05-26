import { UsersDto } from "@/types/UsersDto";
import { RegisterRequest } from "@/types/RegisterRequest";
import { LoginRequest } from "@/types/LoginRequest";
import api from "./api";
import { AxiosResponse } from "axios";

export const UsersAPI = {
    getAll: (): Promise<AxiosResponse<UsersDto[]>> => api.get("/Users"),
    get: (id: number): Promise<AxiosResponse<UsersDto>> => api.get(`/Users/${id}`),

    register: (data: RegisterRequest): Promise<AxiosResponse<UsersDto>> =>
        api.post("/Users", data),

    login: (credentials: LoginRequest): Promise<AxiosResponse<UsersDto>> =>
        api.post("/Auth/login", credentials),

    update: (id: number, data: UsersDto): Promise<AxiosResponse<void>> =>
        api.put(`/Users/${id}`, data),

    delete: (id: number): Promise<AxiosResponse<void>> => api.delete(`/Users/${id}`),
};
