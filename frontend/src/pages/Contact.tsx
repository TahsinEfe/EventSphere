import { useState, useEffect } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { useToast } from "@/components/ui/use-toast";
import Footer from "@/components/Footer";
import Navbar from "@/components/Navbar";
import { Card, CardContent } from "@/components/ui/card";
import { MailIcon, Phone } from "lucide-react";
import api from "@/services/api";

interface User {
    userId: number;
    firstName: string;
    lastName: string;
}

const Contact = () => {
    const [formData, setFormData] = useState({
        name: "",
        email: "",
        subject: "",
        message: "",
    });

    const [allUsers, setAllUsers] = useState<User[]>([]);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const { toast } = useToast();

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const res = await api.get("/Users");
                setAllUsers(res.data);
            } catch (err) {
                console.error("Kullanıcılar alınamadı", err);
            }
        };
        fetchUsers();
    }, []);

    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setIsSubmitting(true);

        const [firstName, ...lastParts] = formData.name.trim().split(" ");
        const lastName = lastParts.join(" ");

        const matchedUser = allUsers.find(
            (u) =>
                u.firstName.toLowerCase() === firstName.toLowerCase() &&
                u.lastName.toLowerCase() === lastName.toLowerCase()
        );

        if (!matchedUser) {
            toast({ title: "Kullanıcı bulunamadı.", variant: "destructive" });
            setIsSubmitting(false);
            return;
        }

        const payload = {
            title: formData.subject,
            message: formData.message,
        };

        try {
            await api.post(`/Notifications?userId=${matchedUser.userId}`, payload);
            toast({ title: "Mesaj bildirime kaydedildi." });
            setFormData({ name: "", email: "", subject: "", message: "" });
        } catch (err) {
            console.error(err);
            toast({ title: "Mesaj gönderilemedi.", variant: "destructive" });
        }

        setIsSubmitting(false);
    };

    return (
        <div className="min-h-screen flex flex-col">
            <Navbar />

            <div className="container mx-auto px-4 py-12">
                <div className="text-center mb-10">
                    <h1 className="text-3xl md:text-4xl font-bold">Contact Us</h1>
                    <p className="text-muted-foreground mt-3 max-w-2xl mx-auto">
                        Have questions or need assistance? We're here to help! Reach out to our team using the form below or contact us directly.
                    </p>
                </div>

                <div className="grid grid-cols-1 lg:grid-cols-3 gap-8 max-w-6xl mx-auto">
                    <div className="lg:col-span-2">
                        <Card>
                            <CardContent className="p-6">
                                <form onSubmit={handleSubmit} className="space-y-6">
                                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                        <div className="space-y-2">
                                            <label htmlFor="name" className="text-sm font-medium">
                                                Full Name
                                            </label>
                                            <Input
                                                id="name"
                                                name="name"
                                                placeholder="FirstName + LastName"
                                                value={formData.name}
                                                onChange={handleChange}
                                                required
                                            />
                                        </div>
                                        <div className="space-y-2">
                                            <label htmlFor="email" className="text-sm font-medium">
                                                Email Address
                                            </label>
                                            <Input
                                                id="email"
                                                name="email"
                                                type="email"
                                                placeholder="mail@example.com"
                                                value={formData.email}
                                                onChange={handleChange}
                                                required
                                            />
                                        </div>
                                    </div>
                                    <div className="space-y-2">
                                        <label htmlFor="subject" className="text-sm font-medium">
                                            Subject
                                        </label>
                                        <Input
                                            id="subject"
                                            name="subject"
                                            placeholder="Subject"
                                            value={formData.subject}
                                            onChange={handleChange}
                                            required
                                        />
                                    </div>
                                    <div className="space-y-2">
                                        <label htmlFor="message" className="text-sm font-medium">
                                            Message
                                        </label>
                                        <Textarea
                                            id="message"
                                            name="message"
                                            placeholder="Write your message..."
                                            value={formData.message}
                                            onChange={handleChange}
                                            rows={6}
                                            required
                                        />
                                    </div>
                                    <Button type="submit" className="w-full" disabled={isSubmitting}>
                                        {isSubmitting ? "Sending..." : "Submit"}
                                    </Button>
                                </form>
                            </CardContent>
                        </Card>
                    </div>

                    <div className="space-y-6">
                        <Card>
                            <CardContent className="p-6 flex items-start space-x-4">
                                <MailIcon className="h-6 w-6 text-primary shrink-0" />
                                <div>
                                    <h3 className="font-semibold">Email</h3>
                                    <p className="text-muted-foreground mt-1">
                                        info@eventsphere.com<br />
                                        support@eventsphere.com
                                    </p>
                                </div>
                            </CardContent>
                        </Card>
                        <Card>
                            <CardContent className="p-6 flex items-start space-x-4">
                                <Phone className="h-6 w-6 text-primary shrink-0" />
                                <div>
                                    <h3 className="font-semibold">Call Us</h3>
                                    <p className="text-muted-foreground mt-1">+90 555 123 45 67</p>
                                </div>
                            </CardContent>
                        </Card>
                    </div>
                </div>
            </div>

            <div className="mt-auto">
                <Footer />
            </div>
        </div>
    );
};

export default Contact;
