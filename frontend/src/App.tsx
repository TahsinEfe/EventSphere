import { Toaster } from "@/components/ui/toaster";
import { Toaster as Sonner } from "@/components/ui/sonner";
import { TooltipProvider } from "@/components/ui/tooltip";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import Index from "./pages/Index";
import Events from "./pages/Events";
import About from "./pages/About";
import Contact from "./pages/Contact";
import FAQ from "./pages/FAQ";
import Terms from "./pages/Terms";
import Privacy from "./pages/Privacy";
import Refund from "./pages/Refund";
import NotFound from "./pages/NotFound";
import EventDetail from "./pages/EventDetail";
import EventPayment from "./pages/EventPayment";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Dashboard from "./pages/Dashboard";
import Feedback from "./pages/Feedback";
import Admin from "./pages/Admin";
import EditProfile from "./pages/EditProfile";
import ViewTicket from "./pages/ViewTicket";
import AddressListPage from "./pages/AddressListPage";
import AddressCreatePage from "./pages/AddressCreatePage";
import AddressEditPage from "./pages/AddressEditPage";
import OrganizationsDashboard from "./pages/OrganizationsDasboard"; 

const queryClient = new QueryClient();

const App = () => (
    <QueryClientProvider client={queryClient}>
        <TooltipProvider>
            <Toaster />
            <Sonner />
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<Index />} />
                    <Route path="/events" element={<Events />} />
                    <Route path="/events/:id" element={<EventDetail />} />
                    <Route path="/events/:id/payment" element={<EventPayment />} />
                    <Route path="/about" element={<About />} />
                    <Route path="/contact" element={<Contact />} />
                    <Route path="/feedback" element={<Feedback />} />
                    <Route path="/faq" element={<FAQ />} />
                    <Route path="/terms" element={<Terms />} />
                    <Route path="/privacy" element={<Privacy />} />
                    <Route path="/refund" element={<Refund />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/dashboard" element={<Dashboard />} />
                    <Route path="/admin" element={<Admin />} />
                    <Route path="/edit-profile" element={<EditProfile />} />
                    <Route path="/tickets/:id" element={<ViewTicket />} />
                    <Route path="/addresses" element={<AddressListPage />} />
                    <Route path="/addresses/create" element={<AddressCreatePage />} />
                    <Route path="/addresses/edit/:id" element={<AddressEditPage />} />

                    {/* ✅ NEW ROUTE */}
                    <Route path="/organizations" element={<OrganizationsDashboard />} />

                    {/* Fallback */}
                    <Route path="*" element={<NotFound />} />
                </Routes>
            </BrowserRouter>
        </TooltipProvider>
    </QueryClientProvider>
);

export default App;
