import { useEffect, useState } from "react";
import { UsersAPI } from "@/services/UsersAPI";
import { UsersDto } from "@/types/UsersDto";
import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Pencil, Trash2, ArrowLeft, UserPlus } from "lucide-react";
import { useToast } from "@/components/ui/use-toast";
import { useNavigate } from "react-router-dom";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

const UserDashboard = () => {
    const [users, setUsers] = useState<UsersDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [isEditOpen, setIsEditOpen] = useState(false);
    const [isAddOpen, setIsAddOpen] = useState(false);
    const [selectedUser, setSelectedUser] = useState<UsersDto | null>(null);
    const { toast } = useToast();
    const navigate = useNavigate();

    const [formData, setFormData] = useState<Partial<UsersDto>>({});

    const fetchUsers = async () => {
        try {
            const response = await UsersAPI.getAll();
            setUsers(response.data);
        } catch (error) {
            toast({ title: "Error", description: "Users couldn't Load." });
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async (id: number) => {
        if (!window.confirm("Are you sure to delete this user?")) return;

        try {
            await UsersAPI.delete(id);
            toast({ title: "Success", description: "User Deleted." });
            setUsers((prev) => prev.filter((u) => u.userId !== id));
        } catch {
            toast({ title: "Error", description: "Users couldn't Delete." });
        }
    };

    const openEditModal = (user: UsersDto) => {
        setSelectedUser(user);
        setFormData(user);
        setIsEditOpen(true);
    };

    const openAddModal = () => {
        setFormData({});
        setIsAddOpen(true);
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData((prev) => ({
            ...prev,
            [e.target.name]: e.target.value,
        }));
    };

    const handleEditSubmit = async () => {
        if (!selectedUser?.userId) return;
        try {
            await UsersAPI.update(selectedUser.userId, formData as UsersDto);
            toast({ title: "Success", description: "User Updated." });
            setIsEditOpen(false);
            fetchUsers();
        } catch {
            toast({ title: "Error", description: "Users couldn't Updated." });
        }
    };

    const handleAddSubmit = async () => {
        try {
            await UsersAPI.register({
                username: formData.username!,
                passwordHash: "123456", // temporary
                email: formData.email!,
                firstName: formData.firstName,
                lastName: formData.lastName,
            });
            toast({ title: "User Added." });
            setIsAddOpen(false);
            fetchUsers();
        } catch {
            toast({ title: "Error", description: "Register Failure." });
        }
    };

    useEffect(() => {
        fetchUsers();
    }, []);

    if (loading) return <p>Users Loading...</p>;

    return (
        <div className="p-6 space-y-4">
            <div className="flex justify-between">
                <Button variant="ghost" onClick={() => navigate("/dashboard")}>
                    <ArrowLeft className="w-4 h-4 mr-2" />
                    Back to Dashboard
                </Button>
                <Button onClick={openAddModal}>
                    <UserPlus className="w-4 h-4 mr-2" />
                    Add New User
                </Button>
            </div>

            <Card>
                <CardHeader>
                    <CardTitle>User Dashboard</CardTitle>
                </CardHeader>
                <CardContent>
                    <div className="overflow-x-auto">
                        <table className="w-full text-sm text-left">
                            <thead className="border-b">
                                <tr>
                                    <th>ID</th>
                                    <th>UserName</th>
                                    <th>Name Surname </th>
                                    <th>Email</th>
                                    <th>Role</th>
                                    <th>Status</th>
                                    <th className="text-center">Operations</th>
                                </tr>
                            </thead>
                            <tbody>
                                {users.map((user) => (
                                    <tr key={user.userId} className="border-b">
                                        <td>{user.userId}</td>
                                        <td>{user.username}</td>
                                        <td>{`${user.firstName || ""} ${user.lastName || ""}`}</td>
                                        <td>{user.email}</td>
                                        <td>{user.roleName}</td>
                                        <td>{user.isActive ? "Active" : "Passive"}</td>
                                        <td className="flex gap-2 justify-center py-2">
                                            <Button size="sm" variant="secondary" onClick={() => openEditModal(user)}>
                                                <Pencil className="w-4 h-4" />
                                            </Button>
                                            <Button size="sm" variant="destructive" onClick={() => handleDelete(user.userId!)}>
                                                <Trash2 className="w-4 h-4" />
                                            </Button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </CardContent>
            </Card>

            {/* Edit Modal */}
            <Dialog open={isEditOpen} onOpenChange={setIsEditOpen}>
                <DialogContent>
                    <DialogHeader>
                        <DialogTitle>Edit User</DialogTitle>
                    </DialogHeader>
                    <div className="space-y-2">
                        <Label>UserName</Label>
                        <Input name="username" value={formData.username || ""} onChange={handleChange} />

                        <Label>FirstName</Label>
                        <Input name="firstName" value={formData.firstName || ""} onChange={handleChange} />

                        <Label>LastName</Label>
                        <Input name="lastName" value={formData.lastName || ""} onChange={handleChange} />

                        <Label>Email</Label>
                        <Input name="email" value={formData.email || ""} onChange={handleChange} />

                        <Label>Role</Label>
                        <select
                            name="roleId"
                            value={formData.roleId || 1}
                            onChange={(e) => setFormData({ ...formData, roleId: parseInt(e.target.value) })}
                            className="w-full border rounded px-3 py-2"
                        >
                            <option value={1}>Attendee</option>
                            <option value={2}>Organizer</option>
                            <option value={3}>Admin</option>
                        </select>

                        <div className="flex items-center gap-2 mt-2">
                            <input
                                type="checkbox"
                                id="isActive"
                                checked={formData.isActive || false}
                                onChange={(e) => setFormData({ ...formData, isActive: e.target.checked })}
                            />
                            <Label htmlFor="isActive">Active?</Label>
                        </div>

                        <Button onClick={handleEditSubmit} className="mt-4 w-full">Update</Button>
                    </div>
                </DialogContent>
            </Dialog>


            {/* Add Modal */}
            <Dialog open={isAddOpen} onOpenChange={setIsAddOpen}>
                <DialogContent>
                    <DialogHeader>
                        <DialogTitle>Add New User</DialogTitle>
                    </DialogHeader>
                    <div className="space-y-2">
                        <Label>UserName</Label>
                        <Input name="username" value={formData.username || ""} onChange={handleChange} />
                        <Label>FirstName</Label>
                        <Input name="firstName" value={formData.firstName || ""} onChange={handleChange} />
                        <Label>LastName</Label>
                        <Input name="lastName" value={formData.lastName || ""} onChange={handleChange} />
                        <Label>Email</Label>
                        <Input name="email" value={formData.email || ""} onChange={handleChange} />
                        <Button onClick={handleAddSubmit} className="mt-4 w-full">Save</Button>
                    </div>
                </DialogContent>
            </Dialog>
        </div>
    );
};

export default UserDashboard;
