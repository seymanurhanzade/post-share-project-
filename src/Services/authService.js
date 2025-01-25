import axios from 'axios';
import jwt_decode from 'jwt-decode';
import apiURL from '../Components/Environments/environment'

const apiUrl= apiURL.Base_URL;

export const login = async (email, password)=>{
    try{
        const response = await axios.post(apiUrl+ 'Account/login', {email, password});
        const data = response.data;
        console.log("Backend cevabı:", response); 
        if(data){
            localStorage.setItem("userToken", data.token);
            localStorage.setItem("localuserId", data.userId)

        }else{
            console.log("Service: Gelen veride token bulunamadı.");
        }   
        return response.data;
    }catch(error){
        throw error.response.data || "Login işlemi başarısız.";
    }
};

export const getUserBilgileri = () => {
    const token = localStorage.getItem("userToken");
    const id = localStorage.getItem("localuserId");

    return { token, id };
};

export const register = async (UserName,FullName, Email, Password) =>{
    try{
        console.log("Gönderilen veri:", { UserName,FullName, Email, Password });

        const response = await axios.post(apiUrl+'Account/register', {UserName,FullName, Email, Password});
        console.log("Register Backend Cevabı: ", response);
        return {
            success:true,
            message:"Kayıt başarılı. Giriş yapabilirsiniz!"
        }
    }catch(error){
        return {
            success:false,
            message:"Lütfen tekrar deneyin"
        }
    }
}

export const logout = () =>{
    localStorage.removeItem("userToken");
    localStorage.removeItem("localuserId");
}

export const getUserRole = () => {
    const token = localStorage.getItem("userToken");
    if (!token) return null;

    try {
        const decoded = jwt_decode(token);
        return decoded.role || null;
    } catch (err) {
        console.error("Token decode hatası:", err);
        return null;
    }
}
