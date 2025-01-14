import { redirect, Route } from "react-router-dom";
import { useAuth } from "../Authentication/AuthenticationProvider";

interface AuthenticatedRouteProps {
    element: React.ReactElement
}

export default function AuthenticatedRoute({element, ...rest}: AuthenticatedRouteProps) {
    const { isAuthenticated } = useAuth();

    return isAuthenticated
    ? (
        <Route {...rest} element={element}/>
    )
    : (
        redirect('/login')
    ) 
}