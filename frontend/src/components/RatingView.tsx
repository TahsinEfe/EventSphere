import { useEffect, useState } from "react";
import { Star } from "lucide-react";
import api from "@/services/api";
import { FeedbacksDto } from "@/types/FeedbacksDto";

interface RatingViewProps {
    eventId: number;
}

const RatingView = ({ eventId }: RatingViewProps) => {
    const [averageRating, setAverageRating] = useState<number | null>(null);

    useEffect(() => {
        const fetchRatings = async () => {
            try {
                const res = await api.get<FeedbacksDto[]>("/Feedbacks");
                const filtered = res.data.filter((f) => f.eventId === eventId);
                if (filtered.length > 0) {
                    const total = filtered.reduce((acc, curr) => acc + curr.rating, 0);
                    setAverageRating(parseFloat((total / filtered.length).toFixed(1)));
                }
            } catch (err) {
                console.error("Puanlar alınamadı", err);
            }
        };

        fetchRatings();
    }, [eventId]);

    if (averageRating === null) return null;

    return (
        <div className="flex items-center gap-2 text-yellow-500 font-medium">
            <Star className="w-5 h-5 fill-yellow-400" />
            {averageRating} / 5
        </div>
    );
};

export default RatingView;
