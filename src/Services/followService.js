import axios from "axios"
import ApiUrl from "../Components/Environments/environment";

export const  TakipEtServisi= async (userId, followerUserId)=>{
    try{
        console.log("jghd<d",userId,followerUserId);
        var responce = await axios.post(ApiUrl.Base_URL + "Following/follower-add", {userId,followerUserId});
        return responce.data;
    }catch(err){
        return err.message;
    }
}
export const TakipDurumu =async (userId, followerUserId)=>{
    try{
        
        var state = await axios.post(ApiUrl.Base_URL+ "Following/following-control", {userId,followerUserId})
        return state.data;
    }catch(err){
        return err.message;
    }
}

export const TakipList = async (userId)=>{
    try{
        var responce = await axios.get(ApiUrl.Base_URL+`Following/takipedilenler/${userId}`)
        console.log("verilersss: ", responce.data);
        return responce.data;

    }catch(err){
        return err.message;
    }
}
export const Takipci = async (userId)=>{
    try{
        var responce = await axios.get(ApiUrl.Base_URL+`Following/takipciler/${userId}`)
        return responce.data;

    }catch(err){
        return err.message;
    }
}


