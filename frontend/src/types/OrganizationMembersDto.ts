export interface OrganizationMembersDto {
    memberId?: number;              
    organizationId: number;        // Bağlı olduğu organizasyon
    userId: number;                
    joinDate?: string;            
    isAdmin: boolean;             // Kullanıcı bu organizasyonda admin mi

    organizationName?: string;    // GET işlemlerinde görüntüleme amaçlı
    userName?: string;            // GET işlemlerinde görüntüleme amaçlı
}
