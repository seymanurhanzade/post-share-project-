import React,{useEffect, useState} from 'react'
import {CommentPost,AddComment} from '../Services/communtService';
import { PostDetails } from '../Services/shareService';
import { useParams } from 'react-router-dom';
import { format } from 'date-fns';
import './CSS/CommentStyle.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faArrowLeftLong,faSpinner } from '@fortawesome/free-solid-svg-icons';
import PostComponent from '../ReusableComponents/PostComponent';
import { UserProfileDetail } from '../Services/userService';

export default function CommentComponent({getUser}) {
  const { postId } = useParams(); 
  const [comments, setComments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [home, setHome] = useState(false);
  const [userDetails,  setUserDetails] = useState([]);
  const [postDetails, setPostDetails] = useState([]);
  const [reply, setReply] = useState('');
  const [alert, setAlert] = useState({ message: '', color: '', border: '' });

  const userId = getUser.id;

  const handleSubmitComment = async (event) => {
    event.preventDefault();
    try {
      const response = await AddComment(userId, postId, reply);
      setAlert({
        message: response.message,
        color: response.success ? 'rgba(60, 193, 71, 0.545)' : 'rgba(193, 60, 60, 0.545)',
        border: response.success ? '1px solid rgba(60, 193, 71, 0.715)' : '1px solid rgba(193, 60, 60, 0.715)',
      });
      setTimeout(() => setAlert(''), 3000);
      setReply("")
    } catch (err) {
      setAlert({ message: 'Bir hata oluştu. Tekrar deneyin', color: 'red', border: '1px solid red' });
    }
  };



  const handleCommentClick = (id) => {
    console.log(`Comment clicked for ID: ${id}`);
    // Comment işlemleri burada yapılabilir
  };

  useEffect(() => {
    const fetchComments = async () => {
      try {
        const entity = await PostDetails(postId,userId);
        console.log(entity);
        const response = await CommentPost(postId);
        setPostDetails(entity);
        setComments(response);
        const img = await UserProfileDetail(userId);
        setUserDetails(img);
      } catch (error) {
        console.error("Error fetching comments:", error);
      } finally {
        setLoading(false);
      }
    };

    

    if (postId) {
      fetchComments();
    }
  }, [postId,comments]);

  return (
    <>
      {alert.message && (
        <p className="success-alert mt-1" style={{ backgroundColor: alert.color, border: alert.border }}>
          {alert.message}
        </p>
      )}
      <div className="icerik-hepsi">
        <div className="icerik pt-3">
          <h3 className='ps-2'><FontAwesomeIcon icon={faArrowLeftLong} style={{ fontSize: 24, marginRight: 10 }} />Post</h3>
          <hr />
          {loading && (
            <>
              <div className="log-ic">
                <FontAwesomeIcon icon={faSpinner} className='loading-icon' />
              </div>
            </>
          )}
          {!loading && (
            <>{Array.isArray(postDetails) && postDetails.length > 0 ? postDetails.map((repo) => (
            <><PostComponent
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
            </> 
          )) : <p>boş</p>}

          <div className="comment-add">
            <form onSubmit={handleSubmitComment}>
            {userDetails && userDetails.length>0?(userDetails.map((repos)=>(
              <div className="row">
                <div className="col-md-1">
                  <div className="img-profile">
                    <img width={40} src={`/Images/${repos.userImages}`} alt="" />
                  </div>
                </div>
                <div className="input-text-comment ms-3 col-md-7">
                  <input
                    placeholder="Cevabını gönder"
                    type="text"
                    className='input-text bg-black w-100'
                    name='reply'
                    value={reply}
                    onChange={(e) => setReply(e.target.value)}
                  />
                </div>
                <div className="comment-button mt-auto mb-auto ms-4 col-md-2">
                  <button className="btn" type='submit'>Cevapla</button>
                </div>
              </div>
              ))
                    
            ):(<>Bir hata oluştu.</>)}
            </form>
          </div>

          {Array.isArray(comments) && comments.length > 0 ? comments.map((repo) => (
            <div className="gönderi-icerik" key={repo.id}>
              <hr style={{margin:5}} />
              
              {repo.getPostModelList.sort((a,b)=> new Date(b.time)- new Date(a.time)).map((comment) => (
                <div className="comments mt-2" key={comment.id}>
                  <div className="row g-0 ms-1">
                    <div className="imgprofile col-md-2">
                      <div className="img-profile">
                        <a href={`/profile/${comment.userId}`}>
                          <img width={40} src={`/Images/${comment.images}`} alt="" />
                        </a>
                      </div>
                    </div>
                    <div className="col">
                      <div className="user me-2">
                          <a href={`/profile/${comment.userId}`}><div className="fullname inline-block">{comment.fullName}</div></a>
                          <a href={`/profile/${comment.userId}`}><div className="username inline-block">{comment.atUserName}</div></a>
                          
                          <div className="time inline-block">{format(new Date(comment.time), 'yyyy-MM-dd HH:mm')}</div>
                        </div>
                          <div className="share">
                            <p>{comment.communt}</p>
                          </div>
                      </div>
                    </div>
                    <div className="yhj mt-2"></div>
                </div>
              ))}
            </div>
          )) : <p className='ps-2'>İlk yorum yapan siz olun.</p>}
            </>
          )}
          
        </div>
      </div>
    </>
  );
}
