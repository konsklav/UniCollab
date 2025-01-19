import NavItem from "./NavItem";

import './Navbar.module.css'
import Logo from "../Text/Logo";
import { useAuth } from "../Authentication/AuthenticationProvider";

import '../../common/common.styles.css'

export default function Navbar() {
    const auth = useAuth()

    return (
        <div className="bg-prim flex-grow-1 d-flex flex-column">
            <Logo color="light"/>
            <nav className="flex-grow-1">
                <ul className="p-0">
                    <NavItem to={'/'}>Home</NavItem>
                    <NavItem to={'/chat'}>Chat</NavItem>
                    <NavItem to={'/users'}>Users</NavItem>
                    <NavItem to={'/groups'}>My Groups</NavItem>
                    <NavItem to={'/posts'}>My Posts</NavItem>
                </ul>
            </nav>
            <div className="text-center mb-3">
                <button type="button" className="btn btn-danger" onClick={auth.logout}>Sign Out</button>
            </div>
        </div>
    )
}