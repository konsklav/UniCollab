import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../../state/authentication/authenticationStore";


export default function AuthenticatedRoute() {
    const { authentication, user } = useAuth();

    return authentication !== 'None' && user ? <Outlet/> : <Navigate to='/login'/>
}