export interface FeedbacksDto {
    feedbackId?: number;            
    eventId: number;                // Hangi etkinliğe ait olduğu
    userId: number;                 
    rating: number;                 // 1–5 arası bir puan
    comments: string;              
    submissionDate?: string;       

    // Sadece gösterim amaçlı, backend'den GET ile gelir
    eventName?: string;
    userName?: string;
}
