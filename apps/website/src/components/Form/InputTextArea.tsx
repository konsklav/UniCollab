import { ChangeEvent, useRef } from 'react'
import styles from './inputs.module.css'
import { generateUniqueId } from '../../utils/uniqueId'

interface BetterInputProps {
    id?: string,
    className?: string
    label?: string,
    value: string,
    onChange: (value: string) => void
}

export default function InputTextArea(props: BetterInputProps) {
    const inputId = useRef(props.id ?? generateUniqueId())

    function handleInput(event: ChangeEvent<HTMLTextAreaElement>): void {
        props.onChange(event.target.value)
    }

    return (
        <div className={`${styles["form-input-group"]} ${props.className}`}>
            {props.label !== undefined && <label className={styles["form-label"]} htmlFor={inputId.current}>{props.label}</label>}
            <textarea onChange={handleInput} value={props.value}/>
        </div>
    )
}