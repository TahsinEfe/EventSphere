
interface QRCodeProps {
  value: string;
  size?: number;
}

const QRCode = ({ value, size = 128 }: QRCodeProps) => {
  // For a real implementation, you would use a QR code library
  // This is a placeholder that uses an external QR code service
  const qrCodeUrl = `https://api.qrserver.com/v1/create-qr-code/?size=${size}x${size}&data=${encodeURIComponent(value)}`;
  
  return (
    <img 
      src={qrCodeUrl} 
      alt="QR Code"
      width={size}
      height={size}
    />
  );
};

export default QRCode;
