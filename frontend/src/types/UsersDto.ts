export interface UsersDto {
    userId: number;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    isActive: boolean;
    roleId: number;
    roleName?: string; // sadece GET işlemleri için
    isAdmin?: boolean; // sadece GET işlemleri için
}
