import { Button, FormControl, FormLabel, Heading, Input, Link, Text } from "@chakra-ui/react";
import { SyntheticEvent, useState } from "react";
import { useNavigate } from "react-router-dom";

const signInUrl = "http://localhost:5074/api/account/signin";

export const SignInPage = () => {

    const navigate = useNavigate();
    const [name, setName] = useState("");
    const [pass, setPass] = useState("");
    const [loading, setLoading] = useState(false);

    const onSubmit = async (e: SyntheticEvent) =>
    {
        e.preventDefault();

        const data = {
            name: name,
            password: pass
        };

        setLoading(true);

        const options: RequestInit = {
            method: "POST",
            credentials: "include",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        };

        var result = await fetch(signInUrl, options);

        if (result.ok)
        {
            navigate("/");
        }
        else
        {
            const error = await result.text();
            setLoading(false);
            alert(error);
        }
    }

    return (
        <div className="w-full h-full justify-center items-center flex">
            <div className="bg-white p-10 rounded border
                border-slate-300 shadow-xl">
                <form onSubmit={onSubmit}>
                    <Heading textAlign="center" mb="5">
                        Sign in
                    </Heading>

                    <FormControl mb="4">
                        <FormLabel>Name</FormLabel>
                        <Input required borderColor="GrayText" 
                            placeholder="input name" onChange={x => setName(x.target.value)}/>
                    </FormControl>

                    <FormControl mb="4">
                        <FormLabel>Password</FormLabel>
                        <Input required type="password" borderColor="GrayText" 
                            placeholder="input password" onChange={x => setPass(x.target.value)}/>
                    </FormControl>

                    <div className="text-center mb-4">
                        <Button colorScheme="green" type="submit" loadingText="Submitting" isLoading={loading}>
                            Submit
                        </Button>
                    </div>
                </form>

                <Text textAlign="center">
                    Don't have an account yet? {' '}
                    <Link href="/register" color="blue">
                        Register
                    </Link>
                </Text>
            </div>
        </div>
    );
}