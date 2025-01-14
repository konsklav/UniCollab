import { fireEvent, render, screen } from "@testing-library/react"
import { AuthenticationProvider, useAuth } from "./AuthenticationProvider"

describe('', () => {

    function TestComponent() {
        const auth = useAuth()

        const doLogin = () => auth.login()
        const doLogout = () => auth.logout()

        return <>
            <div data-testid='auth'>{auth.isAuthenticated.toString()}</div>
            <button onClick={doLogin} data-testid='login'>Login</button>
            <button onClick={doLogout} data-testid='logout'>Logout</button>
         </>
    }

    test('useAuth should not be null if component is wrapped in AuthenticationProvider', () => {
        render(
            <AuthenticationProvider>
                <TestComponent/>
            </AuthenticationProvider>
        )

        const authElement = screen.getByTestId('auth')
        expect(authElement.textContent).toBe('false')
    })    

    test('useAuth throw error if component is not wrapped in AuthenticationProvider', () => {
        // Supress console errors
        const consoleError = jest.spyOn(console, 'error').mockImplementation(() => {})

        expect(() => render(<TestComponent/>)).toThrow()

        consoleError.mockRestore()
    })

    test('Calling login should make isAuthenticated = true', () => {
        render(
            <AuthenticationProvider>
                <TestComponent/>
            </AuthenticationProvider>
        )
        
        const authElement = screen.getByTestId('auth')
        expect(authElement.textContent).toBe('false')

        const loginButton = screen.getByTestId('login')
        fireEvent.click(loginButton)
        
        expect(authElement.textContent).toBe('true')
    })
})

