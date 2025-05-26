
import { Button } from "@/components/ui/button";
import Footer from "@/components/Footer";
import Navbar from "@/components/Navbar";
import { Separator } from "@/components/ui/separator";
import { Link } from "react-router-dom";

const About = () => {
  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      
      <div className="container mx-auto px-4 py-12">
        <div className="max-w-4xl mx-auto">
          <h1 className="text-3xl md:text-4xl font-bold mb-6 text-center">About EventSphere</h1>
          <div className="bg-accent/10 rounded-lg p-6 md:p-10 mb-10">
            <p className="text-lg mb-4">
              EventSphere is your premier destination for discovering and booking tickets to the best events in your city. 
              From electrifying concerts and captivating theater performances to insightful conferences and thrilling sports matches, 
              we bring you a curated selection of experiences to enrich your life.
            </p>
            <p className="text-lg">
              Founded in 2023, we've quickly grown to become a trusted platform for event organizers and attendees alike.
              Our mission is to connect people through shared experiences and make the process of finding and attending events as seamless as possible.
            </p>
          </div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 gap-12 mb-12">
            <div>
              <h2 className="text-2xl font-bold mb-4">Our Mission</h2>
              <p className="mb-4">
                At EventSphere, we believe that great experiences bring people together. Our mission is to make it effortless for everyone to discover, 
                book, and enjoy events that match their interests and passions.
              </p>
              <p>
                We're committed to providing a platform that serves both event attendees and organizers with equal dedication, 
                fostering a vibrant community around shared experiences.
              </p>
            </div>
            
            <div>
              <h2 className="text-2xl font-bold mb-4">Our Vision</h2>
              <p className="mb-4">
                We envision a world where everyone has easy access to enriching experiences that inspire, entertain, and connect.
              </p>
              <p>
                Through innovation and dedication to our users, we aim to become the most trusted global platform for event discovery and ticket purchasing, 
                known for our reliability, ease of use, and exceptional customer service.
              </p>
            </div>
          </div>
          
          <Separator className="my-12" />
          
          <h2 className="text-2xl font-bold mb-6 text-center">Why Choose EventSphere?</h2>
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6 mb-12">
            <div className="bg-card p-6 rounded-lg shadow-sm">
              <h3 className="text-xl font-semibold mb-2">Curated Selection</h3>
              <p>We handpick the best events across all categories to ensure quality experiences for our users.</p>
            </div>
            
            <div className="bg-card p-6 rounded-lg shadow-sm">
              <h3 className="text-xl font-semibold mb-2">Secure Booking</h3>
              <p>Our platform offers secure transaction processing and guaranteed ticket authenticity.</p>
            </div>
            
            <div className="bg-card p-6 rounded-lg shadow-sm">
              <h3 className="text-xl font-semibold mb-2">User-Friendly</h3>
              <p>Intuitive design makes finding and booking your next favorite event quick and easy.</p>
            </div>
          </div>
          
          <div className="text-center mb-12">
            <h2 className="text-2xl font-bold mb-6">Ready to Discover Amazing Events?</h2>
            <div className="flex flex-col sm:flex-row justify-center gap-4">
              <Link to="/events">
                <Button size="lg">Browse Events</Button>
              </Link>
              <Link to="/contact">
                <Button variant="outline" size="lg">Contact Us</Button>
              </Link>
            </div>
          </div>
        </div>
      </div>
      
      <div className="mt-auto">
        <Footer />
      </div>
    </div>
  );
};

export default About;
