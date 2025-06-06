export interface OrganizationMembersDto {
    memberId?: number;
    organizationId: number;
    userId: number;
    joinDate?: string;
    isAdmin: boolean;

    organizationName?: string;
    userName?: string;
}
