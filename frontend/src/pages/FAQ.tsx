
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger
} from "@/components/ui/accordion";

const FAQ = () => {
  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      
      <div className="container mx-auto px-4 py-12">
        <div className="max-w-3xl mx-auto">
          <h1 className="text-3xl md:text-4xl font-bold mb-6 text-center">Frequently Asked Questions</h1>
          <p className="text-muted-foreground text-center mb-8">
            Find answers to the most common questions about EventSphere and our ticketing services.
          </p>
          
          <Accordion type="single" collapsible className="w-full">
            <AccordionItem value="item-1">
              <AccordionTrigger>How do I purchase tickets?</AccordionTrigger>
              <AccordionContent>
                To purchase tickets, browse our events section, select the event you're interested in, 
                and click "Buy Tickets." Follow the prompts to select your seats and complete the payment process. 
                Once your purchase is complete, you'll receive a confirmation email with your e-tickets.
              </AccordionContent>
            </AccordionItem>
            
            <AccordionItem value="item-2">
              <AccordionTrigger>What payment methods do you accept?</AccordionTrigger>
              <AccordionContent>
                We accept all major credit cards (Visa, Mastercard, American Express), PayPal, and 
                Apple Pay. In some regions, we also support local payment methods. All transactions 
                are secure and encrypted.
              </AccordionContent>
            </AccordionItem>
            
            <AccordionItem value="item-3">
              <AccordionTrigger>Can I get a refund if I can't attend an event?</AccordionTrigger>
              <AccordionContent>
                Refund policies vary by event. Some events allow refunds up to a certain date before the event, 
                while others may be non-refundable. Please check the specific event's details page for refund 
                policies. In case of event cancellation by the organizer, you will automatically receive a 
                full refund.
              </AccordionContent>
            </AccordionItem>
            
            <AccordionItem value="item-4">
              <AccordionTrigger>How do I access my tickets?</AccordionTrigger>
              <AccordionContent>
                After completing your purchase, tickets are emailed to the address you provided during checkout. 
                You can also access your tickets at any time by logging into your EventSphere account and visiting the
                "My Tickets" section. Most venues accept mobile tickets, but you can also print your tickets if 
                you prefer.
              </AccordionContent>
            </AccordionItem>
            
            <AccordionItem value="item-5">
              <AccordionTrigger>Are there any additional fees when purchasing tickets?</AccordionTrigger>
              <AccordionContent>
                Yes, there may be service fees and processing fees added to the ticket price. These fees help 
                cover the costs of our platform and payment processing. All fees are clearly displayed before 
                you complete your purchase so there are no surprises.
              </AccordionContent>
            </AccordionItem>
            
            <AccordionItem value="item-6">
              <AccordionTrigger>Can I transfer my tickets to someone else?</AccordionTrigger>
              <AccordionContent>
                Yes, most tickets can be transferred to another person. Log into your account, go to "My Tickets," 
                select the tickets you wish to transfer, and follow the steps to send them via email. Please note 
                that some events may have restrictions on ticket transfers.
              </AccordionContent>
            </AccordionItem>
            
            <AccordionItem value="item-7">
              <AccordionTrigger>What if the event is canceled or rescheduled?</AccordionTrigger>
              <AccordionContent>
                If an event is canceled, you will be notified via email and automatically receive a full refund. 
                If an event is rescheduled, your tickets will typically be valid for the new date. If you cannot 
                attend on the new date, refund options may be available depending on the organizer's policy.
              </AccordionContent>
            </AccordionItem>
            
            <AccordionItem value="item-8">
              <AccordionTrigger>How do I contact customer support?</AccordionTrigger>
              <AccordionContent>
                You can contact our customer support team through the "Contact Us" page on our website, by email 
                at support@eventsphere.com, or by phone at +1 (555) 123-4567. Our support hours are Monday through 
                Friday, 9am-6pm, and Saturday, 10am-4pm.
              </AccordionContent>
            </AccordionItem>
          </Accordion>
        </div>
      </div>
      
      <div className="mt-auto">
        <Footer />
      </div>
    </div>
  );
};

export default FAQ;
