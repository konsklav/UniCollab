import { Outlet } from "react-router-dom";
import Navbar from "../components/Navbar/Navbar";

import styles from './Layout.module.css'

export default function Layout() {
    return (
        <>
            <div id={styles.layout} className="row">
                <div className="col-sm-2 d-flex flex-column">
                    <Navbar/>
                </div>
                <div className="col">
                    <Outlet/>
                </div>
            </div>
        </>
    )
}