export interface OrganizationDto {
    organizationId: number;
    name: string;
    contactEmail?: string;
    phone?: string;
    website?: string;
    socialMedia?: string;
    isActive: boolean;
    createdDate?: string;
}