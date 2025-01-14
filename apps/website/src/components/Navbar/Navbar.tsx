import NavItem from "./NavItem";

import './Navbar.module.css'
import Logo from "../Text/Logo";
import { useAuth } from "../Authentication/AuthenticationProvider";

export default function Navbar() {
    const auth = useAuth()

    return (
        <div className="bg-secondary flex-grow-1">
            <Logo color="light"/>
            <nav className="flex-grow-1">
                <ul className="p-0">
                    <NavItem to={'/'}>Home</NavItem>
                    <NavItem to={'/chat'}>Chat</NavItem>
                </ul>
            </nav>
            <div className="text-center p-3">
                <button type="button" className="btn btn-danger" onClick={auth.logout}>Sign Out</button>
            </div>
        </div>
    )
}