import React from 'react';
import { format } from 'date-fns';
import IconComponent from './IconComponent';
import '../Components/CSS/ProfileStyle.css';


const PostComponent = ({ fullName, atUserName,image, time, share,  count, commentCount,  handleCommentClick,  postId, userId, getUserId }) => {
  
  return (
    <div className="gönderi-icerik">
      <div className="row ms-1">
        <div className="imgprofile col-md-2">
          <div className="img-profile">
            <a href={`/profile/${userId}`}>
              <img width={50} src={`/Images/${image}`} alt="" />
            </a>
          </div>
        </div>
        <div className="col">
          <div className="user mb-1 me-2">
            <a href={`/profile/${userId}`}><div className="fullname inline-block">{fullName}</div></a>
            <a href={`/profile/${userId}`}><div className="username inline-block">{atUserName}</div></a>
            
            <div className="time inline-block">{format(new Date(time), 'yyyy-MM-dd HH:mm')}</div>
          </div>
          <a href={`/post-detail/${postId}`} className="share-link">
            <div className="share">
              <p>{share}</p>
            </div>
          </a>
          </div>
        </div>
        <IconComponent
          postId={postId}
          count={count}
          commentCount={commentCount}
          onCommentClick={() => handleCommentClick(postId)}
          getUserId={getUserId}
          userId={userId}
        />
      <div className='yhj mb-2'></div>
    </div>
  );
};

export default PostComponent;
