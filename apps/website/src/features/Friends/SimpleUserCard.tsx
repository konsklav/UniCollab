import { UserInformation } from "../Users/Users.types";

interface SimpleUserCardProps {
    user: UserInformation
}

export default function SimpleUserCard({user}: SimpleUserCardProps) {
    return (
        <div className="card">
            <div className="card-header">
                <h5 className="fw-bold">{user.username}</h5>    
            </div> 
            <div className="card-body">

            </div>
            <div className="card-footer">

            </div>
        </div>
    )
}