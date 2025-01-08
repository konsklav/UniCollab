import { NavLink, To } from "react-router-dom";
import styles from './NavItem.module.css'

interface NavItemProps {
    to: To
    children: React.ReactNode
}

export default function NavItem({to, children}: NavItemProps) {
    return (
        <li>
            <NavLink className={styles.navItem} to={to}>{children}</NavLink>
        </li>
    )
}