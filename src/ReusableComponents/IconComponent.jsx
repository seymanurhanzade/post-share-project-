import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHeart, faComment, faTrash } from '@fortawesome/free-solid-svg-icons';
import {  IsLikes,IsPostLikedByUser, Delete } from '../Services/shareService';
import useAlert from '../Hooks/AlertMessage';


export default function CommentIcons({postId,  count, commentCount, onCommentClick, getUserId, userId }) {
  const [isLikeState, setIsLikeState] = useState(false);
  const [bool, setbool] = useState(false);
  const { alert, showAlert, showError } = useAlert();
  

  const LikeAdd = async () => {
      try {
        await IsLikes(postId, getUserId);
        // setData((prevData) =>
        //   prevData.map((item) =>
        //     item.postId === postId ? { ...item, isLiked: !item.isLiked, count: item.isLiked ? item.count - 1 : item.count + 1 } : item
        //   )
        // );
      } catch (err) {
        console.error(err);
      }
    };

useEffect(() => {
    const fetchLikeStatus = async () => {
        const ent = await IsPostLikedByUser(postId, getUserId);
        console.log("postId: ",postId," UserId: ",getUserId,"Response in Component:", ent);
        setIsLikeState(ent); 
    };

    fetchLikeStatus();
}, [postId, getUserId]); 

  const toggleLike = async () => {
    try {
        const newLikeState = !isLikeState;
        setIsLikeState(newLikeState); // UI'yi anında güncelle
    } catch (error) {
        console.error('Error toggling like:', error);
    }
  };


const handleDeleteClick= async ()=>{
    try{
        var response = await Delete(postId);
      if(response.success){
        showAlert("Silindi!", true)
      }else{
        showAlert("Bir şeyler yanlış gitti. Tekrar deneyin",false)
      }
    }catch(err){
      showError('Bir hata oluştu. Tekrar deneyin');
    }
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


    <div className="comment-icons">
      <div className="icons inline-block">
        <div className="fav-comment">
          <div className="row g-0 text-center">
            <div className="icon-heart col">
              <div className="icon-heart-a-like inline-block border-radius-50">
                <a
                className='inline-block'
                href="#!"
                onClick={(e) => {
                  e.preventDefault();
                  LikeAdd();
                  toggleLike();
                }}
                style={{ color: isLikeState ? 'red' : 'rgb(173, 173, 173)' }}>
                  <FontAwesomeIcon icon={faHeart} />
                </a>
              </div>

              <a href="#!" className="ms-2 inline-block" style={{ textDecoration: 'none' }}>
                {count}
              </a>
              
            </div>
            <div className="icon-comment col">
              <div className="icon-comment-a-com inline-block border-radius-50">
                <a
                className='icon-comment-link inline-block'
                type='button'
                onClick={(e) => {
                  e.preventDefault();
                  if (onCommentClick) onCommentClick();
                }}><FontAwesomeIcon icon={faComment} /></a>
              </div>
              

              <a href={`/post-detail/${postId}`} className="ms-2 inline-block" style={{ textDecoration: 'none' }}>
                {commentCount}
              </a>
            </div>
              {getUserId=== userId &&
                <div className="icon-faTrash col">
                  
                  <div className="icon-faTrash-a-com inline-block border-radius-50">
                    <a
                    className='icon-faTrash-link inline-block'
                    type='button'
                    onClick={(e) => {
                      e.preventDefault();
                      handleDeleteClick()
                    }}><FontAwesomeIcon icon={faTrash} /></a>
                  </div>
                </div>
              }
              {getUserId!= userId &&
                <div className="icon-faTrash col"></div>
              }
              
          </div>
        </div>
      </div>
      
    </div>
    </>
    

    
  );
}
