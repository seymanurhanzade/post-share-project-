import React from 'react';
import { Navigate } from 'react-router-dom';
import { getUserBilgileri } from '../Services/authService'; 

// PrivateRoute, kullanıcı bilgilerini kontrol eder ve ona göre yönlendirme yapar
const PrivateRoute = ({ element, ...rest }) => {
  const user = getUserBilgileri(); // Kullanıcıyı kontrol et
  return user.token ? (
    React.cloneElement(element, {...rest}) // JSX öğesini render et
  ) : (
    <Navigate to="/register" /> // Kullanıcı yoksa register sayfasına yönlendir
  );
};

export default PrivateRoute;
