import React, { useState,useEffect } from 'react'
import { useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHouse, faUser, faEnvelope,faXmark } from '@fortawesome/free-solid-svg-icons';
import { logout} from "../Services/authService";
import { UserProfileDetail } from '../Services/userService';
import { getUserBilgileri } from '../Services/authService';


export default function MenuComponent({userId}) {
    const [model, setModel]= useState(false);
    const [userDetails,  setUserDetails] = useState([]);
    const [userRole,  setUserRole] = useState(false);
    const navigate = useNavigate();
    const user = getUserBilgileri(); // Kullanıcı bilgilerini al
    useEffect(() => {
    const fetchUsers = async () => {


        try {
            const responce = await UserProfileDetail(userId);
            setUserDetails(responce);
        } catch (error) {
            console.error(error);
        }
        };
        fetchUsers();
    }); 
    
    const Logout = () => {
            logout(); // localStorage temizlenir
            console.log("Çıkış yapıldı.");
            navigate("/register"); // Kullanıcıyı giriş sayfasına yönlendir
        };
  return (
    <>
    <div className="as-left-list">
        <div className="as-left-list-t ms-4">
            <div className="as-m">
                <div className="as-ms inline-block">
                    <a href="/">
                        <div className="col">LOGO</div>
                    </a>
                </div>
            </div>
            <div className="as-m">
                <div className="as-ms inline-block bg-hover">
                    <a href="/">
                        <div className="row align-items-center g-0">
                            <div className="col-md-2 text-center">
                                <FontAwesomeIcon icon={faHouse} />
                            </div>
                            <div className="col">Anasayfa</div>
                        </div>
                    </a>
                </div>
            </div>
            <div className="as-m">
                <div className="as-ms inline-block bg-hover">
                    <a href={`/profile/${userId}`}>
                        <div className="row align-items-center g-0">
                        <div className="col-md-2 text-center">
                            <FontAwesomeIcon icon={faUser} />
                        </div>
                        <div className="col">Profil</div>
                        </div>
                    </a>
                </div>
            </div>
            
            <div className="as-m">
            {user && user.id === 'da127330-4872-412d-b3d2-2a71bf9dc638' && (
                <div className="as-ms inline-block bg-hover">
                <a href="/admin">
                    <div className="row align-items-center g-0">
                    <div className="col-md-2 text-center">
                        <FontAwesomeIcon icon={faEnvelope} />
                    </div>
                    <div className="col">Yönetim</div>
                    </div>
                </a>
                </div>
            )}
            </div>         
        </div>        
        <div className="as-user-profile-link ms-2">
            <div className="jhgf transition-03 font-12 bg-hover border-radius-50 p-2">
                <a type='button' className='as-user-a-link ' onClick={()=>setModel(true)}>
                {userDetails && Array.isArray(userDetails) && userDetails.length > 0 ? (
                    userDetails.map((repo) => (
                        <div className="row g-0" key={repo.id}>
                            <div className="col-md-2">
                                <div className="as-imgprofile border-radius-50">
                                    <div className="as-img-profile">
                                        <img width={40} src={`/Images/${repo.userImages}`} alt="" />
                                    </div>
                                </div>
                            </div>
                            <div className="col as-username ms-2">
                                <span style={{ fontWeight: 700 }}>{repo.fullName}</span> <br />
                                <span style={{ color: '#464646' }}>{repo.atUserName}</span>
                            </div>
                        </div>
                    ))
                ) : (
                    <>Bir hata oluştu.</>
                )}

                    
                </a>
            </div>
            
        </div>
    </div>

    {model && (
            <>
            <div className="modal-disi" onClick={()=>setModel(false)}>
                <div className="menu-model">
              <div className="modal  fade show"  data-bs-theme="dark" style={{ display: 'block'}} tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div className="model-dialog">
                  <button type="button" class="btn-close" aria-label="Close" style={{fontSize:13}} onClick={()=>setModel(false)}></button>
                    <div className="modal-body">
                    {userDetails && userDetails.length>0?(userDetails.map((repo)=>(
                        <> 
                        <div className="" id="">
                        <a type='button' onClick={Logout}>Çıkış <span className='ms-2'>{repo.atUserName}</span></a>
                    </div>
                        </>
                    ))):(<>Bir hata oluştu.</>)}
                   
                    </div>
                </div>
              </div>
            </div>
            </div>
            
              
            </>
          )}
    
    
    
    
    </>
  )
}
