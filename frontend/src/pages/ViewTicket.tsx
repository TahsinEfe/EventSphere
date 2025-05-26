
import { useParams } from "react-router-dom";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Calendar, Clock, MapPin, Download, Share2, Ticket } from "lucide-react";
import { Badge } from "@/components/ui/badge";
import QRCode from "@/components/QRCode";

const ViewTicket = () => {
  const { id } = useParams<{ id: string }>();
  
  // Mock ticket data
  const ticketData = {
    id: id || "123456",
    eventName: "Summer Music Festival",
    date: "June 15, 2023",
    time: "6:00 PM",
    location: "Central Park, New York",
    ticketType: "VIP",
    price: "$150.00",
    ticketHolder: "John Doe",
    qrCode: "https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=TicketID-123456"
  };

  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      
      <div className="container mx-auto px-4 py-8 flex-1">
        <h1 className="text-3xl font-bold mb-6">Your Ticket</h1>
        
        <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
          <Card className="ticket-card">
            <CardHeader>
              <CardTitle className="text-2xl">{ticketData.eventName}</CardTitle>
              <Badge variant="default" className="w-fit">{ticketData.ticketType}</Badge>
            </CardHeader>
            <CardContent className="space-y-6">
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <p className="text-sm text-muted-foreground">Date</p>
                  <div className="flex items-center mt-1">
                    <Calendar className="h-4 w-4 mr-2" />
                    <span>{ticketData.date}</span>
                  </div>
                </div>
                <div>
                  <p className="text-sm text-muted-foreground">Time</p>
                  <div className="flex items-center mt-1">
                    <Clock className="h-4 w-4 mr-2" />
                    <span>{ticketData.time}</span>
                  </div>
                </div>
                <div className="col-span-2">
                  <p className="text-sm text-muted-foreground">Location</p>
                  <div className="flex items-center mt-1">
                    <MapPin className="h-4 w-4 mr-2" />
                    <span>{ticketData.location}</span>
                  </div>
                </div>
              </div>
              
              <div className="pt-4 border-t">
                <p className="text-sm text-muted-foreground">Ticket Holder</p>
                <p className="font-medium">{ticketData.ticketHolder}</p>
              </div>
              
              <div className="pt-4 border-t">
                <p className="text-sm text-muted-foreground">Price</p>
                <p className="font-medium">{ticketData.price}</p>
              </div>
              
              <div className="pt-4 border-t flex flex-col items-center">
                <p className="text-sm text-muted-foreground mb-2">Ticket ID: {ticketData.id}</p>
                <div className="bg-white p-4 rounded-lg">
                  <QRCode value={`TicketID-${ticketData.id}`} size={200} />
                </div>
                <p className="text-xs text-muted-foreground mt-2">Present this QR code at the event entrance</p>
              </div>
              
              <div className="flex justify-between mt-6">
                <Button variant="outline" className="flex items-center gap-2">
                  <Download className="h-4 w-4" />
                  Download
                </Button>
                <Button variant="outline" className="flex items-center gap-2">
                  <Share2 className="h-4 w-4" />
                  Share
                </Button>
                <Button className="flex items-center gap-2">
                  <Calendar className="h-4 w-4" />
                  Add to Calendar
                </Button>
              </div>
            </CardContent>
          </Card>
          
          <div className="space-y-6">
            <Card>
              <CardHeader>
                <CardTitle>Event Details</CardTitle>
              </CardHeader>
              <CardContent>
                <p className="text-muted-foreground">
                  Join us for an unforgettable day of music, food, and fun at the Summer Music Festival. 
                  Featuring top artists from around the world, this is an event you won't want to miss!
                </p>
                <Button variant="outline" className="mt-4 w-full">View Event Details</Button>
              </CardContent>
            </Card>
            
            <Card>
              <CardHeader>
                <CardTitle>Important Information</CardTitle>
              </CardHeader>
              <CardContent className="space-y-4">
                <div>
                  <h4 className="font-semibold">Entry Requirements</h4>
                  <p className="text-sm text-muted-foreground">Please bring a valid ID that matches the ticket holder's name.</p>
                </div>
                <div>
                  <h4 className="font-semibold">Items Not Allowed</h4>
                  <p className="text-sm text-muted-foreground">Outside food and drinks, professional cameras, and large bags are not permitted.</p>
                </div>
                <div>
                  <h4 className="font-semibold">Parking Information</h4>
                  <p className="text-sm text-muted-foreground">Paid parking is available at the venue. We recommend using public transportation.</p>
                </div>
              </CardContent>
            </Card>
          </div>
        </div>
      </div>
      
      <Footer />
    </div>
  );
};

export default ViewTicket;
