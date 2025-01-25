import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './CSS/Register.css';
import { register } from '../Services/authService';
import LoginComponent from './LoginComponent'
import useAlert from '../Hooks/AlertMessage';

export default function RegisterComponent() {
  const [registerData, setRegisterData] = useState({
    userName: '',
    fullName: '',
    email: '',
    password: ''
  });
  const navigate = useNavigate();
  const [model, setModel] = useState(false);
  const {alert, showAlert, showError}= useAlert();

  const [stars, setStars] = useState([]);
  useEffect(() => {
    // Rastgele yıldızları oluştur
    const numberOfStars = 100; // Yıldız sayısı
    const uretilenYildizlar = [];

    for (let i = 0; i < numberOfStars; i++) {
      uretilenYildizlar.push({
        id: i,
        left: Math.random() * 100, // Rastgele yatay pozisyon (%)
        delay: Math.random() * 3, // 0-5 saniye arasında rastgele animasyon gecikmesi
        duration: Math.random() * 15 + 10, // 2-5 saniye arasında rastgele animasyon süresi
      });
    }

    setStars(uretilenYildizlar);
  }, []);

  const thisRegisterData = (e) => {
    const { name, value } = e.target; //inputun name ve value değerlerini alır
    setRegisterData({
      ...registerData,
      [name]: value
    });
  }

  const registerDataSubmit = async (e) => {
    e.preventDefault();
    try {
      const result = await register(registerData.userName, registerData.fullName, registerData.email, registerData.password);
      if(result.success){
        showAlert("Kayıt başarılı! Giriş yapabilirsiniz.", true)
        setModel(true);
      }else{
        showAlert("Bir hata oldu. Lütfen tekrar deneyin!", false)
      }
      
      // navigate("/register");
    } catch (err) {
      showError("Bir hata oldu. Lütfen tekrar deneyin.")
    }
  }

  return (
    <>
      {alert.message && (
        <p
          className="success-alert-register mt-1"
          style={{ backgroundColor: alert.color, border: alert.border }}
        >
          {alert.message}
        </p>
      )}
      <div className="tum-sayfa">
        <div className="animasyon">
          <div className="row">
            <div className="stars-container">
              {stars.map((star) => (
                <div
                  key={star.id}
                  className="star"
                  style={{
                    left: `${star.left}%`,
                    animationDelay: `${star.delay}s`,
                    animationDuration: `${star.duration}s`,
                  }}
                ></div>
              ))}
            </div>

            <div className="col left mt-auto mb-auto">
              <div className=''>
                <p>LOGO</p>
              </div>
            </div>
            <div className="col right register-content">
              <div className="cont">
                <h1 className='mb-4'>Şu anda olup bitenler</h1>
                <h2>Hemen katıl.</h2>
                <form onSubmit={registerDataSubmit}>
                  <div className="form-group mt-4">
                    <div className="form-floating mb-3">
                      <input  type="text" className="form-control" id="floatingInput" placeholder="userName" name='userName' value={registerData.userName} onChange={thisRegisterData} />
                      <label htmlFor="floatingInput">Username</label>
                    </div>
                    <div className="form-floating mb-3">
                      <input type="text" className="form-control" id="floatingInput" placeholder="fullName" name='fullName' value={registerData.fullName} onChange={thisRegisterData} />
                      <label htmlFor="floatingInput">Full name</label>
                    </div>
                    <div className="form-floating mb-3">
                      <input type="email" className="form-control" id="floatingInput" placeholder="name@example.com" name='email' value={registerData.email} onChange={thisRegisterData} />
                      <label htmlFor="floatingInput">Email</label>
                    </div>
                    <div className="form-floating mb-3">
                      <input type="password" className="form-control" id="floatingInput" placeholder="password" name='password' value={registerData.password} onChange={thisRegisterData} />
                      <label htmlFor="floatingInput">Password</label>
                    </div>
                    <div className="register-buton">
                      <button className="btn reg-buton">Kayıt oluştur</button>
                    </div>
                  </div>
                </form>
                <div className="gfhg">
                  <div className="p mt-5"><p>Zaten hesabın var mı?</p></div>
                <div className="login-buton">
                  <button className="btn log-buton" onClick={() => setModel(true)}>Giriş yap</button>
                </div>
                </div>
                
              </div>
            </div>
          </div>
        </div>
      </div>

      {model && (
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
                    onClick={() => setModel(false)}
                  ></button>
                  <h2 style={{color:'white'}} className='m-auto'>...'e giriş yap</h2>
                <div className="modal-body"><LoginComponent/></div>
              </div>
            </div>
          </div>
          </div>
          
        </>
      )}
    </>
  );
}
