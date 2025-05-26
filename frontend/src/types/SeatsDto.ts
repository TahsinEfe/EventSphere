export interface SeatsDto {
    seatId?: number;          
    eventId: number;          
    section: string;          // Örnek: Balkon, VIP, Zemin Kat
    rowNumber: string;        // Örnek: A, B, C
    seatNumber: string;       // Örnek: 1, 2, 3
    isReserved: boolean;      

    // Gösterim için isteğe bağlı alanlar (GET için)
    eventName?: string;
}
