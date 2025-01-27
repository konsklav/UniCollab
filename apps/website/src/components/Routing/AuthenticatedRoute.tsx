import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../../state/authentication/authenticationStore";


export default function AuthenticatedRoute() {
    const { isAuthenticated } = useAuth();

    return isAuthenticated() ? <Outlet/> : <Navigate to='/login'/>
}