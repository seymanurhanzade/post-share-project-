import axios from "axios";
import apiURL from "../Components/Environments/environment";

export const userProfilePost = async (userId) => {
    try {

        const response = await axios.get(apiURL.Base_URL + `User/user-profile/${userId}`);
        return response.data;
    } catch (err) {
        console.error("Hata oluştu: ", err.message.message || err);
        throw err;
    }
};

export const UserProfileDetail = async (userId) => {
    try {

        const response = await axios.get(apiURL.Base_URL + `User/profile/${userId}`);
        return response.data;
    } catch (err) {
        console.error("Hata oluştu: ", err.message.message || err);
        throw err;
    }
};

export const GetUsers= async ()=>{
    try{
        const response = await axios.get(apiURL.Base_URL+"User/GetUsers");
        console.log("response.data: ", response.data);
        return response.data
    }catch(err){
        console.log("Error: ",err.message);
    }
}

export const ProfileEdit = async (formData)=>{
    try{ 
        
        console.log("Gönderilen verilerC: ", {formData});
        const response = await axios.post(
            `${apiURL.Base_URL}User/edit-profile`,
            formData,
            // {
            //   headers: {
            //     "Content-Type": "multipart/form-data",
            //   },
            // }
          );
       
    }catch(err){
        console.log("Error: ",err.message);
    }
}