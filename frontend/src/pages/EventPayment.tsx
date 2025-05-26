
import { useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import { mockEvents } from "@/data/mockData";
import { CreditCard, Check } from "lucide-react";
import { useToast } from "@/components/ui/use-toast";

const paymentSchema = z.object({
  cardNumber: z.string().min(16, "Card number must be at least 16 digits").max(19),
  cardName: z.string().min(2, "Name is required"),
  expiry: z.string().regex(/^(0[1-9]|1[0-2])\/[0-9]{2}$/, "Expiry must be in MM/YY format"),
  cvc: z.string().min(3, "CVC must be at least 3 digits").max(4)
});

const EventPayment = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { toast } = useToast();
  const [isProcessing, setIsProcessing] = useState(false);
  const [isComplete, setIsComplete] = useState(false);
  
  const event = mockEvents.find(event => event.id === parseInt(id || "0"));
  
  const form = useForm<z.infer<typeof paymentSchema>>({
    resolver: zodResolver(paymentSchema),
    defaultValues: {
      cardNumber: "",
      cardName: "",
      expiry: "",
      cvc: ""
    }
  });
  
  if (!event) {
    return (
      <div className="min-h-screen flex flex-col">
        <Navbar />
        <div className="container mx-auto flex-1 flex items-center justify-center">
          <Card className="w-full max-w-md">
            <CardHeader>
              <CardTitle>Event Not Found</CardTitle>
              <CardDescription>We couldn't find the event you're looking for.</CardDescription>
            </CardHeader>
            <CardFooter>
              <Button onClick={() => navigate("/events")} className="w-full">
                Back to Events
              </Button>
            </CardFooter>
          </Card>
        </div>
        <Footer />
      </div>
    );
  }
  
  const handlePayment = (data: z.infer<typeof paymentSchema>) => {
    setIsProcessing(true);
    
    // Simulate payment processing
    setTimeout(() => {
      setIsProcessing(false);
      setIsComplete(true);
      
      toast({
        title: "Payment Successful",
        description: `Your payment for "${event.name}" has been processed successfully.`,
        variant: "default",
      });
      
      // Redirect after short delay
      setTimeout(() => {
        navigate("/events");
      }, 3000);
    }, 2000);
  };
  
  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      
      <div className="container mx-auto py-8 flex-1">
        <div className="max-w-4xl mx-auto">
          <div className="flex flex-col md:flex-row gap-6">
            <div className="w-full md:w-1/2">
              <Card>
                <CardHeader>
                  <CardTitle>Event Details</CardTitle>
                  <CardDescription>Reviewing your ticket purchase</CardDescription>
                </CardHeader>
                <CardContent>
                  <div className="space-y-4">
                    <div className="aspect-[16/9] overflow-hidden rounded-md">
                      <img
                        src={event.imageUrl || "/placeholder.svg"}
                        alt={event.name}
                        className="h-full w-full object-cover"
                      />
                    </div>
                    
                    <div>
                      <h3 className="font-bold text-xl">{event.name}</h3>
                      <p className="text-muted-foreground mt-1">
                        {new Date(event.startDateTime).toLocaleDateString("en-US", {
                          day: "numeric",
                          month: "long",
                          year: "numeric",
                          hour: "2-digit",
                          minute: "2-digit"
                        })}
                      </p>
                      <p className="mt-2">
                        {event.address?.city}, {event.address?.country}
                      </p>
                    </div>
                    
                    <div className="border-t pt-4 mt-4">
                      <div className="flex justify-between">
                        <span>Ticket Price</span>
                        <span>{event.price ? `${event.price} TL` : "Free"}</span>
                      </div>
                      <div className="flex justify-between font-bold mt-2">
                        <span>Total</span>
                        <span>{event.price ? `${event.price} TL` : "Free"}</span>
                      </div>
                    </div>
                  </div>
                </CardContent>
              </Card>
            </div>
            
            <div className="w-full md:w-1/2">
              {!isComplete ? (
                <Card>
                  <CardHeader>
                    <CardTitle>Payment Information</CardTitle>
                    <CardDescription>Enter your card details to complete the purchase</CardDescription>
                  </CardHeader>
                  <CardContent>
                    <Form {...form}>
                      <form onSubmit={form.handleSubmit(handlePayment)} className="space-y-4">
                        <FormField
                          control={form.control}
                          name="cardName"
                          render={({ field }) => (
                            <FormItem>
                              <FormLabel>Cardholder Name</FormLabel>
                              <FormControl>
                                <Input placeholder="John Doe" {...field} />
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                        
                        <FormField
                          control={form.control}
                          name="cardNumber"
                          render={({ field }) => (
                            <FormItem>
                              <FormLabel>Card Number</FormLabel>
                              <FormControl>
                                <Input placeholder="1234 5678 9012 3456" {...field} />
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                        
                        <div className="grid grid-cols-2 gap-4">
                          <FormField
                            control={form.control}
                            name="expiry"
                            render={({ field }) => (
                              <FormItem>
                                <FormLabel>Expiry Date</FormLabel>
                                <FormControl>
                                  <Input placeholder="MM/YY" {...field} />
                                </FormControl>
                                <FormMessage />
                              </FormItem>
                            )}
                          />
                          
                          <FormField
                            control={form.control}
                            name="cvc"
                            render={({ field }) => (
                              <FormItem>
                                <FormLabel>CVC</FormLabel>
                                <FormControl>
                                  <Input placeholder="123" {...field} />
                                </FormControl>
                                <FormMessage />
                              </FormItem>
                            )}
                          />
                        </div>
                        
                        <Button 
                          type="submit" 
                          className="w-full mt-4" 
                          disabled={isProcessing}
                        >
                          {isProcessing ? (
                            <>Processing Payment...</>
                          ) : (
                            <>
                              <CreditCard className="mr-2 h-4 w-4" />
                              Pay {event.price ? `${event.price} TL` : "Free"}
                            </>
                          )}
                        </Button>
                      </form>
                    </Form>
                  </CardContent>
                </Card>
              ) : (
                <Card>
                  <CardHeader>
                    <CardTitle className="text-center text-green-600">Payment Successful</CardTitle>
                  </CardHeader>
                  <CardContent className="text-center">
                    <div className="mx-auto bg-green-100 w-16 h-16 rounded-full flex items-center justify-center mb-4">
                      <Check className="w-8 h-8 text-green-600" />
                    </div>
                    <p className="mb-4">
                      Thank you for your purchase! Your ticket for "{event.name}" has been confirmed.
                    </p>
                    <p className="text-muted-foreground">
                      A confirmation email has been sent to your email address.
                    </p>
                    <Button className="mt-6" onClick={() => navigate("/events")}>
                      Return to Events
                    </Button>
                  </CardContent>
                </Card>
              )}
            </div>
          </div>
        </div>
      </div>
      
      <Footer />
    </div>
  );
};

export default EventPayment;
