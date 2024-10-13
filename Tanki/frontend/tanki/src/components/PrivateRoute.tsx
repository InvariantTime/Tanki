import { ReactNode, useEffect, useState } from "react"
import { Navigate, Outlet } from "react-router-dom"
import { UserService, UserData, invalid } from "../services/UserService"
import { Spinner } from "@chakra-ui/react"
import { Header } from "./Header"

export const PrivateRoute = () => {
    const [loading, setLoading] = useState(true);
    const [user, setUser] = useState<UserData>(invalid);

    const initUser = async () => {
        var result = await UserService.verify();
        setLoading(false);
        setUser(result);
    }

    useEffect(() => {
        initUser();
        console.log();
    }, []);

    if (loading === true) {
        return (
            <div className="h-full w-full flex justify-center items-center">
                <Spinner/>
            </div>
        );
    }

    if (user.isSuccess === false)
        return <Navigate to="/signin"/>

    return (
        <>
            <div className="flex h-[8%]">
                <Header name={user.name} score={user.score}/>
            </div>

            <div className="flex h-[90%]">
                <Outlet />
            </div>
        </>
    );
}