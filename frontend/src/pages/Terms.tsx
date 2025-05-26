
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import { Separator } from "@/components/ui/separator";

const Terms = () => {
  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      
      <div className="container mx-auto px-4 py-12">
        <div className="max-w-4xl mx-auto">
          <h1 className="text-3xl md:text-4xl font-bold mb-4">Terms of Service</h1>
          <p className="text-muted-foreground mb-8">Last updated: May 18, 2025</p>
          
          <div className="prose prose-slate max-w-none">
            <h2 className="text-2xl font-semibold mt-8 mb-4">1. Introduction</h2>
            <p>
              Welcome to EventSphere! These Terms of Service ("Terms") govern your access to and use of
              the EventSphere website, mobile applications, and services (collectively, the "Services").
              By accessing or using our Services, you agree to be bound by these Terms and our Privacy Policy.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">2. Definitions</h2>
            <p>
              "EventSphere," "we," "our," or "us" refers to EventSphere, Inc. and its subsidiaries and affiliates.
            </p>
            <p>
              "User," "you," or "your" refers to the individual or entity accessing or using the Services.
            </p>
            <p>
              "Content" refers to any text, images, graphics, videos, or other material that is uploaded, 
              posted, or otherwise made available through the Services.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">3. Account Registration</h2>
            <p>
              To access certain features of our Services, you may be required to create an account. When 
              registering for an account, you agree to provide accurate, current, and complete information. 
              You are responsible for maintaining the confidentiality of your account credentials and for 
              all activities that occur under your account.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">4. Purchasing Tickets</h2>
            <p>
              When you purchase tickets through our Services, you agree to provide accurate payment information 
              and authorize us to charge your payment method for the total amount of your purchase, including any 
              applicable taxes and fees. All ticket sales are final, and we do not offer refunds or exchanges 
              unless required by law or as specified in an event's refund policy.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">5. User Conduct</h2>
            <p>
              You agree not to use the Services to:
            </p>
            <ul className="list-disc pl-6 mb-4">
              <li>Violate any applicable laws or regulations</li>
              <li>Infringe on the rights of others</li>
              <li>Post or transmit harmful, fraudulent, or deceptive content</li>
              <li>Attempt to gain unauthorized access to our systems or user accounts</li>
              <li>Engage in any activity that interferes with or disrupts the Services</li>
            </ul>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">6. Intellectual Property Rights</h2>
            <p>
              The Services and all content and materials available through the Services are protected by copyright, 
              trademark, and other intellectual property laws. You may not reproduce, distribute, modify, create 
              derivative works of, publicly display, publicly perform, republish, download, store, or transmit 
              any of the material on our Services without our express prior written permission.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">7. Limitation of Liability</h2>
            <p>
              To the fullest extent permitted by applicable law, EventSphere shall not be liable for any indirect,
              incidental, special, consequential, or punitive damages, or any loss of profits or revenues, 
              whether incurred directly or indirectly, or any loss of data, use, goodwill, or other intangible 
              losses resulting from your access to or use of or inability to access or use the Services.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">8. Modifications to the Terms</h2>
            <p>
              We may modify these Terms at any time. If we make changes, we will provide notice of such changes, 
              such as by posting the updated Terms to the Services or sending you a notification. Your continued 
              use of the Services following the posting of updated Terms constitutes your acceptance of the changes.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">9. Termination</h2>
            <p>
              We may terminate or suspend your access to the Services immediately, without prior notice or liability, 
              for any reason, including if you breach these Terms. Upon termination, your right to use the Services 
              will immediately cease.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">10. Governing Law</h2>
            <p>
              These Terms shall be governed by and construed in accordance with the laws of the State of California, 
              without regard to its conflict of law provisions. Any dispute arising from these Terms shall be resolved 
              exclusively in the state or federal courts located in San Francisco County, California.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">11. Contact Information</h2>
            <p>
              If you have any questions about these Terms, please contact us at legal@eventsphere.com.
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

export default Terms;
