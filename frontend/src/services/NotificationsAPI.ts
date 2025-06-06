import axios from "axios";
import { NotificationDto } from "@/types/NotificationDto";

export const NotificationsAPI = {
    getByUserId: (userId: number) =>
        axios.get<NotificationDto[]>(`/api/Notifications?userId=${userId}`),

    markAsRead: (notificationId: number, userId: number) =>
        axios.put(`/api/Notifications/${notificationId}?userId=${userId}`, {
            notificationId: notificationId,
            isRead: true
        }),

    delete: (notificationId: number, userId: number) =>
        axios.delete(`/api/Notifications/${notificationId}?userId=${userId}`),
};