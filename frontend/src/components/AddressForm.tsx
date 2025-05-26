import React, { useState, useEffect } from "react";
import { AddressesDto } from "../types/AddressesDto";

interface Props {
    initialData?: AddressesDto;
    onSubmit: (data: AddressesDto) => void;
    loading?: boolean;
}

const AddressForm: React.FC<Props> = ({ initialData, onSubmit, loading = false }) => {
    const [formData, setFormData] = useState<AddressesDto>({
        city: "",
        country: "",
        street: "",
        district: "",
        postalCode: "",
    });

    useEffect(() => {
        if (initialData) {
            setFormData(initialData);
        }
    }, [initialData]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        onSubmit(formData);
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            <div>
                <label>Şehir (city)</label>
                <input
                    type="text"
                    name="city"
                    value={formData.city}
                    onChange={handleChange}
                    required
                />
            </div>

            <div>
                <label>Ülke (country)</label>
                <input
                    type="text"
                    name="country"
                    value={formData.country}
                    onChange={handleChange}
                    required
                />
            </div>

            <div>
                <label>Cadde (street)</label>
                <input
                    type="text"
                    name="street"
                    value={formData.street || ""}
                    onChange={handleChange}
                />
            </div>

            <div>
                <label>İlçe (district)</label>
                <input
                    type="text"
                    name="district"
                    value={formData.district || ""}
                    onChange={handleChange}
                />
            </div>

            <div>
                <label>Posta Kodu (postalCode)</label>
                <input
                    type="text"
                    name="postalCode"
                    value={formData.postalCode || ""}
                    onChange={handleChange}
                />
            </div>

            <button type="submit" disabled={loading}>
                {loading ? "Kaydediliyor..." : "Kaydet"}
            </button>
        </form>
    );
};

export default AddressForm;
