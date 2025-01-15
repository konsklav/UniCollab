import { ChangeEvent, useRef, useState } from "react";
import { generateUniqueId } from "../../utils/uniqueId";
import { PasswordInputProps } from "./inputs.types";

import styles from './inputs.module.css'

export default function InputPassword({value, onChange, placeholder, label, id, isVisible = false}: PasswordInputProps) {
    const [visible, setVisible] = useState(isVisible)
    const inputId = useRef(id ?? generateUniqueId())

    const inputType = visible ? 'text' : 'password'
    const eyeIcon = visible ? 'eye-slash' : 'eye'

    const toggleVisible = () => setVisible(v => !v)

    function handleInput(event: ChangeEvent<HTMLInputElement>): void {
        onChange(event.target.value)
    }

    return (
        <div className={styles["form-input-group"]}>
            {label !== undefined && <label className={styles["form-label"]} htmlFor={inputId.current}>{label}</label>}
            <div className={styles["password-input"]}>
                <input type={inputType}
                        placeholder={placeholder}
                        id={inputId.current} 
                        value={value} 
                        onChange={handleInput}/>
                <span onClick={toggleVisible} className={styles["password-toggle-visible"]}>
                    <i className={`bi bi-${eyeIcon}`}/>
                </span>
            </div>
        </div>
    )
}