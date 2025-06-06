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
    ArrowLeft,
    Plus,
    Edit,
    Trash2,
    Save,
    X,
    RefreshCw
} from "lucide-react";
import { OrganizationsAPI } from "@/services/OrganizationsAPI";
import { OrganizationDto } from "@/types/OrganizationDto";
import { OrganizationMembersAPI } from "@/services/OrganizationMembersAPI";
import { OrganizationMembersDto } from "@/types/OrganizationMembersDto";
import { useNavigate } from "react-router-dom";
import { useToast } from "@/components/ui/use-toast";
import { UsersDto } from "../types/UsersDto";
import { UsersAPI } from "../services/UsersAPI";


const OrganizationsDashboard = () => {
    const [organizations, setOrganizations] = useState<OrganizationDto[]>([]);
    const [members, setMembers] = useState<OrganizationMembersDto[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [activeTab, setActiveTab] = useState("organizations");
    const navigate = useNavigate();
    const { toast } = useToast();
    const [allUsers, setAllUsers] = useState<UsersDto[]>([]);
    const [allOrganizations, setAllOrganizations] = useState<OrganizationDto[]>([]);


    const [orgDialogOpen, setOrgDialogOpen] = useState<boolean>(false);
    const [editingOrg, setEditingOrg] = useState<OrganizationDto | null>(null);

    const [orgForm, setOrgForm] = useState({
        name: "",
        contactEmail: "",
        phone: "",
        website: "",
        socialMedia: "",
        isActive: true
    });

    const [memberDialogOpen, setMemberDialogOpen] = useState(false);
    const [editingMember, setEditingMember] = useState<OrganizationMembersDto | null>(null);
    const [memberForm, setMemberForm] = useState<{
        organizationId: number;
        userId: number;
        isAdmin: boolean;
    }>({ organizationId: 0, userId: 0, isAdmin: false });


    const openMemberDialog = (member: OrganizationMembersDto | null = null) => {
        if (member) {
            setEditingMember(member);
            setMemberForm({
                organizationId: member.organizationId,
                userId: member.userId,
                isAdmin: member.isAdmin,
            });
        } else {
            setEditingMember(null);
            setMemberForm({ organizationId: 0, userId: 0, isAdmin: false });
        }
        setMemberDialogOpen(true);
    };

    const handleCreateMember = async () => {
        try {
            setLoading(true);
            const res = await OrganizationMembersAPI.create(memberForm, currentUserId); // ✔️ DÜZELTİLDİ
            setMembers((prev) => [...prev, res.data]);
            setMemberDialogOpen(false);
            toast({ title: "Success", description: "Member created successfully" });
        } catch (err) {
            console.error("Create member error", err);
            toast({ title: "Error", description: "Failed to create member", variant: "destructive" });
        } finally {
            setLoading(false);
        }
    };


    const handleUpdateMember = async () => {
        if (!editingMember) return;
        try {
            setLoading(true);
            await OrganizationMembersAPI.update(
                editingMember.memberId!, 
                { isAdmin: memberForm.isAdmin },
                currentUserId 
            );
            setMembers((prev) =>
                prev.map((m) =>
                    m.memberId === editingMember.memberId ? { ...m, isAdmin: memberForm.isAdmin } : m
                )
            );
            setMemberDialogOpen(false);
            toast({ title: "Success", description: "Member updated successfully" });
        } catch (err) {
            console.error("Update member error", err);
            toast({ title: "Error", description: "Failed to update member", variant: "destructive" });
        } finally {
            setLoading(false);
        }
    };


    const handleDeleteMember = async (member: OrganizationMembersDto) => {
        if (!confirm(`Are you sure you want to delete member #${member.memberId}?`)) return;
        try {
            setLoading(true);
            await OrganizationMembersAPI.delete(member.memberId!, currentUserId);
            setMembers((prev) => prev.filter((m) => m.memberId !== member.memberId));
            toast({ title: "Success", description: "Member deleted successfully" });
        } catch (err) {
            console.error("Delete member error", err);
            toast({ title: "Error", description: "Failed to delete member", variant: "destructive" });
        } finally {
            setLoading(false);
        }
    };


    const getCurrentUser = (): number => {
        try {
            const userData = localStorage.getItem('user');
            if (userData) {
                const user = JSON.parse(userData);
                return user.id || user.userId || 1;
            }
        } catch (error) {
            console.warn('Error parsing user data from localStorage:', error);
        }
        return 1;
    };

    const currentUserId = getCurrentUser();

    useEffect(() => {
        fetchOrganizations();
        fetchUserAndOrgList(); 
    }, []);

useEffect(() => {
    if (activeTab === "members") {
        fetchMembers();
    }
}, [activeTab]);


    const fetchUserAndOrgList = async () => {
        try {
            const [userRes, orgRes] = await Promise.all([
                UsersAPI.getAll(),
                OrganizationsAPI.getAll()
            ]);
            setAllUsers(Array.isArray(userRes.data) ? userRes.data : []);
            setAllOrganizations(Array.isArray(orgRes.data) ? orgRes.data : []);
        } catch (err) {
            console.error("List load error", err);
            toast({ title: "Error", description: "User/Organization list couldn't be loaded", variant: "destructive" });
        }
    };

    const fetchOrganizations = async () => {
        try {
            setLoading(true);
            const res = await OrganizationsAPI.getAll();
            const data = Array.isArray(res.data) ? res.data : [];
            setOrganizations(data);
        } catch (err) {
            console.error("Organization fetch error", err);
            setOrganizations([]);
            toast({ title: "Error", description: "Failed to fetch organizations", variant: "destructive" });
        } finally {
            setLoading(false);
        }
    };

    const fetchMembers = async () => {
        try {
            setLoading(true);
            const res = await OrganizationMembersAPI.getAll();
            const data = Array.isArray(res.data) ? res.data : [];
            console.log("FETCHED MEMBERS", data);
            setMembers(data);
        } catch (err) {
            console.error("Member fetch error", err);
            setMembers([]);
            toast({ title: "Error", description: "Failed to fetch organization members", variant: "destructive" });
        } finally {
            setLoading(false);
        }
    };

    const handleCreateOrganization = async () => {
        try {
            setLoading(true);
            const res = await OrganizationsAPI.create({ name: orgForm.name, contactEmail: orgForm.contactEmail, isActive: orgForm.isActive }, currentUserId);
            setOrganizations(prev => [...prev, res.data]);
            setOrgDialogOpen(false);
            resetOrgForm();
            toast({ title: "Success", description: "Organization created successfully" });
        } catch (err) {
            console.error("Create organization error", err);
            toast({ title: "Error", description: "Failed to create organization", variant: "destructive" });
        } finally {
            setLoading(false);
        }
    };

    const handleUpdateOrganization = async () => {
        if (!editingOrg) return;
        try {
            setLoading(true);
            const payload = { name: orgForm.name, contactEmail: orgForm.contactEmail, isActive: orgForm.isActive };
            await OrganizationsAPI.update(editingOrg.organizationId, payload, currentUserId);
            setOrganizations(prev => prev.map(org => org.organizationId === editingOrg.organizationId ? { ...org, ...payload } : org));
            setOrgDialogOpen(false);
            resetOrgForm();
            toast({ title: "Success", description: "Organization updated successfully" });
        } catch (err) {
            console.error("Update organization error", err);
            toast({ title: "Error", description: "Failed to update organization", variant: "destructive" });
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteOrganization = async (org: OrganizationDto) => {
        if (!confirm(`Are you sure you want to delete "${org.name}"?`)) return;
        try {
            setLoading(true);
            await OrganizationsAPI.delete(org.organizationId, currentUserId);
            setOrganizations(prev => prev.filter(o => o.organizationId !== org.organizationId));
            toast({ title: "Success", description: "Organization deleted successfully" });
        } catch (err) {
            console.error("Delete organization error", err);
            toast({ title: "Error", description: "Failed to delete organization", variant: "destructive" });
        } finally {
            setLoading(false);
        }
    };

    const resetOrgForm = () => {
        setOrgForm({ name: "", contactEmail: "", phone: "", website: "", socialMedia: "", isActive: true });
        setEditingOrg(null);
    };

    const openOrgDialog = (org: OrganizationDto | null = null) => {
        if (org) {
            setEditingOrg(org);
            setOrgForm({ name: org.name || "", contactEmail: org.contactEmail || "", phone: org.phone || "", website: org.website || "", socialMedia: org.socialMedia || "", isActive: org.isActive ?? true });
        } else {
            resetOrgForm();
        }
        setOrgDialogOpen(true);
    };

    return (
        <div className="container mx-auto px-4 py-6">
            <Tabs value={activeTab} onValueChange={setActiveTab}>
                <div className="flex items-center justify-between mb-6">
                    <Button variant="ghost" className="text-blue-600 hover:text-blue-800" onClick={() => navigate("/dashboard")}>
                        <ArrowLeft className="mr-2 h-4 w-4" /> Back to Dashboard
                    </Button>
                    <TabsList className="grid w-auto grid-cols-2 gap-2">
                        <TabsTrigger value="organizations">Organizations</TabsTrigger>
                        <TabsTrigger value="members">Organization Members</TabsTrigger>
                    </TabsList>
                    <div />
                </div>

                <TabsContent value="organizations">
                    <Card className="shadow-lg border-0">
                        <CardHeader className="bg-gradient-to-r from-blue-600 to-purple-600 text-white rounded-t-lg">
                            <div className="flex justify-between items-center">
                                <CardTitle className="text-xl font-bold">Organizations</CardTitle>
                                <div className="flex space-x-2">
                                    <Button onClick={fetchOrganizations} disabled={loading} size="sm" className="bg-white/20 text-white hover:bg-white/30">
                                        <RefreshCw className="mr-2 h-4 w-4" /> Refresh
                                    </Button>
                                    <Dialog open={orgDialogOpen} onOpenChange={setOrgDialogOpen}>
                                        <DialogTrigger asChild>
                                            <Button onClick={() => openOrgDialog()} className="bg-white text-blue-600 hover:bg-blue-50">
                                                <Plus className="mr-2 h-4 w-4" /> New Organization
                                            </Button>
                                        </DialogTrigger>
                                        <DialogContent className="max-w-md">
                                            <DialogHeader>
                                                <DialogTitle>{editingOrg ? "Edit Organization" : "New Organization"}</DialogTitle>
                                            </DialogHeader>
                                            <div className="space-y-4">
                                                <Label htmlFor="name">Name</Label>
                                                <Input id="name" value={orgForm.name} onChange={(e) => setOrgForm(prev => ({ ...prev, name: e.target.value }))} placeholder="Organization name" />

                                                <Label htmlFor="contactEmail">Email</Label>
                                                <Input id="contactEmail" type="email" value={orgForm.contactEmail} onChange={(e) => setOrgForm(prev => ({ ...prev, contactEmail: e.target.value }))} placeholder="email@example.com" />

                                                <Label htmlFor="phone">Phone</Label>
                                                <Input id="phone" value={orgForm.phone} onChange={(e) => setOrgForm(prev => ({ ...prev, phone: e.target.value }))} placeholder="+90 xxx xxx xx xx" />

                                                <Label htmlFor="website">Website</Label>
                                                <Input id="website" value={orgForm.website} onChange={(e) => setOrgForm(prev => ({ ...prev, website: e.target.value }))} placeholder="https://example.com" />

                                                <Label htmlFor="socialMedia">Social Media</Label>
                                                <Input id="socialMedia" value={orgForm.socialMedia} onChange={(e) => setOrgForm(prev => ({ ...prev, socialMedia: e.target.value }))} placeholder="@organization" />

                                                <div className="flex items-center space-x-2">
                                                    <Switch id="isActive" checked={orgForm.isActive} onCheckedChange={(checked) => setOrgForm(prev => ({ ...prev, isActive: checked }))} />
                                                    <Label htmlFor="isActive">Active</Label>
                                                </div>
                                            </div>
                                            <DialogFooter>
                                                <Button variant="outline" onClick={() => setOrgDialogOpen(false)}>
                                                    <X className="mr-2 h-4 w-4" /> Cancel
                                                </Button>
                                                <Button onClick={editingOrg ? handleUpdateOrganization : handleCreateOrganization} disabled={loading || !orgForm.name.trim()} className="bg-blue-600 hover:bg-blue-700">
                                                    <Save className="mr-2 h-4 w-4" /> {loading ? "Saving..." : "Save"}
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
                                            <th className="text-left p-3 font-semibold text-gray-700">Name</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Email</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Status</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {organizations.length === 0 ? (
                                            <tr>
                                                <td colSpan={5} className="text-center p-8 text-gray-500">
                                                    {loading ? "Loading..." : "No organizations found"}
                                                </td>
                                            </tr>
                                        ) : (
                                            organizations.map((org) => (
                                                <tr key={org.organizationId} className="border-b hover:bg-gray-50 transition-colors">
                                                    <td className="p-3">{org.organizationId}</td>
                                                    <td className="p-3 font-medium">{org.name}</td>
                                                    <td className="p-3">{org.contactEmail || "-"}</td>
                                                    <td className="p-3">
                                                        <span className={`px-2 py-1 rounded-full text-xs ${org.isActive ? "bg-green-100 text-green-800" : "bg-red-100 text-red-800"}`}>
                                                            {org.isActive ? "Active" : "Inactive"}
                                                        </span>
                                                    </td>
                                                    <td className="p-3">
                                                        <div className="flex space-x-2">
                                                            <Button size="sm" variant="outline" onClick={() => openOrgDialog(org)} className="text-blue-600 hover:text-blue-800">
                                                                <Edit className="h-4 w-4" />
                                                            </Button>
                                                            <Button size="sm" variant="outline" onClick={() => handleDeleteOrganization(org)} className="text-red-600 hover:text-red-800">
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

                <TabsContent value="members">
                    <Card className="shadow-lg border-0">
                        <CardHeader className="bg-gradient-to-r from-green-600 to-teal-600 text-white rounded-t-lg">
                            <div className="flex justify-between items-center">
                                <CardTitle className="text-xl font-bold">Organization Members</CardTitle>
                                <div className="flex space-x-2">
                                    <Button onClick={fetchMembers} disabled={loading} size="sm" className="bg-white/20 text-white hover:bg-white/30">
                                        <RefreshCw className="mr-2 h-4 w-4" /> Refresh
                                    </Button>
                                    <Dialog open={memberDialogOpen} onOpenChange={setMemberDialogOpen}>
                                        <DialogTrigger asChild>
                                            <Button onClick={() => openMemberDialog()} className="bg-white text-green-700 hover:bg-green-50">
                                                <Plus className="mr-2 h-4 w-4" /> New Member
                                            </Button>
                                        </DialogTrigger>
                                        <DialogContent className="max-w-md">
                                            <DialogHeader>
                                                <DialogTitle>{editingMember ? "Edit Member" : "New Member"}</DialogTitle>
                                            </DialogHeader>
                                            <div className="space-y-4">
                                                <Label htmlFor="organizationId">Organization</Label>
                                                <select
                                                    id="organizationId"
                                                    value={memberForm.organizationId}
                                                    onChange={(e) =>
                                                        setMemberForm((prev) => ({
                                                            ...prev,
                                                            organizationId: parseInt(e.target.value),
                                                        }))
                                                    }
                                                    className="w-full border rounded px-3 py-2 text-sm"
                                                >
                                                    <option value="">Select organization</option>
                                                    {allOrganizations.map((org) => (
                                                        <option key={org.organizationId} value={org.organizationId}>
                                                            {org.name}
                                                        </option>
                                                    ))}
                                                </select>

                                                <Label htmlFor="userId">User</Label>
                                                <select
                                                    id="userId"
                                                    value={memberForm.userId}
                                                    onChange={(e) =>
                                                        setMemberForm((prev) => ({
                                                            ...prev,
                                                            userId: parseInt(e.target.value),
                                                        }))
                                                    }
                                                    className="w-full border rounded px-3 py-2 text-sm"
                                                >
                                                    <option value="">Select user</option>
                                                    {allUsers.map((user) => (
                                                        <option key={user.userId} value={user.userId}>
                                                            {user.firstName} {user.lastName}
                                                        </option>
                                                    ))}
                                                </select>

                                                <div className="flex items-center space-x-2">
                                                    <Switch
                                                        id="isAdmin"
                                                        checked={memberForm.isAdmin}
                                                        onCheckedChange={(checked) =>
                                                            setMemberForm((prev) => ({ ...prev, isAdmin: checked }))
                                                        }
                                                    />
                                                    <Label htmlFor="isAdmin">Is Admin</Label>
                                                </div>
                                            </div>
                                            <DialogFooter>
                                                <Button variant="outline" onClick={() => setMemberDialogOpen(false)}>
                                                    <X className="mr-2 h-4 w-4" /> Cancel
                                                </Button>
                                                <Button
                                                    onClick={editingMember ? handleUpdateMember : handleCreateMember}
                                                    disabled={loading}
                                                    className="bg-green-600 hover:bg-green-700"
                                                >
                                                    <Save className="mr-2 h-4 w-4" />
                                                    {loading ? "Saving..." : "Save"}
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
                                            <th className="text-left p-3 font-semibold text-gray-700">Organization</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">User</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Join Date</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Is Admin</th>
                                            <th className="text-left p-3 font-semibold text-gray-700">Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {members.length === 0 ? (
                                            <tr>
                                                <td colSpan={6} className="text-center p-8 text-gray-500">
                                                    {loading ? "Loading..." : "No organization members found"}
                                                </td>
                                            </tr>
                                        ) : (
                                            members.map((member) => (
                                                <tr key={member.memberId} className="border-b hover:bg-gray-50 transition-colors">
                                                    <td className="p-3">{member.memberId}</td>
                                                    <td className="p-3">{member.organizationName}</td>
                                                    <td className="p-3">{member.userName}</td>
                                                    <td className="p-3">{new Date(member.joinDate).toLocaleDateString()}</td>
                                                    <td className="p-3">
                                                        <span className={`px-2 py-1 rounded-full text-xs ${member.isAdmin ? "bg-green-100 text-green-800" : "bg-gray-100 text-gray-600"}`}>
                                                            {member.isAdmin ? "Admin" : "Member"}
                                                        </span>
                                                    </td>
                                                    <td className="p-3">
                                                        <div className="flex space-x-2">
                                                            <Button size="sm" variant="outline" onClick={() => openMemberDialog(member)} className="text-green-700 hover:text-green-800">
                                                                <Edit className="h-4 w-4" />
                                                            </Button>
                                                            <Button size="sm" variant="outline" onClick={() => handleDeleteMember(member)} className="text-red-600 hover:text-red-800">
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
            </Tabs>
        </div>
    );
};

export default OrganizationsDashboard;


