import { ChildrenProps } from '../common/common.types'
import styles from './NavigationButtons.module.css'

export default function NavigationButtons({children}: ChildrenProps) { 
    return (
        <div className={`btn-group ${styles['nav-btn-group']}`}>
            {children}
        </div>
    )
}