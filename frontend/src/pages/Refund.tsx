
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";

const Refund = () => {
  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      
      <div className="container mx-auto px-4 py-12">
        <div className="max-w-4xl mx-auto">
          <h1 className="text-3xl md:text-4xl font-bold mb-4">Refund Policy</h1>
          <p className="text-muted-foreground mb-8">Last updated: May 18, 2025</p>
          
          <div className="prose prose-slate max-w-none">
            <p className="lead">
              We appreciate your purchase of tickets through EventSphere. Please review our refund policy outlined below.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">General Refund Policy</h2>
            <p>
              All ticket sales on EventSphere are generally final and non-refundable unless otherwise specified by the event organizer.
              However, refunds may be available in specific circumstances as detailed below.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Event Cancellations</h2>
            <p>
              If an event is canceled by the organizer, you will automatically receive a full refund of the ticket price, 
              including any fees that were applied during your purchase. Refunds will be processed within 10 business days 
              and will be credited to the original payment method used for the purchase.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Event Rescheduling</h2>
            <p>
              If an event is rescheduled by the organizer, your tickets will typically remain valid for the new date. 
              If you are unable to attend the rescheduled event, you may be eligible for a refund, depending on the 
              organizer's policy. To request a refund for a rescheduled event, please contact us within 7 days of the 
              announcement of the new date.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Event Changes</h2>
            <p>
              If there are significant changes to an event (such as venue change, major lineup changes for a concert 
              or festival), refunds may be available at the discretion of the event organizer. Please contact our 
              customer support team to inquire about refund eligibility in such cases.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Refunds by Request</h2>
            <p>
              In certain cases, you may request a refund for your tickets, subject to the following conditions:
            </p>
            <ul className="list-disc pl-6 mb-4">
              <li>
                <strong>Refund Window:</strong> Refund requests must be submitted at least 14 days before the event date, 
                unless a different policy is specified by the organizer on the event page.
              </li>
              <li>
                <strong>Refund Fees:</strong> If your refund request is approved, a processing fee (typically 10% of the 
                ticket price) may be deducted from the refund amount.
              </li>
              <li>
                <strong>Eligibility:</strong> Not all events are eligible for refunds by request. The refund policy 
                for each event is displayed on the event's ticket purchase page.
              </li>
            </ul>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">How to Request a Refund</h2>
            <p>
              To request a refund:
            </p>
            <ol className="list-decimal pl-6 mb-4">
              <li>Log into your EventSphere account</li>
              <li>Navigate to "My Tickets"</li>
              <li>Select the order for which you want a refund</li>
              <li>Click the "Request Refund" button and follow the instructions</li>
            </ol>
            <p>
              Alternatively, you can contact our customer support team at refunds@eventsphere.com or call us at 
              +1 (555) 123-4567.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Processing Time</h2>
            <p>
              Approved refunds are typically processed within 5-10 business days. However, it may take an additional 
              3-5 business days for the funds to appear in your account, depending on your financial institution.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Special Circumstances</h2>
            <p>
              We understand that special circumstances may arise that prevent you from attending an event. In cases 
              of serious illness, family emergency, or other exceptional situations, please contact our customer 
              support team with any supporting documentation, and we will work with the event organizer to explore 
              possible refund options.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Contact Information</h2>
            <p>
              If you have any questions about our refund policy or need assistance with a refund request, please 
              contact us at:
            </p>
            <p>
              Email: refunds@eventsphere.com<br />
              Phone: +1 (555) 123-4567<br />
              Hours: Monday-Friday, 9am-6pm EST
            </p>
          </div>
        </div>
      </div>
      
      <div className="mt-auto">
        <Footer />
      </div>
    </div>
  );
};

export default Refund;
