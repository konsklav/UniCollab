import {render, screen} from '@testing-library/react'
import InputText from './InputText'
import '@testing-library/jest-dom'

const mockOnChange = jest.fn()

describe('Rendering', () => {
    test('Renders the input with initial value if specified', () => {
        const initialValue = 'Testing!'
    
        render(<InputText value={initialValue} onChange={mockOnChange}/>)
    
        const inputElement = screen.getByDisplayValue(initialValue)
        expect(inputElement).toBeInTheDocument()
        expect(inputElement).toHaveAttribute('value', initialValue)
    })

    test('Renders the input with a label if specified', () => {
        const label = 'Test Label'

        render(<InputText value='Testing!' label={label} onChange={mockOnChange}/>)

        const labelElement = screen.getByLabelText(label)
        expect(labelElement).toBeInTheDocument()
    })
})