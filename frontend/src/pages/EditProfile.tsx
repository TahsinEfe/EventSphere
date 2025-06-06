import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import { useNavigate } from "react-router-dom";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { useToast } from "@/components/ui/use-toast";
import { Form, FormField, FormItem, FormLabel, FormControl, FormMessage } from "@/components/ui/form";
import { AuthAPI } from "@/services/auth";
import api from "@/services/api";
import { ArrowLeft } from "lucide-react";

const profileFormSchema = z.object({
    userId: z.number(),
    username: z.string().min(2),
    firstName: z.string().min(2),
    lastName: z.string().min(2),
    email: z.string().email(),
    password: z.string().min(4).optional(),
});

type ProfileFormValues = z.infer<typeof profileFormSchema>;

const EditProfile = () => {
    const { toast } = useToast();
    const [initialValues, setInitialValues] = useState<ProfileFormValues | null>(null);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const form = useForm<ProfileFormValues>({
        resolver: zodResolver(profileFormSchema),
        defaultValues: async () => initialValues ?? {},
        mode: "onChange",
    });

    useEffect(() => {
        const fetchUser = async () => {
            const currentUser = AuthAPI.getCurrentUser();
            if (!currentUser) return;

            try {
                const res = await api.get(`/Users/${currentUser.userId}`);
                setInitialValues(res.data);
                form.reset(res.data);
            } catch (err) {
                console.error("User fetch failed", err);
            }
        };

        fetchUser();
    }, []);

    const onSubmit = async (data: ProfileFormValues) => {
        setLoading(true);
        try {
            await api.put(`/Users/${data.userId}`, data);
            toast({ title: "Profile updated successfully." });
        } catch (err) {
            console.error("Update failed", err);
            toast({ title: "Update failed", variant: "destructive" });
        } finally {
            setLoading(false);
        }
    };

    if (!initialValues) return <div className="p-8">Loading...</div>;

    return (
        <div className="container mx-auto max-w-2xl py-10">
            <div className="mb-4">
                <Button variant="ghost" onClick={() => navigate(-1)}>
                    <ArrowLeft className="mr-2 h-4 w-4" />
                    Go Back
                </Button>
            </div>

            <Card>
                <CardHeader>
                    <CardTitle>Edit Profile</CardTitle>
                </CardHeader>
                <CardContent>
                    <div className="flex justify-center mb-6">
                        <Avatar className="h-20 w-20">
                            <AvatarImage src="/blank-profile.png" />
                            <AvatarFallback>{initialValues.firstName[0]}</AvatarFallback>
                        </Avatar>
                    </div>

                    <Form {...form}>
                        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
                            <FormField
                                control={form.control}
                                name="username"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>Username</FormLabel>
                                        <FormControl><Input {...field} /></FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />

                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                <FormField
                                    control={form.control}
                                    name="firstName"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>First Name</FormLabel>
                                            <FormControl><Input {...field} /></FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />

                                <FormField
                                    control={form.control}
                                    name="lastName"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Last Name</FormLabel>
                                            <FormControl><Input {...field} /></FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />
                            </div>

                            <FormField
                                control={form.control}
                                name="email"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>Email</FormLabel>
                                        <FormControl><Input {...field} /></FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />

                            <FormField
                                control={form.control}
                                name="password"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>New Password (optional)</FormLabel>
                                        <FormControl>
                                            <Input type="password" placeholder="Leave blank to keep old password" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />

                            <div className="flex justify-end">
                                <Button type="submit" disabled={loading}>
                                    {loading ? "Saving..." : "Save Changes"}
                                </Button>
                            </div>
                        </form>
                    </Form>
                </CardContent>
            </Card>
        </div>
    );
};

export default EditProfile;
