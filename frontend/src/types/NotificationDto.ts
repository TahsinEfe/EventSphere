export interface NotificationDto {
    notificationId: number;
    userId: number;
    title: string;
    message: string;
    isRead: boolean;
    createdDate: string;
}
