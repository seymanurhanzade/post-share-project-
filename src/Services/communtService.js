import axios from "axios";
import apiURL from "../Components/Environments/environment";

export const CommentPost= async (postId)=>{
    try{
        console.log("Giden veriler: ", postId);
        const response =await axios.get(apiURL.Base_URL+`Share/get-communt/${postId}`);
        localStorage.setItem("CommenPostId", response.data.id)
        return response.data;
    }catch(err){
        console.log("Hata: ", err);
    }
}

export const AddComment = async (userId, postId,communt)=>{
    try{
        const response = await axios.post(apiURL.Base_URL+'Share/add-communt', {userId, postId,communt})
        return {
            success: true,
            message: "Cevap gönderildi.",
        };

    }catch(err){
        console.log("Error: ", err);
        return {
            success: false,
            message: "Bir hata oldu. Lütfen tekrar deneyin.",
        };
    }
}