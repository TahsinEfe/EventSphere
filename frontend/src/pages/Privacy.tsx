
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";

const Privacy = () => {
  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      
      <div className="container mx-auto px-4 py-12">
        <div className="max-w-4xl mx-auto">
          <h1 className="text-3xl md:text-4xl font-bold mb-4">Privacy Policy</h1>
          <p className="text-muted-foreground mb-8">Last updated: May 18, 2025</p>
          
          <div className="prose prose-slate max-w-none">
            <p className="lead mb-8">
              At EventSphere, we take your privacy seriously. This Privacy Policy explains how we collect, use,
              disclose, and safeguard your information when you visit our website or use our services.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Information We Collect</h2>
            <h3 className="text-xl font-medium mt-4 mb-2">Personal Information</h3>
            <p>
              We may collect personal information that you voluntarily provide to us when you:
            </p>
            <ul className="list-disc pl-6 mb-4">
              <li>Create an account on our platform</li>
              <li>Purchase tickets to events</li>
              <li>Sign up for our newsletter</li>
              <li>Contact our customer support</li>
              <li>Participate in surveys or promotions</li>
            </ul>
            <p>
              This information may include your name, email address, postal address, phone number, payment information, 
              and any other information you choose to provide.
            </p>
            
            <h3 className="text-xl font-medium mt-4 mb-2">Automatically Collected Information</h3>
            <p>
              When you access our website or use our services, we may automatically collect certain information, including:
            </p>
            <ul className="list-disc pl-6 mb-4">
              <li>IP address</li>
              <li>Browser type and version</li>
              <li>Device type and operating system</li>
              <li>Pages visited and features used</li>
              <li>Time and date of your visit</li>
              <li>Referring website or application</li>
            </ul>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">How We Use Your Information</h2>
            <p>
              We may use the information we collect for various purposes, including to:
            </p>
            <ul className="list-disc pl-6 mb-4">
              <li>Process and fulfill your ticket purchases</li>
              <li>Create and manage your account</li>
              <li>Send you transaction confirmations and event updates</li>
              <li>Respond to your inquiries and provide customer support</li>
              <li>Send you marketing communications (if you've opted in)</li>
              <li>Improve and personalize our services</li>
              <li>Analyze usage patterns and trends</li>
              <li>Prevent fraudulent activities and enhance security</li>
              <li>Comply with legal obligations</li>
            </ul>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Information Sharing and Disclosure</h2>
            <p>
              We may share your information with:
            </p>
            <ul className="list-disc pl-6 mb-4">
              <li>Event organizers (limited to information necessary for event management)</li>
              <li>Service providers that help us operate our business</li>
              <li>Payment processors to complete transactions</li>
              <li>Legal authorities when required by law</li>
            </ul>
            <p>
              We do not sell your personal information to third parties.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Data Security</h2>
            <p>
              We implement appropriate technical and organizational measures to protect your personal information 
              against unauthorized access, alteration, disclosure, or destruction. However, no method of transmission 
              over the Internet or electronic storage is 100% secure, and we cannot guarantee absolute security.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Your Privacy Rights</h2>
            <p>
              Depending on your location, you may have certain rights regarding your personal information, including:
            </p>
            <ul className="list-disc pl-6 mb-4">
              <li>The right to access your personal information</li>
              <li>The right to rectify inaccurate information</li>
              <li>The right to request deletion of your information</li>
              <li>The right to restrict or object to processing</li>
              <li>The right to data portability</li>
            </ul>
            <p>
              To exercise these rights, please contact us using the information provided in the "Contact Us" section.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Cookies and Tracking Technologies</h2>
            <p>
              We use cookies and similar tracking technologies to track activity on our website and collect certain information. 
              Cookies are small data files that are stored on your device. You can instruct your browser to refuse all cookies 
              or to indicate when a cookie is being sent.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Children's Privacy</h2>
            <p>
              Our services are not directed to individuals under the age of 16. We do not knowingly collect personal 
              information from children under 16. If you become aware that a child has provided us with personal 
              information, please contact us.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Changes to This Privacy Policy</h2>
            <p>
              We may update our Privacy Policy from time to time. We will notify you of any changes by posting the 
              new Privacy Policy on this page and updating the "Last updated" date at the top.
            </p>
            
            <h2 className="text-2xl font-semibold mt-8 mb-4">Contact Us</h2>
            <p>
              If you have any questions about this Privacy Policy, please contact us at:
            </p>
            <p>
              Email: privacy@eventsphere.com<br />
              Address: 123 Event Street, San Francisco, CA 94103, United States
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

export default Privacy;
