import { NavLink } from "react-router-dom";
import NavItem from "./NavItem";

import './Navbar.module.css'

const flexConfig = "d-flex flex-sm-column flex-row"

export default function Navbar() {
    return (
        <div className={`bg-secondary flex-grow-1 ${flexConfig}`}>
            <div className="p-2 fs-2 text-center fw-bolder">
                UniCollab
            </div>
            <nav className={`${flexConfig} flex-grow-1`}>
                <ul className="p-0">
                    <NavItem to={'/'}>Home</NavItem>
                    <NavItem to={'/chat'}>Chat</NavItem>
                </ul>
            </nav>
            <div className="text-center p-3">
                <NavLink to={'/login'} className="text-danger">Log Out</NavLink>
            </div>
        </div>
    )
}