import { Outlet } from "react-router-dom";
import Navbar from "../components/Navbar/Navbar";

import styles from './Layout.module.css'

export default function Layout() {
    return (
        <div id={styles["app-container"]}>
            <div id={styles.layout}>
                <div className="navigation d-flex flex-column">
                    <Navbar/>
                </div>
                <main id={styles.content}>
                    <Outlet/>
                </main>
            </div>
        </div>
    )
}