import { Outlet } from "react-router-dom";
import Logo from "../components/Text/Logo";

import styles from './GuestLayout.module.css'

export default function GuestLayout() {
    return (
        <div id={styles['auth']}>
            <div id={styles['auth-logo']}>
                <Logo color="dark"/>
            </div>
            <Outlet/>
        </div>
    )
}