import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../Services/authService";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    setError("");

    try {
      const token = await login(email, password);
      navigate("/");
      console.log("giriş başarılı");

      if(token ==null){
        console.log("token null");
      }else{
        console.log("token: ", token.token)

      }

    } catch (err) {
      setError("Email veya şifre yanlış. Tekrar deneyin.");
    }
  };


  return (
    <>
      <div className="login-page">
      {error && <p style={{ padding:"10px",backgroundColor: "rgba(255, 0, 0, 0.355)" }}>{error}</p>}
        <div className="login-content">  
          <div className="login-content">
            <form onSubmit={handleLogin}>
              <div className="form-group">
                    <div className="form-floating mb-3">
                      <input type="email" className="form-control" id="floatingInput" placeholder="name@example.com" name='Email' value={email} onChange={(e) => setEmail(e.target.value)} />
                      <label htmlFor="floatingInput">Email</label>
                    </div>
                    <div className="form-floating mb-3">
                      <input type="password" className="form-control" id="floatingInput" placeholder="password" name='Password' value={password} onChange={(e) => setPassword(e.target.value)}/>
                      <label htmlFor="floatingInput">Password</label>
                    </div>
                    <div className="login-buton">
                      <button className="btn log-btn" type="submit">Giriş</button>
                    </div>
              </div>
            </form>
          </div>

        </div>
      </div>


    </>
  );
};

export default Login;
