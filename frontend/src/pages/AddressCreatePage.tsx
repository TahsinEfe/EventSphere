import React, { useState } from "react";
import { AddressAPI } from "../services/AddressAPI";
import AddressForm from "../components/AddressForm";
import { useNavigate } from "react-router-dom";
import { AddressesDto } from "../types/AddressesDto";

const AddressCreatePage = () => {
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleSubmit = async (data: AddressesDto) => {
        setLoading(true);
        try {
            await AddressAPI.create(data);
            navigate("/addresses");
        } catch (err) {
            console.error("Adres eklenemedi:", err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h2>Yeni Adres Ekle</h2>
            <AddressForm onSubmit={handleSubmit} loading={loading} />
        </div>
    );
};

export default AddressCreatePage;
