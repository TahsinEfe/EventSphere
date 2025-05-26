// src/services/NotificationsAPI.ts

import axios from "axios";
import { NotificationDto } from "@/types/NotificationDto";

export const NotificationsAPI = {
    getByUserId: (userId: number) =>
        axios.get<NotificationDto[]>(`/api/Notifications/user/${userId}`),
};
