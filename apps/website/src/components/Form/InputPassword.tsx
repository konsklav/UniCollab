import { ChangeEvent, useRef, useState } from "react";
import { generateUniqueId } from "../../utils/uniqueId";
import { PasswordInputProps } from "./inputs.types";



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
        <div className="form-input-group">
            {label !== undefined && <label className="form-label" htmlFor={inputId.current}>{label}</label>}
            <div className="password-input">
                <input type={inputType}
                        placeholder={placeholder}
                        id={inputId.current} 
                        value={value} 
                        onChange={handleInput}/>
                <span onClick={toggleVisible} className="password-toggle-visible">
                    <i className={`bi bi-${eyeIcon}`}/>
                </span>
            </div>
        </div>
    )
}