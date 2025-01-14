import styles from './Logo.module.css'

interface LogoProps {
    color: 'light' | 'dark'
}

export default function Logo({color}: LogoProps) {

    const getColorClass = () => {
        if (color === 'light') return 'text-light'
        if (color === 'dark') return 'text-dark'
    }

    return (
        <div className={`${styles.logo} ${getColorClass()}`}>UniCollab</div>
    )
}