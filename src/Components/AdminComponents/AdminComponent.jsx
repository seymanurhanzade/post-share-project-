import React,{useEffect, useState} from 'react'
import AdminPostsComponent from './AdminPostsComponent'
import AdminUsersComponent from './AdminUsersComponent'
import { useNavigate } from "react-router-dom";
import { getUserBilgileri } from '../../Services/authService';

export default function AdminComponent() {
  const [menu, setMenu]= useState('kullanicilar');
  const navigate= useNavigate();

  useEffect(() => {
    const userRole = getUserBilgileri();
    console.log("------", userRole);
    if (userRole.id !== "da127330-4872-412d-b3d2-2a71bf9dc638") {
      navigate("/unauthorized");
    }
  }, [navigate]);

  
  return (
    <>
    <div className="admin-page">
      <div className="admin-icerik">
        <div className="row g-0">
          <div className="header">
            <h3>Admin Panel</h3>
          </div>
          <div className="col-md-2 admin-menu">
            <div className='admin-secenekler p-2 ps-4'>
              <a href="" onClick={(e)=> {e.preventDefault(); setMenu('kullanicilar')}}>Kullanıcılar</a>
            </div>
            <div className='admin-secenekler p-2 ps-4'>
              <a href="" onClick={(e)=>{e.preventDefault(); setMenu('postlar')}}>Postlar</a>
            </div>
          </div>
          <div className="col">
            <div className="users-page-admin">
              {menu==='kullanicilar' && <AdminUsersComponent/>}
              
            </div>
            <div className="posts-page-admin">
              {menu==='postlar' && <AdminPostsComponent/>}
            </div>
          </div>
        </div>
      </div>
    </div>
    </>
  )
}
