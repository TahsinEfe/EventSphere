import { useEffect, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Menu, Search, User, X, LogOut } from "lucide-react";

interface AuthUser {
    username: string;
    firstName: string;
    email: string;
    isAdmin: boolean;
}

const Navbar = () => {
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const [authUser, setAuthUser] = useState<AuthUser | null>(null);
    const location = useLocation();
    const navigate = useNavigate();

    useEffect(() => {
        const stored = localStorage.getItem("user");
        if (stored) {
            const parsed = JSON.parse(stored);
            setAuthUser(parsed);
        }
    }, []);

    const toggleMenu = () => setIsMenuOpen(!isMenuOpen);
    const closeMenu = () => setIsMenuOpen(false);

    const handleLogout = () => {
        localStorage.removeItem("user");
        setAuthUser(null);
        navigate("/");
    };

    const isAdmin = authUser?.isAdmin;

    return (
        <header className="sticky top-0 z-50 w-full border-b bg-background/80 backdrop-blur-md">
            <div className="container flex h-16 items-center justify-between px-4 md:px-6">
                <Link to="/" className="flex items-center gap-2">
                    <img
                        src="/event-images/event-sphere-logo.png"
                        alt="EventSphere Logo"
                        className="h-8 w-8 object-contain"
                    />
                    <span className="text-2xl font-semibold bg-gradient-to-r from-primary to-accent bg-clip-text text-transparent">
                        EventSphere
                    </span>
                </Link>

                {/* Desktop */}
                <nav className="hidden md:flex items-center gap-6">
                    <Link to="/" className={`text-sm font-medium ${location.pathname === '/' ? 'text-primary' : 'text-muted-foreground hover:text-foreground'}`}>Home</Link>
                    <Link to="/events" className={`text-sm font-medium ${location.pathname.startsWith('/events') ? 'text-primary' : 'text-muted-foreground hover:text-foreground'}`}>Events</Link>

                    {isAdmin ? (
                        <Link to="/dashboard" className={`text-sm font-medium ${location.pathname === '/dashboard' ? 'text-primary' : 'text-muted-foreground hover:text-foreground'}`}>Dashboard</Link>
                    ) : (
                        <>
                            <Link to="/about" className={`text-sm font-medium ${location.pathname === '/about' ? 'text-primary' : 'text-muted-foreground hover:text-foreground'}`}>About</Link>
                            <Link to="/contact" className={`text-sm font-medium ${location.pathname === '/contact' ? 'text-primary' : 'text-muted-foreground hover:text-foreground'}`}>Contact</Link>
                        </>
                    )}
                </nav>

                {/* Actions */}
                <div className="flex items-center gap-2">
                    <Button variant="ghost" size="icon" className="md:hidden" onClick={toggleMenu}>
                        <Menu className="h-5 w-5" />
                    </Button>

                    <div className="hidden md:flex items-center gap-2">
                        <Button variant="ghost" size="icon">
                            <Search className="h-5 w-5" />
                        </Button>

                        {authUser ? (
                            <>
                                <span className="text-sm font-medium">{authUser.firstName}</span>
                                <Button variant="ghost" size="icon" onClick={handleLogout}>
                                    <LogOut className="h-5 w-5" />
                                    <span className="sr-only">Logout</span>
                                </Button>
                            </>
                        ) : (
                            <>
                                <Link to="/login"><Button variant="ghost">Login</Button></Link>
                                <Link to="/register"><Button>Sign up</Button></Link>
                            </>
                        )}
                    </div>
                </div>
            </div>

            {/* Mobile Menu */}
            {isMenuOpen && (
                <div className="fixed inset-0 z-50 bg-background">
                    <div className="container flex flex-col h-full">
                        <div className="flex items-center justify-between h-16 px-4">
                            <Link to="/" onClick={closeMenu}>
                                <span className="text-2xl font-semibold bg-gradient-to-r from-primary to-accent bg-clip-text text-transparent">
                                    EventSphere
                                </span>
                            </Link>
                            <Button variant="ghost" size="icon" onClick={closeMenu}>
                                <X className="h-5 w-5" />
                            </Button>
                        </div>

                        <nav className="flex flex-col gap-4 p-4">
                            <Link to="/" className="text-lg font-medium p-2" onClick={closeMenu}>Home</Link>
                            <Link to="/events" className="text-lg font-medium p-2" onClick={closeMenu}>Events</Link>

                            {isAdmin ? (
                                <Link to="/dashboard" className="text-lg font-medium p-2" onClick={closeMenu}>Dashboard</Link>
                            ) : (
                                <>
                                    <Link to="/about" className="text-lg font-medium p-2" onClick={closeMenu}>About</Link>
                                    <Link to="/contact" className="text-lg font-medium p-2" onClick={closeMenu}>Contact</Link>
                                </>
                            )}

                            <div className="h-px bg-border my-2" />

                            {authUser ? (
                                <>
                                    <Button variant="outline" onClick={() => { handleLogout(); closeMenu(); }}>
                                        Logout
                                    </Button>
                                </>
                            ) : (
                                <>
                                    <Link to="/login" className="text-lg font-medium p-2" onClick={closeMenu}>Login</Link>
                                    <Link to="/register" className="text-lg font-medium p-2" onClick={closeMenu}>Sign up</Link>
                                </>
                            )}
                        </nav>
                    </div>
                </div>
            )}
        </header>
    );
};

export default Navbar;
