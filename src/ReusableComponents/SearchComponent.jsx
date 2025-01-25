import React from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';

export default function SearchComponent({searchTerm, setSearchTerm, filteredUsers}) {
  return (
    <div className="as-right-list mt-2">
        <div className="container-fluid">
            <form
            className="search-form"
            role="search"
            onSubmit={e => e.preventDefault()}>
            <div className="as-right-dfg row">
                <div className="col-md-1">
                <FontAwesomeIcon icon={faMagnifyingGlass} />
                </div>
                <input
                className="col search-home"
                type="search"
                placeholder="Search"
                aria-label="Search"
                value={searchTerm}
                onChange={e => setSearchTerm(e.target.value)}/>
            </div>
            </form>
            {searchTerm && (
            <div className="search-results mt-2">
                {filteredUsers.length > 0 ? (
                filteredUsers.map(user => (
                    <div key={user.userId} className="search-result-item bg-hover border-radius-50 ps-1">
                    <div className="jhgfg">
                        <a href={`/profile/${user.userId}`} className='as-user-a-links'>
                        <div className="row g-0" value={user.userId}>
                            <div className="col-md-2">
                            <div className="as-search-img mt-1">
                                <div className="as-search-imgs">
                                    <img className='w-100' width={20} src={`/Images/${user.userImages}`} alt="" />
                                </div>
                            </div>
                            </div>
                            <div className="col search-users w-100 font-12 ms-1">
                            <span>{user.fullName}</span>  <br />
                            <span>{user.atUserName}</span>
                            </div>
                        </div>
                        </a>
                    </div>
                    
                    </div>
                ))
                ) : (
                <div className="no-results">Sonuç bulunamadı.</div>
                )}
            </div>
            )}
        </div>

    </div>
  )
}
