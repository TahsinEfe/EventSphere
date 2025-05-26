import React, { useEffect, useState } from "react";
import { AddressAPI } from "../services/AddressAPI";
import { AddressesDto } from "../types/AddressesDto";

const AddressListPage = () => {
    const [addresses, setAddresses] = useState<AddressesDto[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchAddresses = async () => {
            try {
                const res = await AddressAPI.getAll();
                setAddresses(res.data);
            } catch (err) {
                console.error("Adresler alınamadı:", err);
            } finally {
                setLoading(false);
            }
        };

        fetchAddresses();
    }, []);

    if (loading) return <p>Yükleniyor...</p>;

    return (
        <div>
            <h2>Adres Listesi</h2>
            <ul>
                {addresses.map((addr) => (
                    <li key={addr.addressId}>
                        {addr.street ?? "-"}, {addr.district ?? "-"}, {addr.city}, {addr.country}, {addr.postalCode ?? "-"}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default AddressListPage;
