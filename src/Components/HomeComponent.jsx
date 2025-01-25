import React, { useState, useEffect } from 'react';
import './CSS/HomeStyle.css';
import { shareService, getAllShare, PostDetails } from '../Services/shareService';
import {CommentPost,AddComment} from '../Services/communtService';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark,faSpinner } from '@fortawesome/free-solid-svg-icons';
import PostComponent from '../ReusableComponents/PostComponent';
import useAlert from '../Hooks/AlertMessage';
import { UserProfileDetail } from '../Services/userService';

export default function HomeComponent({getUser}) {

  const [shareContent, setShareContent] = useState({ userId: getUser.id, share: ''});
  const [data, setData] = useState([]);
  const [detailspostId, setPostId] = useState();
  const [reply, setReply] = useState('');
  // const [modelData, setModelData] = useState([]);
  const [model ,setModel]= useState(false);
  const [loading, setLoading] = useState(true);
  const { alert, showAlert, showError } = useAlert();
  const [active, setActive] = useState('HomePost');
  const [userDetails,  setUserDetails] = useState([]);
  const [postDetails, setPostDetails] = useState([]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setShareContent({ ...shareContent, [name]: value });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const response = await shareService(shareContent.userId, shareContent.share);
      setShareContent({ userId: getUser.id, share: '' });
      if(response.success){
        showAlert("İşlem başarılı!", true)
      }else{
        showAlert("Bir şeyler yanlış gitti. Tekrar deneyin",false)
      }
     
    } catch (error) {
      showError('Bir hata oluştu. Tekrar deneyin');
    }
  };

  useEffect(() => {
    const fetchAllShares = async () => {
      try {
        const ent = await getAllShare(shareContent.userId);
        setData(ent);
        const responce = await UserProfileDetail(shareContent.userId);
        setUserDetails(responce);
        
      } catch (error) {
        console.error(error);
      } finally {
        setLoading(false);
      }
    };
    fetchAllShares();
  }, [data]);


  const handleCommentClick=async (id)=>{
    setModel(true);
    try{
      var response = await CommentPost(id);
      setPostId(id)
      const postd = await PostDetails(id,getUser.id);
        setPostDetails(postd);
      
      if(response.success){
        showAlert("İşlem başarılı!", true)
      }
      
    }catch(err){
      showError('Bir hata oluştu. Tekrar deneyin');
    }finally {
      setLoading(false); 
    }
  }

  const handleSubmitComment = async (e,postId) => {
      e.preventDefault();
      try {
        const response = await AddComment(shareContent.userId, postId, reply);
        console.log("cevap: ", response);
        if (response.success) {
          showAlert('Cevap gönderildi!', true); 
        } else {
          showError('Bir şeyler yanlış gitti. Lütfen tekrar deneyin.'); 
        }
      } catch (err) {
        showError('Bir hata oluştu. Tekrar deneyin'); 
      }
  };
  

  
  const getActiveLink=(link)=>{
    return link=== active? {backgroundColor:'#262626'}:{};
  }

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
      <div className="icerik-hepsi">
        <div className="icerik">
          <div className="home-ust-secenekler yhj">
            <div className="row row-h-100 g-0 text-center">
              <div className="col display-grid-place-item bg-hover transition-03"  style={getActiveLink('HomePost')}>
                <a type="button" 
                  onClick={()=>setActive('HomePost')}
                  >Senin için</a>
                </div>
              <div className="col display-grid-place-item bg-hover transition-03" style={getActiveLink('TEHomePost')}>
                <a type="button" 
                  onClick={()=> setActive('TEHomePost')}
                  >Takip ettiklerin</a>
                </div>
            </div>
          </div>
            <div className="share-input" style={{ padding: 7, margin: 'auto' }}>
              <form onSubmit={handleSubmit}>
                <div className="input-group">
                  <textarea
                    maxLength={120}
                  style={{height:90}}
                    className="anasayfa-input w-100 "
                    placeholder="Ne düşünüyorsun?!"
                    name="share"
                    
                    value={shareContent.share}
                    onChange={handleInputChange}
                  />
                </div>
                <span style={{color:'#b6b6b6', fontSize:10, float:'right'}}>Maximum 120 karakter</span>
                <div className="button mt-2">
                  <div className="comment-button mt-auto mb-auto col-md-2">
                    <button className="btn" type='submit'>Paylaş</button>
                  </div>
                </div>
              </form>
            </div>
            
            <hr />
            <div className="gonderiler mt-3">
            {loading && (
              <>
                <div className="log-ic">
                  <FontAwesomeIcon icon={faSpinner} className='loading-icon' />
                </div>
              </>
            )}
            {!loading && (
              <>{active==='HomePost' &&( data && data.length>0 ? (data.map((ent)=>(
                  ent.homePostLists && ent.homePostLists.length>0 ?(ent.homePostLists
                    .sort((a, b) => new Date(b.time) - new Date(a.time)).map((repo) => (
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
                      userId={repo.userId}
                      getUserId={shareContent.userId}
                    />
                    ))):(<p>Bir hata oluştu. Sayfayı yenileyin.</p>) 
                  ))
                ):(<>Bir hata oluştu. Sayfayı yenileyin.</>))
              } 

                {active==='TEHomePost' &&( data && data.length>0 ? (data.map((ent)=>(
                  ent.takipEdilenlerPostLists && ent.takipEdilenlerPostLists.length>0 ?(ent.takipEdilenlerPostLists
                    .sort((a, b) => new Date(b.time) - new Date(a.time)).map((repo) => (
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
                      userId={repo.userId}
                      getUserId={shareContent.userId}
                    />
                    ))):(<p>Bir hata oluştu. Sayfayı yenileyin.</p>) ))
                  ):(<>Bir hata oluştu. Sayfayı yenileyin.</>))}    
              </>
            )} 
            </div>
          </div>
        </div>
      {model && (
        <>
        {alert.message && (
        <p
          className="success-alert mt-1"
          style={{ backgroundColor: alert.color, border: alert.border }}
        >
          {alert.message}
        </p>
      )}
        <div className="home-modal">
          <div className="modal fade show" style={{ display: 'block', marginTop:50}} tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div className="modal-dialog">
              <div className="modal-content">
                <button
                    type="button"
                    className="btn-close mb-2 mt-3 ms-3"
                    style={{color:'white'}}
                    onClick={() => {
                      setModel(false)
                      setPostDetails([])}}
                  ><FontAwesomeIcon icon={faXmark} /></button>
                <div className="modal-body">
                  {loading && (
                    <>
                      <div className="log-ic">
                        <FontAwesomeIcon icon={faSpinner} className='loading-icon' />
                      </div>
                    
                    </>
                  )}
                  {!loading &&(
                    <>
                    {Array.isArray(postDetails) && postDetails.length > 0 ? postDetails.map((repo) => (
                    <>
                      <div className="comment-modal">
                        <div className="gönderi-icerik w-100">
                        <PostComponent
                          key={repo.id}
                          fullName={repo.fullName}
                          atUserName={repo.atUserName}
                          image={repo.image}
                          time={repo.time}
                          share={repo.share}
                          isLiked={repo.isLiked}
                          count={repo.count}
                          commentCount={repo.commentCount}
                          handleCommentClick={handleCommentClick}
                          postId={repo.id}
                          getUserId={getUser.id}
                          userId={repo.userId}
                        />
                        <div className="asd">
                          </div>
                            <div className="cevap-verin mt-1">
                              <span style={{ fontSize:13, paddingLeft:20}}><a href="" style={{textDecoration:'none',fontWeight: 800, color:'rgb(0, 7, 217)'}}>{repo.atUserName}</a> 'e cevap verin</span>
                            </div>
                          </div>
                        </div>
                        <div className="comment-add mt-4">
                          <form onSubmit={(e) => handleSubmitComment(e, repo.id)}>
                            <div className="row">


                              {userDetails&& userDetails.length>0?(userDetails.map((repo)=>(
                                <div className="col-md-2">
                                  <div className="img-profile">
                                    <img width={40} src={`/Images/${repo.userImages}`} alt="" />
                                  </div>
                                </div>
                              ))):(<></>)}
                              
                              <div className="input-text-comment ms-2 col-md-5">
                                <input
                                style={{backgroundColor:'black'}}
                                  placeholder="Cevabını gönder"
                                  type="text"
                                  className='input-text'
                                  name='reply'
                                  value={reply}
                                  onChange={(e) => setReply(e.target.value)}
                                />
                              </div>
                              <div className="comment-button mt-auto mb-auto ms-4 col-md-2">
                                <button className="btn" type='submit'>Cevapla</button>
                              </div>
                            </div>
                          </form>
                        </div>
                      </> 
                    )) : <p>Bir hata olsu. Lütfen tekrar deneyin</p>}
                      </>
                    )
                  }
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
