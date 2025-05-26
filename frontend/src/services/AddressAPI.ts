import axios from "axios";
import { AddressesDto } from "../types/AddressesDto";

const API_BASE = "http://localhost:5065/api/Address";

export const AddressAPI = {
    getAll: () => axios.get<AddressesDto[]>(API_BASE),
    getById: (id: number) => axios.get<AddressesDto>(`${API_BASE}/${id}`),
    create: (data: AddressesDto) => axios.post(API_BASE, data),
    update: (id: number, data: AddressesDto) => axios.put(`${API_BASE}/${id}`, data),
    delete: (id: number) => axios.delete(`${API_BASE}/${id}`)
};
