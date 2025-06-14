export interface UsersDto {
    userId: number;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    isActive: boolean;
    roleId: number;
    /** Sadece GET için. PUT/POST'da kullanma. */
    roleName?: string;
    /** Sadece frontend logic. Backend'e göndermeyin. */
    isAdmin?: boolean;
}
