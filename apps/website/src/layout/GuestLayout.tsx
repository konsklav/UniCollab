import { Outlet } from "react-router-dom";
import Logo from "../components/Text/Logo";

export default function GuestLayout() {
    return (
        <div id="auth">
            <div id="auth-logo">
                <Logo color="dark"/>
            </div>
            <Outlet/>
        </div>
    )
}