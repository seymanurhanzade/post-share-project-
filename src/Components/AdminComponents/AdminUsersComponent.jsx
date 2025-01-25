import React, { useEffect, useState } from 'react'
import { GetUsers } from '../../Services/adminservice';

export default function AdminUsersComponent() {
    const [data, setData]= useState([]);
    useEffect(()=>{
        const getUsers = async ()=>{
            var data = await GetUsers();
            console.log(data);
            setData(data);
            console.log(data);
        }
        getUsers();
    },[])

    
  return (
    <>
    <div className="admin">
            <h3>Posts</h3>
            <div className="adm">                
                <div>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                            <th scope="col">Image</th>
                            <th scope="col">Id</th>
                            <th scope="col">Full Name</th>
                            <th scope="col">@User Name</th>
                            <th scope="col">Email</th>
                            <th scope="col">User Name</th>
                            <th scope="col">Email Confirmed</th>
                            <th></th>
                            </tr>
                        </thead>
                        <tbody>
                        {data && data.length>0 ? (data.map((repo)=>(
                            <tr>
                                <td>
                                <div className="imgprofile col-md-2">
                                    <div className="img-profile">
                                        <img width={50} src={`./Images/${repo.image}`} alt="" />
                                    </div>
                                </div>
                                </td>
                                <th scope="row">{repo.id}</th>
                                <td>{repo.fullName}</td>
                                <td>{repo.atUserName}</td>
                                <td>{repo.email}</td>
                                <td>{repo.userName}</td>
                                {repo.emailConfirmed=== true&&<td>True</td>}
                                {repo.emailConfirmed=== false&& <td>False</td>}
                                {/* <td><button className='btn btn-danger'><FontAwesomeIcon icon={faTrash} /></button></td> */}
                            </tr>
                            
                        ))):(<></>)}
                        </tbody>
                    </table>
                </div>           
            </div>
        </div>
    </>
  )
}
