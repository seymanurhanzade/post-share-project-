import axios from "axios";
import apiURL from "../Components/Environments/environment";



export const shareService=async (userId, share)=> {
    try{
        console.log("Gönderilen veriler: ", {userId,share});
        if(userId!= null&& share!=null){
            const response = await axios.post(apiURL.Base_URL+'Share/share-add', {userId,share});
            return {
                success: true,
                message: "Gönderi başarıyla oluşturuldu.",
            };
        }
        else{
            // Hata mesajını döndür
            return {
                success: false,
                message: "Bir hata oluştu. Lütfen tekrar deneyin.",
            };
        }
    }catch(error){
        // Hata mesajını döndür
        return {
            success: false,
            message: error.response?.data || "Bir hata oluştu. Lütfen tekrar deneyin.",
        };
    }
}

export const getAllShare= async(userId)=>{
    try{
        const response = await axios.get(apiURL.Base_URL+`Share/home/${userId}`);
        return response.data;
    }catch(error){
        console.log("veriler yok.", error);
    }
}

export const PostDetails= async (postId,userId)=>{
    try{
        const response = await axios.get(apiURL.Base_URL+`Share/post-detail/${postId}/${userId}`);
        return response.data;
    }catch(error){
        console.log("post details hata: ", error);
    }
}

export const Delete = (id)=>{
    try{
        const response = axios.delete(apiURL.Base_URL+'Share/Delete', {
            params:{id: id}
        });
        return {
            success: true,
            message: "Gönderi silindi.",
        };
    }catch(err){
        return {
            success: false,
            message: "Bir hata oldu. Lütfen tekrar deneyin.",
        };
    }
}

export const IsLikes=async(postId,userId)=>{
    try{
        await axios.post(apiURL.Base_URL+ 'Share/like-add', {postId,userId});
    }catch(err){
        console.log("Error: ",err.message);
    }
}
export const IsPostLikedByUser = async (postId, userId) => {
    try {

        const response = await axios.get(
            `${apiURL.Base_URL}Share/like-post`,
            {
                params: {
                    postId: postId,
                    userId: userId,
                },
            }
        );

        return response.data; 
    } catch (err) {
        console.log("Error:", err.message);
    }
};



