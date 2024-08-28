import { ReactNode } from "react"
import { Navigate } from "react-router-dom"
import { UserService } from "../services/UserService"

interface Props {
    children: ReactNode
}

export const PrivateRoute = ({children}: Props) =>
{
    if (UserService.isLogIn() === false)
        return <Navigate to="/register"/>

    return (<>{children}</>);
}