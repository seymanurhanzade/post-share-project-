import React,{useState,useEffect} from 'react'
import { Outlet } from 'react-router-dom';
import MenuComponent from '../ReusableComponents/MenuComponent';
import SearchComponent from '../ReusableComponents/SearchComponent';
import { GetUsers ,UserProfileDetail } from '../Services/userService';
import './CSS/Style.css';

export default function MainComponent({user}) {
    const [searchTerm, setSearchTerm]= useState("");
    const [filteredUsers, setFilteredUsers]= useState([]);
    const [users ,setUsers]= useState([]);

    useEffect(() => {
        const fetchUsers = async () => {
          try {
            const fetchedUsers = await GetUsers();
            setUsers(fetchedUsers);
            console.log(fetchUsers);
          } catch (error) {
            console.error(error);
          }
        };
        if (searchTerm) {
          fetchUsers();
        }
    
      }, [searchTerm]); 
    
      // Filter işlemi
      useEffect(() => {
        if (searchTerm) {
          setFilteredUsers(
            users.filter((user) =>
              user.atUserName.toLowerCase().includes(searchTerm.toLowerCase())
            )
          );
        } else {
          setFilteredUsers([]); 
        }
      }, [searchTerm, users]);
return (
    <>
    <div className="icerik-hepsi">
        <div className="row g-0">
            <div className="col">
                <MenuComponent
                    userId={user.id}
                />
            </div>
            <div className="col-md-4">
                <Outlet/>
            </div>
            <div className="col ms-2">
               <div className="col ms-2">
                    <SearchComponent
                        searchTerm={searchTerm}
                        setSearchTerm={setSearchTerm}
                        filteredUsers={filteredUsers}
                    />
                </div>
            </div>
        </div>
    </div>
    </>
);
}
