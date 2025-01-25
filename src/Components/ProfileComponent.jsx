// ProfileComponent.js
import React, { useEffect, useState } from 'react';
import { IsLikes,Delete } from '../Services/shareService';
import { userProfilePost, UserProfileDetail ,ProfileEdit } from '../Services/userService';
import { TakipEtServisi,TakipDurumu } from '../Services/followService';
import './CSS/ProfileStyle.css';
import PostComponent from '../ReusableComponents/PostComponent';
import {  useParams } from 'react-router-dom';
import useAlert from '../Hooks/AlertMessage';


export default function ProfileComponent({getUser}) {
  const {userId} = useParams();
  const [updateData, setUpdateData] = useState({
    fullName: "",
    Images: null, // Dosya null olarak başlatılır
  });
  const [userProfilePostData, setUserProfilePostData] = useState(null);
  const [userProfileData, setUserProfileData] = useState();
  const {alert, showAlert, showError } = useAlert();
  const [resimmodel, setResimModel] = useState(false);
  const [isFollowing, setIsFollowing] = useState(false);
  

  const [activeTab, setActiveTab] = useState('gönderiler');
  useEffect(() => {
    const profilPostlari = async () => {
      try {
        const response = await userProfilePost(userId);
        const entity= await UserProfileDetail(userId);
        const state = await TakipDurumu(getUser.id, userId);
        console.log(response);
        setUserProfilePostData(response);
        setUserProfileData(entity);
        setIsFollowing(state)
      } catch (error) {
        console.log("error: ", error);
      }
    };
    profilPostlari();
  }, [userProfilePostData,userProfileData]);

  const handleCommentClick = (id) => {
    console.log("Comment click for ID:", id);
  };

  const TakipEtButonu = async(e)=>{
    e.preventDefault();
    try{
      await TakipEtServisi(getUser.id, userId);
    }catch(err){
      console.error(err);
    }
  }

  const handleChange = (e) => {
    const { name, value, files } = e.target;
    if (name === "Images") {
      setUpdateData({ ...updateData, [name]: files[0] }); 
    } else {
      setUpdateData({ ...updateData, [name]: value }); 
    }
  }; 

  const handleSubmit  = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append("Images", updateData.Images); 
    formData.append("fullName", updateData.fullName);
    formData.append("UserId", getUser.id); 

    try {
      for (let pair of formData.entries()) {
        console.log(pair[0] + ": " + pair[1]);
      }
      
      var resp = await ProfileEdit(formData);

      console.log("Veri başarıyla gönderildi:", formData);
    } catch (error) {
      console.error("Veri gönderme hatası:", error);
    }
  };

 
  return (
    <>
      {alert.message && (
        <p
          className="success-alert mt-1"
          style={{ backgroundColor: alert.color, border: alert.border }}
        >
          {alert.message}
        </p>
      )}
      <div className="profil">
        <div className="profile">
          <div className="icerik pt-3">
            {userProfileData && userProfileData.length>0 ? (
              userProfileData.map((ent)=> (
                <div className=' profil-bilgileri' key={ent.id}>
                  <div className="yhj ps-3">
                    <div className="row g-0">
                      <div className="col">
                        <a  onClick={(e)=> setResimModel(true)}>
                          <div className="p-imgprofile">
                            <div className="p-img-profile" >
                              <img width={100} src={`/Images/${ent.userImages}`} alt="" />
                            </div>
                          </div>
                        </a>
                        <div className="profile-username">{ent.fullName}</div>
                      </div>
                      <div className="col takip-col">
                        <div className="takip-buton me-3">
                          {getUser.id===userId &&
                              <div>
                                <button type='submit' onClick={()=> setResimModel(true)} className="btn">Edit</button>
                              </div>
                          }
                          {!isFollowing.status && getUser.id!=userId &&
                            <form onSubmit={(e)=>TakipEtButonu(e, ent.id)}>
                              <div>
                                <button type='submit' className="btn">Takip et</button>
                              </div>
                            </form>
                          }
                          {isFollowing.status && getUser.id!=userId &&
                            <form onSubmit={(e)=>TakipEtButonu(e, ent.id)}>
                              <div>
                                
                                <button type='submit' className="btn">Takibi Bırak</button>
                              </div>
                            </form>
                          }
                        </div>
                      </div>
                    </div>

                    <div className="followTotal mt-3">
                      <div className="row followTotal-row g-0">
                        <div className="takip-edilen col-md-3">
                          <span><a href={`/profile/followUsers/${userId}`} name='takipEdilen' className='inline-block' >
                            <p style={{ fontSize:12, color:'grey'}}><span style={{color:'white'}}>{ent.takipEdilenSayisi}</span>  Takip Edilen</p></a></span>
                        </div>
                        <div className="takipci col-md-3">
                        <span><a href={`/profile/followUsers/${userId}`} name='takipci' className='inline-block'><p style={{ fontSize:12, color:'grey'}}><span style={{color:'white'}}>{ent.takipciSayisi}</span> Takipçi</p></a></span>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div className="profile-nav yhj text-center">
                    <div className="row">
                      <div className="col">
                        <a className='w-100 p-3' type="button" onClick={() => setActiveTab('gönderiler')}>Gönderiler</a>
                      </div>
                      <div className="col">
                        <a className='w-100 p-3' type="button" onClick={() => setActiveTab('begeniler')}>Beğeniler</a>
                      </div>
                    </div>
                  </div>
                </div>
              ))
            ):(<p>Profil bulunamadı.</p>)}
            {userProfilePostData && userProfilePostData.length > 0 ? (
              userProfilePostData.map((user) => (
                <div className="profile2pp mt-2">
                  <div>
                    {activeTab === 'gönderiler' && (
                      <>
                        {user.userProfileList.sort((a, b) => new Date(b.time) - new Date(a.time)).map((repo) => (
                          <PostComponent
                          key={repo.id}
                          fullName={repo.fullName}
                          atUserName={repo.atUserName}
                          image={repo.image}
                          time={repo.time}
                          share={repo.share}
                          count={repo.count}
                          commentCount={repo.commentCount}
                          handleCommentClick={handleCommentClick}
                          postId={repo.id}
                          getUserId={getUser.id}
                          userId={user.userId}
                          />
                        ))}
                      </>
                    )}
                    {activeTab === 'begeniler' && (
                      <>
                        {user.likePosts.sort((a, b) => new Date(b.time) - new Date(a.time)).map((repo) => (
                          <>
                          
                          <PostComponent
                          key={repo.id}
                          fullName={repo.fullName}
                          atUserName={repo.atUserName}
                          image={repo.image}
                          time={repo.time}
                          share={repo.share}
                          count={repo.count}
                          commentCount={repo.commentCount}
                          handleCommentClick={handleCommentClick}
                          postId={repo.postId}
                          getUserId={getUser.id}
                          userId={repo.userId}
                          /></>
                          
                        ))}
                      </>
                    )}
                  </div>
                </div>
              ))
            ) : (
              <div>
                <p className='ps-2' style={{color:'#929292'}}>Henüz bir gönderi yapılmamış</p>
              </div>
            )}
          </div>
          
        </div>
      </div>
      
      {resimmodel && (
          <>
          <div className="modal-backdrop fade show"></div>
          <div className='gjthgd' >
            <div className="modal fade show" data-bs-theme="dark" style={{ display: 'block', marginTop:50, color:'white'}} tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div className="modal-dialog">
              <div className="modal-content">
                <button
                    aria-label="Close"
                    type="button"
                    className="btn-close mb-2 mt-3 ms-3"
                    onClick={() => setResimModel(false)}
                  ></button>
                  <h2 style={{color:'white', fontSize:15}} className='m-auto'>Edit</h2>
                <div className="modal-body">
                  <form onSubmit={handleSubmit} encType='multipart/form-data'>
                      {userProfileData &&  (
                        userProfileData.map((ent)=> (
                          <div className=' profil-bilgileri' key={ent.id}>
                            <div className="yhj ps-3">
                              <div>
                                <div className="col-md-3" style={{margin:'auto'}}>
                                    <div className="p-imgprofile">
                                      <div className="p-img-profile" >
                                        <img width={100} src={`/Images/${ent.userImages}`} alt="" />
                                      </div>
                                    </div>
                                </div>
                              </div>
                            </div>
                            <div class="mb-3">
                              <label for="formFileSm" class="form-label" style={{fontSize:12}}>Fotoğraf yükle</label>
                              <input className="form-control form-control-sm" name="Images" id="formFileSm" type="file" onChange={handleChange}/>
                            </div>
                            <div class="form-floating mb-3">
                            <input type="text" className="form-control" name="fullName" 
                            value={updateData.fullName}
                            id="floatingInput" placeholder="fullName" onChange={handleChange}/>
                          <label htmlFor="floatingInput">İsim-{ent.fullName}</label>
                            </div>
                            <button className="btn btn-primary mt-3" type='submit' style={{float:'right'}}>Güncelle</button>
                          </div>
                        )))
                      }
                  </form>
                </div>
              </div>
            </div>
          </div>
          </div>
          </>
          
      )}
    </>
  );
}
