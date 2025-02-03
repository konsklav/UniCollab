import NavItem from "./NavItem";

import styles from './Navbar.module.css'
import Logo from "../Text/Logo";

import '../../common/common.styles.css'
import { useAuth } from "../../state/authentication/authenticationStore";
import { useEffect, useRef } from "react";

export default function Navbar() {
    const { invalidate } = useAuth()
    const navbar = useRef<HTMLElement | null>(null)

    const collapse = styles['collapse']
    const navItems = 'nav-items'

    const toggleNavCollapse = () => {
        navbar.current?.classList.toggle(collapse)
    }

    useEffect(() => {
        document.getElementById(navItems)?.childNodes.forEach(el => {
            el.addEventListener('click', toggleNavCollapse)
        })
    }, [])

    return (
        <div className="bg-prim flex-grow-1 d-flex flex-column">
            <div className="d-flex align-items-center">
                <div className="flex-grow-1">
                    <Logo color="light"/>
                </div>
                <button type="button" id={styles['collapse-btn']} className="btn p-4" onClick={toggleNavCollapse}>
                    <i className="text-light fs-1 bi bi-list"></i>
                </button>
            </div>
            <nav className={`${styles['unicollab-navbar']} ${collapse}`} ref={navbar}>
                <ul id={navItems} className="p-0">
                    <NavItem to={'/'}>Home</NavItem>
                    <NavItem to={'/chat'}>Chat</NavItem>
                    <NavItem to={'/users'}>Users</NavItem>
                    <NavItem to={'/groups'}>Groups</NavItem>
                    <NavItem to={'/posts'}>My Posts</NavItem>
                </ul>

                <div className="text-center mb-3">
                    <button type="button" className="btn btn-danger" onClick={invalidate}>Sign Out</button>
                </div>
            </nav>
        </div>
    )
}