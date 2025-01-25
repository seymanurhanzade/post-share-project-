import React, { useEffect, useState } from 'react'
import {TakipList,Takipci,TakipDurumu} from '../Services/followService';
import './CSS/HomeStyle.css';
import { TakipEtServisi } from '../Services/followService';
import { useParams } from 'react-router-dom';


export default function FollowUserComponent({getUser}) {
    const {userId} =useParams();
    const [user, setUser]= useState([]);
    const [isFollowing, setIsFollowing] = useState(false);
    const [loading, setLoading]=useState(false);
    const [follow, setFollow]=useState('TakipEdilenler');
    const [btnUserId, setBtnUserId]=useState('');
    const [UserId, setUserId]=useState([]);
    
    useEffect(()=>{
        const FollowedUsers = async () => {
            try {
                const data = await TakipList(userId);
                setUser(data); 
                for(const id of data){
                    for(const item of id.takipEdilenlerList){
                        if(item.userId!=getUser.id){
                            setUserId(item.userId);
                            const state = await TakipDurumu(getUser.id,item.userId);
                            setIsFollowing(prevState => ({
                                ...prevState,
                                [item.userId]: state 
                            }));
                            
                            console.log("Id2: ", item.userId, "state2: ", state);
                        }
                        
                    }
                    
                }
                

            } catch (err) {
                console.error(err);
            } finally {
                setLoading(false);
            }
        };
        FollowedUsers();
    },[userId,isFollowing])

    const TakipEtButonu = async(e)=>{
        e.preventDefault();
        try{
          await TakipEtServisi(getUser.id, btnUserId);
        }catch(err){
          console.error(err);
        }
      }

  return (
    <div className='icerik'>
            <div className="home-ust-secenekler yhj">
                <div className="row row-h-100 g-0 text-center">
                    <div className="col display-grid-place-item bg-hover transition-03">
                        <a onClick={() => setFollow('TakipEdilenler')} href="#">Takip Edilenler</a>
                    </div>
                    <div className="col display-grid-place-item bg-hover transition-03">
                        <a onClick={() => setFollow('Takipci')} href="#">Takipçiler</a>
                    </div>
                </div>
            </div>
            {user && user.length>0 ?( user.map((repo)=>(
                <>
                <div>
                {follow==='TakipEdilenler'&& repo.takipEdilenlerList && repo.takipEdilenlerList.length>0? (
                    repo.takipEdilenlerList.map((user)=>(
                        <>
                        <div className="follow-page " key={user.userId}>
                        <div className="fw-p mt-2 pt-1">
                            <a className='' href={`/profile/${user.userId}`}>
                                <div className="row g-0">
                                    <div className="imgprofile col-md-2">
                                        <div className="img-profile">
                                            <img width={50} src={`/Images/${user.images}`} alt="" />
                                        </div>
                                    </div>
                                    <div className="col">
                                        <div className="user mb-1 me-2">
                                            <div className="fullname inline-block">{user.fullName}</div>
                                            <div className="username inline-block">{user.atUserName}</div>
                                        </div>
                                    </div>
                                   
                                    <div className="col takip-col">
                                        <div className="takip-buton me-3">

                                        {!isFollowing[user.userId]?.status && getUser.id!= user.userId && 
                                            <form onSubmit={(e)=>TakipEtButonu(e, user.userId)}>
                                                <div>
                                                    <button type='submit' onClick={()=>setBtnUserId(user.userId)}  className="btn">Takip et</button>
                                                </div>
                                            </form>
                                        }
                                        {isFollowing[user.userId]?.status && getUser.id!= user.userId && 
                                            <form onSubmit={(e)=>TakipEtButonu(e, user.userId)}>
                                                <div>
                                                    <button type='submit' onClick={()=>setBtnUserId(user.userId)}  className="btn">Takibi Bırak</button>
                                                </div>
                                            </form>
                                        }
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                        </>
                    ))
                ):(<></>)}
                 {follow==='Takipci' && repo.takipcilerList && repo.takipcilerList.length>0? (
                    repo.takipcilerList.map((user)=>(
                        <>
                        <div className="follow-page " key={user.userId}>
                        <div className="fw-p mt-2 pt-1">
                            <a className='' href={`/profile/${user.userId}`}>
                                <div className="row g-0">
                                    <div className="imgprofile col-md-2">
                                        <div className="img-profile">
                                            <img width={50} src={`/Images/${user.images}`} alt="" />
                                        </div>
                                    </div>
                                    <div className="col">
                                        <div className="user mb-1 me-2">
                                            <div className="fullname inline-block">{user.fullName}</div>
                                            <div className="username inline-block">{user.atUserName}</div>
                                        </div>
                                    </div>
                                    <div className="col takip-col">
                                        <div className="takip-buton me-3">

                                        {!isFollowing && getUser.id!= user.userId  &&
                                            <form onSubmit={(e)=>TakipEtButonu(e, user.userId)}>
                                                <div>
                                                    <button type='submit' onClick={()=>setBtnUserId(user.userId)}  className="btn">Takip et</button>
                                                </div>
                                            </form>
                                        }
                                        {isFollowing && getUser.id!= user.userId &&
                                            <form onSubmit={(e)=>TakipEtButonu(e, user.userId)}>
                                                <div>
                                                    <button type='submit' onClick={()=>setBtnUserId(user.userId)}  className="btn">Takibi Bırak</button>
                                                </div>
                                            </form>
                                        }
                                        
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                        </>
                    ))
                ):(<></>)}
                </div></>
            ))

            ):(<></>)}
        </div>
  )
}
