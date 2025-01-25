import {useState} from 'react'

export default function AlertMessage() {

    const [alert, setAlert] = useState({ message: '', color: '', border: '' });

    const showAlert = (message, success = true) => {
        setAlert({
          message: message,
          color: success ? 'rgba(60, 193, 71, 0.545)' : 'rgba(193, 60, 60, 0.545)',
          border: success ? '1px solid rgba(60, 193, 71, 0.715)' : '1px solid rgba(193, 60, 60, 0.715)',
        });
        setTimeout(() => setAlert({ message: '', color: '', border: '' }), 3000);
      };
    
      const showError = (message) => {
        showAlert(message, false);
      };
    
      return { alert, showAlert, showError };

}
