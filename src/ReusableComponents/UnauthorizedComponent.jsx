import React from 'react'

export default function UnauthorizedComponent() {
  return (
    <div className='icerik mt-5' style={{textAlign:'center'}}>
        <h1>403 - Yetkisiz Erişim</h1>
        <p>Bu sayfaya erişim hakkınız bulunmuyor</p>

        <a href="/register">Giriş yap</a>
    </div>
  )
}
