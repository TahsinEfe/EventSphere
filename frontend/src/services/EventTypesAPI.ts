// src/services/EventTypesAPI.ts
import api from "./api";
import { AxiosResponse } from "axios";

export const EventTypesAPI = {
    getAll: (): Promise<AxiosResponse<{ id: number; name: string }[]>> => api.get("/EventTypes")
};
