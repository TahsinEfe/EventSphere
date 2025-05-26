import { useEffect, useState } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Tabs, TabsList, TabsTrigger, TabsContent } from "@/components/ui/tabs";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Switch } from "@/components/ui/switch";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
    DialogFooter
} from "@/components/ui/dialog";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import {
    ArrowLeft,
    Plus,
    Edit,
    Trash2,
    Save,
    X,
    RefreshCw
} from "lucide-react";
import { OrganizationsAPI } from "@/services/OrganizationsAPI";
import { OrganizationMembersAPI } from "@/services/OrganizationMembersAPI";
import { UsersAPI } from "@/services/UsersAPI";
import { OrganizationDto } from "@/types/OrganizationDto";
import { OrganizationMembersDto } from "@/types/OrganizationMembersDto";
import { UsersDto } from "@/types/UsersDto";
import { useNavigate } from "react-router-dom";
import { useToast } from "@/components/ui/use-toast";

const OrganizationsDashboard = () => {    const [activeTab, setActiveTab] = useState<string>("organizations");
    const [organizations, setOrganizations] = useState<OrganizationDto[]>([]);
    const [members, setMembers] = useState<OrganizationMembersDto[]>([]);
    const [users, setUsers] = useState<UsersDto[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const navigate = useNavigate();
    const { toast } = useToast();

    // Dialog states
    const [orgDialogOpen, setOrgDialogOpen] = useState<boolean>(false);
    const [memberDialogOpen, setMemberDialogOpen] = useState<boolean>(false);
    const [editingOrg, setEditingOrg] = useState<OrganizationDto | null>(null);
    const [editingMember, setEditingMember] = useState<OrganizationMembersDto | null>(null);

    // Form states
    const [orgForm, setOrgForm] = useState({
        name: "",
        contactEmail: "",
        phone: "",
        website: "",
        socialMedia: "",
        isActive: true
    });

    const [memberForm, setMemberForm] = useState({
        organizationId: "",
        userId: "",
        isAdmin: false
    });    // Get current user ID from authentication
    const getCurrentUser = (): number => {
        try {
            const userData = localStorage.getItem('user');
            if (userData) {
                const user = JSON.parse(userData);
                return user.id || user.userId || 1; // fallback to 1 if no ID found
            }
        } catch (error) {
            console.warn('Error parsing user data from localStorage:', error);
        }
        return 1; // Default fallback
    };

    const currentUserId = getCurrentUser();

    useEffect(() => {
        fetchOrganizations();
        fetchMembers();
        fetchUsers();
    }, []);    const fetchOrganizations = async () => {
        try {
            setLoading(true);
            const res = await OrganizationsAPI.getAll();
            const data = Array.isArray(res.data) ? res.data : [];
            setOrganizations(data);
        } catch (err) {
            console.error("Organization fetch error", err);
            setOrganizations([]);
            toast({
                title: "Error",
                description: "Failed to fetch organizations",
                variant: "destructive",
            });
        } finally {
            setLoading(false);
        }
    };

    const fetchMembers = async () => {
        try {
            const res = await OrganizationMembersAPI.getAll();
            const data = Array.isArray(res.data) ? res.data : [];
            setMembers(data);
        } catch (err) {
            console.error("Member fetch error", err);
            setMembers([]);
            toast({
                title: "Error",
                description: "Failed to fetch members",
                variant: "destructive",
            });
        }
    };

    const fetchUsers = async () => {
        try {
            const res = await UsersAPI.getAll();
            const data = Array.isArray(res.data) ? res.data : [];
            setUsers(data);
        } catch (err) {
            console.error("Users fetch error", err);
            setUsers([]);
            toast({
                title: "Error",
                description: "Failed to fetch users",
                variant: "destructive",
            });
        }
    };    // Organization CRUD operations
    const handleCreateOrganization = async () => {
        try {
            setLoading(true);
            const res = await OrganizationsAPI.create(orgForm, currentUserId);
            setOrganizations(prev => [...prev, res.data]);
            setOrgDialogOpen(false);
            resetOrgForm();
            toast({
                title: "Success",
                description: "Organization created successfully",
            });
        } catch (err) {
            console.error("Create organization error", err);
            toast({
                title: "Error",
                description: "Failed to create organization",
                variant: "destructive",
            });
        } finally {
            setLoading(false);
        }
    };

    const handleUpdateOrganization = async () => {
        if (!editingOrg) return;
        try {
            setLoading(true);
            await OrganizationsAPI.update(editingOrg.organizationId, orgForm, currentUserId);
            setOrganizations(prev =>
                prev.map(org =>
                    org.organizationId === editingOrg.organizationId
                        ? { ...org, ...orgForm }
                        : org
                )
            );
            setOrgDialogOpen(false);
            resetOrgForm();
            toast({
                title: "Success",
                description: "Organization updated successfully",
            });
        } catch (err) {
            console.error("Update organization error", err);
            toast({
                title: "Error",
                description: "Failed to update organization",
                variant: "destructive",
            });
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteOrganization = async (org: OrganizationDto) => {
        if (!confirm(`"${org.name}" organizasyonunu silmek istediğinize emin misiniz?`)) return;

        try {
            setLoading(true);
            await OrganizationsAPI.delete(org.organizationId, currentUserId);
            setOrganizations(prev => prev.filter(o => o.organizationId !== org.organizationId));
            toast({
                title: "Success",
                description: "Organization deleted successfully",
            });
        } catch (err) {
            console.error("Delete organization error", err);
            toast({
                title: "Error",
                description: "Failed to delete organization",
                variant: "destructive",
            });
        } finally {
            setLoading(false);
        }
    };    // Member CRUD operations
    const handleCreateMember = async () => {
        try {
            setLoading(true);
            const res = await OrganizationMembersAPI.create({
                organizationId: parseInt(memberForm.organizationId),
                userId: parseInt(memberForm.userId),
                isAdmin: memberForm.isAdmin
            }, currentUserId);
            setMembers(prev => [...prev, res.data]);
            setMemberDialogOpen(false);
            resetMemberForm();
            toast({
                title: "Success",
                description: "Member added successfully",
            });
        } catch (err) {
            console.error("Create member error", err);
            toast({
                title: "Error",
                description: "Failed to add member",
                variant: "destructive",
            });
        } finally {
            setLoading(false);
        }
    };    const handleUpdateMember = async () => {
        if (!editingMember) return;
        try {
            setLoading(true);
            await OrganizationMembersAPI.update(editingMember.memberId!, {
                ...editingMember,
                isAdmin: memberForm.isAdmin
            }, currentUserId);
            setMembers(prev =>
                prev.map(member =>
                    member.memberId === editingMember.memberId
                        ? { ...member, isAdmin: memberForm.isAdmin }
                        : member
                )
            );
            setMemberDialogOpen(false);
            resetMemberForm();
            toast({
                title: "Success",
                description: "Member updated successfully",
            });
        } catch (err) {
            console.error("Update member error", err);
            toast({
                title: "Error",
                description: "Failed to update member",
                variant: "destructive",
            });
        } finally {
            setLoading(false);
        }
    };const handleDeleteMember = async (member: OrganizationMembersDto) => {
        if (!confirm("Bu üyeyi organizasyondan çıkarmak istediğinize emin misiniz?")) return;

        try {
            setLoading(true);
            await OrganizationMembersAPI.delete(member.memberId!, currentUserId);
            setMembers(prev => prev.filter(m => m.memberId !== member.memberId));
            toast({
                title: "Success",
                description: "Member removed successfully",
            });
        } catch (err) {
            console.error("Delete member error", err);
            toast({
                title: "Error",
                description: "Failed to remove member",
                variant: "destructive",
            });
        } finally {
            setLoading(false);
        }
    };

    // Refresh functions
    const refreshAll = async () => {
        await Promise.all([
            fetchOrganizations(),
            fetchMembers(),
            fetchUsers()
        ]);
    };

    const refreshOrganizations = () => fetchOrganizations();
    const refreshMembers = () => fetchMembers();

    // Form helpers
    const resetOrgForm = () => {
        setOrgForm({
            name: "",
            contactEmail: "",
            phone: "",
            website: "",
            socialMedia: "",
            isActive: true
        });
        setEditingOrg(null);
    };

    const resetMemberForm = () => {
        setMemberForm({
            organizationId: "",
            userId: "",
            isAdmin: false
        });
        setEditingMember(null);
    };    const openOrgDialog = (org: OrganizationDto | null = null) => {
        if (org) {
            setEditingOrg(org);
            setOrgForm({
                name: org.name || "",
                contactEmail: org.contactEmail || "",
                phone: org.phone || "",
                website: org.website || "",
                socialMedia: org.socialMedia || "",
                isActive: org.isActive ?? true
            });
        } else {
            resetOrgForm();
        }
        setOrgDialogOpen(true);
    };    const openMemberDialog = (member: OrganizationMembersDto | null = null) => {
        if (member) {
            setEditingMember(member);
            setMemberForm({
                organizationId: member.organizationId.toString(),
                userId: member.userId.toString(),
                isAdmin: member.isAdmin
            });
        } else {
            resetMemberForm();
        }
        setMemberDialogOpen(true);
    };

    return (
        <div className="container mx-auto px-4 py-6">
            <Tabs value={activeTab} onValueChange={setActiveTab}>
                {/* Header */}                <div className="flex items-center justify-between mb-6">
                    <Button variant="ghost" className="text-blue-600 hover:text-blue-800" onClick={() => navigate("/dashboard")}>
                        <ArrowLeft className="mr-2 h-4 w-4" /> Ana Panel'e Dön
                    </Button>

                    <TabsList className="grid w-auto grid-cols-3">
                        <TabsTrigger value="organizations">Organizasyonlar</TabsTrigger>
                        <TabsTrigger value="members">Üyeler</TabsTrigger>
                        <TabsTrigger value="contacts">İletişim</TabsTrigger>
                    </TabsList>

                    <div /> {/* spacer */}
                </div>

                {/* Organizations Tab */}
                <TabsContent value="organizations">
                    <Card className="shadow-lg border-0">                        <CardHeader className="bg-gradient-to-r from-blue-600 to-purple-600 text-white rounded-t-lg">
                            <div className="flex justify-between items-center">
                                <CardTitle className="text-xl font-bold">Organizasyonlar</CardTitle>
                                <div className="flex space-x-2">
                                    <Button
                                        onClick={refreshOrganizations}
                                        disabled={loading}
                                        size="sm"
                                        className="bg-white/20 text-white hover:bg-white/30"
                                    >
                                        <RefreshCw className="mr-2 h-4 w-4" />
                                        Yenile
                                    </Button>
                                    <Dialog open={orgDialogOpen} onOpenChange={setOrgDialogOpen}>
                                        <DialogTrigger asChild>
                                            <Button
                                                onClick={() => openOrgDialog()}
                                                className="bg-white text-blue-600 hover:bg-blue-50"
                                            >
                                                <Plus className="mr-2 h-4 w-4" />
                                                Yeni Organizasyon
                                            </Button>
                                        </DialogTrigger>
                                    <DialogContent className="max-w-md">
                                        <DialogHeader>
                                            <DialogTitle>
                                                {editingOrg ? "Organizasyon Düzenle" : "Yeni Organizasyon"}
                                            </DialogTitle>
                                        </DialogHeader>
                                        <div className="space-y-4">
                                            <div>
                                                <Label htmlFor="name">Organizasyon Adı</Label>
                                                <Input
                                                    id="name"
                                                    value={orgForm.name}
                                                    onChange={(e) => setOrgForm(prev => ({ ...prev, name: e.target.value }))}
                                                    placeholder="Organizasyon adını girin"
                                                />
                                            </div>
                                            <div>
                                                <Label htmlFor="contactEmail">İletişim E-postası</Label>
                                                <Input
                                                    id="contactEmail"
                                                    type="email"
                                                    value={orgForm.contactEmail}
                                                    onChange={(e) => setOrgForm(prev => ({ ...prev, contactEmail: e.target.value }))}
                                                    placeholder="ornek@email.com"
                                                />
                                            </div>
                                            <div>
                                                <Label htmlFor="phone">Telefon</Label>
                                                <Input
                                                    id="phone"
                                                    value={orgForm.phone}
                                                    onChange={(e) => setOrgForm(prev => ({ ...prev, phone: e.target.value }))}
                                                    placeholder="+90 xxx xxx xx xx"
                                                />
                                            </div>
                                            <div>
                                                <Label htmlFor="website">Website</Label>
                                                <Input
                                                    id="website"
                                                    value={orgForm.website}
                                                    onChange={(e) => setOrgForm(prev => ({ ...prev, website: e.target.value }))}
                                                    placeholder="https://example.com"
                                                />
                                            </div>
                                            <div>
                                                <Label htmlFor="socialMedia">Sosyal Medya</Label>
                                                <Input
                                                    id="socialMedia"
                                                    value={orgForm.socialMedia}
                                                    onChange={(e) => setOrgForm(prev => ({ ...prev, socialMedia: e.target.value }))}
                                                    placeholder="@organizasyon"
                                                />
                                            </div>
                                            <div className="flex items-center space-x-2">
                                                <Switch
                                                    id="isActive"
                                                    checked={orgForm.isActive}
                                                    onCheckedChange={(checked) => setOrgForm(prev => ({ ...prev, isActive: checked }))}
                                                />
                                                <Label htmlFor="isActive">Aktif</Label>
                                            </div>
                                        </div>
                                        <DialogFooter>
                                            <Button
                                                variant="outline"
                                                onClick={() => setOrgDialogOpen(false)}
                                            >
                                                <X className="mr-2 h-4 w-4" />
                                                İptal
                                            </Button>                                            <Button
                                                onClick={editingOrg ? handleUpdateOrganization : handleCreateOrganization}
                                                disabled={loading || !orgForm.name.trim()}
                                                className="bg-blue-600 hover:bg-blue-700"
                                            >
                                                <Save className="mr-2 h-4 w-4" />
                                                {loading ? "Kaydediliyor..." : "Kaydet"}
                                            </Button>
                                        </DialogFooter>
                                    </DialogContent>
                                </Dialog>
                            </div>
                        </div>
                        </CardHeader>
                        <CardContent className="p-6">
                            <div className="overflow-x-auto">
                                <table className="min-w-full">
                                    <thead>
                                        <tr className="border-b-2 border-gray-200">
                                            <th className="text-left p-3 font-semibold text-gray-700">ID</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Ad</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">E-posta</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Telefon</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Website</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Durum</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">İşlemler</th>
                                        </tr>
                                    </thead>
                                    <tbody>                                        {organizations.length === 0 ? (
                                            <tr>
                                                <td colSpan={7} className="text-center p-8 text-gray-500">
                                                    {loading ? "Yükleniyor..." : "Henüz organizasyon bulunmuyor"}
                                                </td>
                                            </tr>
                                        ) : (
                                            organizations.map((org) => (
                                                <tr key={org.organizationId} className="border-b hover:bg-gray-50 transition-colors">
                                                    <td className="p-3">{org.organizationId}</td>
                                                    <td className="p-3 font-medium">{org.name}</td>
                                                    <td className="p-3">{org.contactEmail || "-"}</td>
                                                    <td className="p-3">{org.phone || "-"}</td>
                                                    <td className="p-3">
                                                        {org.website ? (
                                                            <a href={org.website} target="_blank" rel="noopener noreferrer"
                                                                className="text-blue-600 hover:underline">
                                                                {org.website}
                                                            </a>
                                                        ) : "-"}
                                                    </td>
                                                    <td className="p-3">
                                                        <span className={`px-2 py-1 rounded-full text-xs ${org.isActive
                                                                ? "bg-green-100 text-green-800"
                                                                : "bg-red-100 text-red-800"
                                                            }`}>
                                                            {org.isActive ? "Aktif" : "Pasif"}
                                                        </span>
                                                    </td>
                                                    <td className="p-3">
                                                        <div className="flex space-x-2">
                                                            <Button
                                                                size="sm"
                                                                variant="outline"
                                                                onClick={() => openOrgDialog(org)}
                                                                className="text-blue-600 hover:text-blue-800"
                                                            >
                                                                <Edit className="h-4 w-4" />
                                                            </Button>
                                                            <Button
                                                                size="sm"
                                                                variant="outline"
                                                                onClick={() => handleDeleteOrganization(org)}
                                                                className="text-red-600 hover:text-red-800"
                                                            >
                                                                <Trash2 className="h-4 w-4" />
                                                            </Button>
                                                        </div>
                                                    </td>
                                                </tr>
                                            ))
                                        )}
                                    </tbody>
                                </table>
                            </div>
                        </CardContent>
                    </Card>
                </TabsContent>

                {/* Members Tab */}
                <TabsContent value="members">
                    <Card className="shadow-lg border-0">
                        <CardHeader className="bg-gradient-to-r from-green-600 to-blue-600 text-white rounded-t-lg">
                            <div className="flex justify-between items-center">
                                <CardTitle className="text-xl font-bold">Organizasyon Üyeleri</CardTitle>
                                <Dialog open={memberDialogOpen} onOpenChange={setMemberDialogOpen}>
                                    <DialogTrigger asChild>
                                        <Button
                                            onClick={() => openMemberDialog()}
                                            className="bg-white text-green-600 hover:bg-green-50"
                                        >
                                            <Plus className="mr-2 h-4 w-4" />
                                            Yeni Üye
                                        </Button>
                                    </DialogTrigger>
                                    <DialogContent className="max-w-md">
                                        <DialogHeader>
                                            <DialogTitle>
                                                {editingMember ? "Üye Düzenle" : "Yeni Üye Ekle"}
                                            </DialogTitle>
                                        </DialogHeader>
                                        <div className="space-y-4">
                                            <div>
                                                <Label htmlFor="organizationId">Organizasyon</Label>                                                <Select
                                                    value={memberForm.organizationId}
                                                    onValueChange={(value) => setMemberForm(prev => ({ ...prev, organizationId: value }))}
                                                    disabled={!!editingMember}
                                                >
                                                    <SelectTrigger>
                                                        <SelectValue placeholder="Organizasyon seçin" />
                                                    </SelectTrigger>
                                                    <SelectContent>
                                                        {organizations.map((org) => (
                                                            <SelectItem key={org.organizationId} value={org.organizationId.toString()}>
                                                                {org.name}
                                                            </SelectItem>
                                                        ))}
                                                    </SelectContent>
                                                </Select>
                                            </div>
                                            <div>
                                                <Label htmlFor="userId">Kullanıcı</Label>                                                <Select
                                                    value={memberForm.userId}
                                                    onValueChange={(value) => setMemberForm(prev => ({ ...prev, userId: value }))}
                                                    disabled={!!editingMember}
                                                >
                                                    <SelectTrigger>
                                                        <SelectValue placeholder="Kullanıcı seçin" />
                                                    </SelectTrigger>
                                                    <SelectContent>
                                                        {users.map((user) => (
                                                            <SelectItem key={user.userId} value={user.userId.toString()}>
                                                                {user.firstName} {user.lastName}
                                                            </SelectItem>
                                                        ))}
                                                    </SelectContent>
                                                </Select>
                                            </div>
                                            <div className="flex items-center space-x-2">
                                                <Switch
                                                    id="isAdmin"
                                                    checked={memberForm.isAdmin}
                                                    onCheckedChange={(checked) => setMemberForm(prev => ({ ...prev, isAdmin: checked }))}
                                                />
                                                <Label htmlFor="isAdmin">Admin Yetkisi</Label>
                                            </div>
                                        </div>
                                        <DialogFooter>
                                            <Button
                                                variant="outline"
                                                onClick={() => setMemberDialogOpen(false)}
                                            >
                                                <X className="mr-2 h-4 w-4" />
                                                İptal
                                            </Button>
                                            <Button
                                                onClick={editingMember ? handleUpdateMember : handleCreateMember}
                                                disabled={loading || !memberForm.organizationId || !memberForm.userId}
                                                className="bg-green-600 hover:bg-green-700"
                                            >
                                                <Save className="mr-2 h-4 w-4" />
                                                {loading ? "Kaydediliyor..." : "Kaydet"}
                                            </Button>
                                        </DialogFooter>
                                    </DialogContent>
                                </Dialog>
                            </div>
                        </CardHeader>
                        <CardContent className="p-6">
                            <div className="overflow-x-auto">
                                <table className="min-w-full">
                                    <thead>
                                        <tr className="border-b-2 border-gray-200">
                                            <th className="text-left p-3 font-semibold text-gray-700">Üye ID</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Organizasyon</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Kullanıcı</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Katılım Tarihi</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Admin</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">İşlemler</th>
                                        </tr>
                                    </thead>
                                    <tbody>                                        {members.length === 0 ? (
                                            <tr>
                                                <td colSpan={6} className="text-center p-8 text-gray-500">
                                                    {loading ? "Yükleniyor..." : "Henüz üye bulunmuyor"}
                                                </td>
                                            </tr>
                                        ) : (
                                            members.map((member) => (
                                                <tr key={member.memberId} className="border-b hover:bg-gray-50 transition-colors">
                                                    <td className="p-3">{member.memberId}</td>
                                                    <td className="p-3">{member.organizationName || `Org #${member.organizationId}`}</td>
                                                    <td className="p-3">{member.userName || `User #${member.userId}`}</td>
                                                    <td className="p-3">
                                                        {member.joinDate 
                                                            ? new Date(member.joinDate).toLocaleDateString('tr-TR')
                                                            : '-'
                                                        }
                                                    </td>
                                                    <td className="p-3">
                                                        <span className={`px-2 py-1 rounded-full text-xs ${member.isAdmin
                                                                ? "bg-purple-100 text-purple-800"
                                                                : "bg-gray-100 text-gray-800"
                                                            }`}>
                                                            {member.isAdmin ? "Admin" : "Üye"}
                                                        </span>
                                                    </td>
                                                    <td className="p-3">
                                                        <div className="flex space-x-2">
                                                            <Button
                                                                size="sm"
                                                                variant="outline"
                                                                onClick={() => openMemberDialog(member)}
                                                                className="text-blue-600 hover:text-blue-800"
                                                            >
                                                                <Edit className="h-4 w-4" />
                                                            </Button>
                                                            <Button
                                                                size="sm"
                                                                variant="outline"
                                                                onClick={() => handleDeleteMember(member)}
                                                                className="text-red-600 hover:text-red-800"
                                                            >
                                                                <Trash2 className="h-4 w-4" />
                                                            </Button>
                                                        </div>
                                                    </td>
                                                </tr>
                                            ))
                                        )}
                                    </tbody>
                                </table>
                            </div>
                        </CardContent>
                    </Card>
                </TabsContent>

                {/* Contacts Tab */}
                <TabsContent value="contacts">
                    <Card className="shadow-lg border-0">
                        <CardHeader className="bg-gradient-to-r from-purple-600 to-pink-600 text-white rounded-t-lg">
                            <CardTitle className="text-xl font-bold">Organizatör İletişim Bilgileri</CardTitle>
                        </CardHeader>
                        <CardContent className="p-6">
                            <div className="text-center py-12">
                                <div className="text-6xl mb-4">🚧</div>
                                <h3 className="text-lg font-semibold text-gray-700 mb-2">
                                    Bu özellik henüz geliştiriliyor
                                </h3>
                                <p className="text-gray-500">
                                    Organizatör iletişim bilgileri ve CRUD işlemleri yakında eklenecek.
                                </p>
                            </div>
                        </CardContent>
                    </Card>
                </TabsContent>
            </Tabs>
        </div>
    );
};

export default OrganizationsDashboard;