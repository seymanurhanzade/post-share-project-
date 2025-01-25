import axios from "axios"
import apiURL from "../Components/Environments/environment";

export const GetPosts= async ()=>{
    const responce = await axios.get(apiURL.Base_URL+'Admin/GetPosts');
    return responce.data;
}
export const GetUsers= async ()=>{
    const responce = await axios.get(apiURL.Base_URL+'Admin/GetUsers');
    return responce.data;
}
export const PostDelete= async (id)=>{
    try{
        const responce = await axios.delete(apiURL.Base_URL+'Admin/Delete', {
            params:{id: id}
        });
        return {
            success: true,
            message: "Gönderi silindi.",
        };
    }catch(err){
        console.log(err.message)
        return {
            success: false,
            message: "Gönderi silinemedi. Tekrar deneyin.",
            
        };
    }
    
}