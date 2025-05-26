import api from "./api";

export interface LoginRequest {
    username: string;
    passwordHash: string;
}

export interface LoginResponse {
    userId: number;
    username: string;
    email: string;
    firstName: string;
    lastName: string;
    roleId: number;
    role: string;
    isAdmin: boolean;
}

export const AuthAPI = {
    login: (data: LoginRequest) => api.post<LoginResponse>("/Auth/Login", data),

    getCurrentUser: (): LoginResponse | null => {
        const user = localStorage.getItem("user");
        return user ? JSON.parse(user) : null;
    },
};


