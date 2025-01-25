import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { getUserBilgileri } from './Services/authService';
import RegisterComponent from './Components/RegisterComponent';
import HomeComponent from './Components/HomeComponent';
import LoginComponent from './Components/LoginComponent';
import ProfileComponent from './Components/ProfileComponent';
import CommentComponent from './Components/CommentComponent';
import MainComponent from './Components/MainComponent';
import FollowUserComponent from './Components/FollowUserComponent';
import AdminComponent from './Components/AdminComponents/AdminComponent';
import UnauthorizedComponent from './ReusableComponents/UnauthorizedComponent';
import PrivateRoute from './ReusableComponents/PrivateRoute'; // PrivateRoute bileşenini içeri aktar

const user = getUserBilgileri();

export default function RouterModel() {
  return (
    <Routes>
      <Route path="/" element={<MainComponent user={user} />}>
        <Route path="/" element={<PrivateRoute element={<HomeComponent getUser={user} />} />} />
        <Route path="/profile/:userId" element={<PrivateRoute element={<ProfileComponent getUser={user} />} />} />
        <Route path="/post-detail/:postId" element={<PrivateRoute element={<CommentComponent getUser={user} />} />} />
        <Route path="/profile/followUsers/:userId" element={<PrivateRoute element={<FollowUserComponent getUser={user} />} />} />
      </Route>

      <Route path="/unauthorized" element={<UnauthorizedComponent />} />
      <Route path="/admin" element={<PrivateRoute element={<AdminComponent />} />} />

      <Route path="/register" element={<RegisterComponent />} />
      <Route path="/login" element={<PrivateRoute element={<LoginComponent/>} />}/>
    </Routes>
  );
}
