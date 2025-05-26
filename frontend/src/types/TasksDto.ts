export interface TasksDto {
    taskId?: number; // Yeni kayıt oluşturulurken backend tarafından atanır
    eventId: number;
    title: string;
    assignedUserId: number;
    dueDate: string; // ISO formatlı tarih: "2025-06-01T15:00:00"
    taskStatusId: number;

    // Opsiyonel olarak aşağıdaki alanlar sadece GET işlemlerinde doldurulur
    eventName?: string;
    assignedUserName?: string;
    taskStatusName?: string;
}
