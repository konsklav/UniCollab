import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../../state/authentication/authenticationStore";


export default function AuthenticatedRoute() {
    const { authentication } = useAuth();

    return authentication !== 'None' ? <Outlet/> : <Navigate to='/login'/>
}