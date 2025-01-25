import React, { useEffect, useState } from 'react'
import { GetPosts,PostDelete } from '../../Services/adminservice';
import { format } from 'date-fns';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';


export default function AdminPostsComponent() {
    const [posts, setPosts] = useState([]);

  useEffect(()=>{
    const getPosts= async()=>{
      var data = await GetPosts();
      setPosts(data);
    }
    getPosts();

  },[posts])

  const DeletePost=(id)=>{
    try{
        if(id!=null){
            const responce = PostDelete(id);
        }
        
    }catch(err){
        console.log(err);
    }
    
  }
  return (
    <>
    <div className="admin-posts">
        <h3>Posts</h3>
        <div className="adm">                
            <div>
                <table class="table table-striped">
                    <thead>
                        <tr>
                        <th scope="col">#</th>
                        <th scope="col">Post</th>
                        <th scope="col">Time</th>
                        <th scope="col">UserId</th>
                        <th></th>
                        </tr>
                    </thead>
                    <tbody>
                    {posts && posts.length>0 ? (posts.map((repo)=>(
                        <tr>
                            <th scope="row">{repo.id}</th>
                            <td>{repo.share}</td>
                            <td>{format(new Date(repo.time), 'yyyy-MM-dd HH:mm')}</td>
                            <td>{repo.userId}</td>
                            <td> <button className='btn btn-danger' onClick={()=> DeletePost(repo.id)}><FontAwesomeIcon icon={faTrash} /></button></td>
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
