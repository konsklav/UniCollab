import { fireEvent, render, screen } from "@testing-library/react"
import LoginForm from "./LoginForm"
import { LoginCredentials } from "../Users.types"

describe('LoginForm', () => {
    test('Renders two input fields and a submit button', () => {
        render(<LoginForm onLogin={jest.fn()}/>)
        
        const inputs = screen.getAllByRole('textbox')
        expect(inputs).toHaveLength(2)

        const buttons = screen.getAllByRole('button')
        expect(buttons).toHaveLength(1)

    })

    test('Invokes the onLogin() callback when submit button is pressed', () => {
        const mockLogin = jest.fn()
        const expectedLoginCredentials: LoginCredentials = 
        {
            username: 'Nove', 
            password: 'abc123'
        }

        render(<LoginForm onLogin={mockLogin}/>)

        const usernameInput = screen.getByLabelText(/username/i)
        const passwordInput = screen.getByLabelText(/password/i)

        fireEvent.change(usernameInput, {target: {value: expectedLoginCredentials.username}})
        fireEvent.change(passwordInput, {target: {value: expectedLoginCredentials.password}})

        const button = screen.getByRole('button')
        fireEvent.click(button)

        expect(mockLogin).toHaveBeenCalledTimes(1)
        expect(mockLogin).toHaveBeenCalledWith(expectedLoginCredentials)
    })
})