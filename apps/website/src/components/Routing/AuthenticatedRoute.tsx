import { Navigate, Route, RouteProps } from "react-router-dom";
import { useAuth } from "../Authentication/AuthenticationProvider";


export default function AuthenticatedRoute({element, ...rest}: RouteProps ) {
    const { isAuthenticated } = useAuth();

    return isAuthenticated
    ? (
        <Route {...rest} element={element}/>
    )
    : (
        <Navigate to={'/login'} replace/>
    ) 
}