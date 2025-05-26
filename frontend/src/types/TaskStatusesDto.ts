export interface TaskStatusesDto {
    taskStatusId?: number;         // Otomatik atanır, update için gereklidir
    statusName: string;            // Görev durum adı, örneğin: "Beklemede", "Tamamlandı"
}
