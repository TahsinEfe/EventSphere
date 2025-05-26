import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { AddressAPI } from "../services/AddressAPI";
import { AddressesDto } from "../types/AddressesDto";
import AddressForm from "../components/AddressForm";

const AddressEditPage = () => {
    const { id } = useParams();
    const [address, setAddress] = useState<AddressesDto | null>(null);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchData = async () => {
            if (id) {
                try {
                    const res = await AddressAPI.getById(Number(id));
                    setAddress(res.data);
                } catch (err) {
                    console.error("Adres alınamadı:", err);
                }
            }
        };
        fetchData();
    }, [id]);

    const handleUpdate = async (data: AddressesDto) => {
        setLoading(true);
        try {
            await AddressAPI.update(Number(id), data);
            navigate("/addresses");
        } catch (err) {
            console.error("Adres güncellenemedi:", err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h2>Adres Güncelle</h2>
            {address ? (
                <AddressForm initialData={address} onSubmit={handleUpdate} loading={loading} />
            ) : (
                <p>Adres yükleniyor...</p>
            )}
        </div>
    );
};

export default AddressEditPage;
